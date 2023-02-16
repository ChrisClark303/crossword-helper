using CrosswordHelper.Data.Models;

namespace CrosswordHelper.Data.Import
{
    public class UsualSuspectDataImporter : IUsualSuspectDataImporter
    {
        private readonly ICrosswordHelperManagerRepository _repository;

        public UsualSuspectDataImporter(ICrosswordHelperManagerRepository repository)
        {
            _repository = repository;
        }

        public void Import(string[] data)
        {
            var usualSuspects = data.Select(SplitOutWordAndReplacementText)
                .SelectMany(wr =>
                {
                    var allReplacements = GenerateListOfValidReplacements(wr.replacementText);
                    return GenerateListOfIndividualWords(wr.word)
                        .Select(w => (word: w, allReplacements));
                });

            foreach (var usualSuspect in usualSuspects)
            {
                _repository.AddAUsualSuspect(usualSuspect.word, usualSuspect.allReplacements);
            }
        }

        private static (string word, string replacementText) SplitOutWordAndReplacementText(string item)
        {
            var posOfFirstComma = item.IndexOf(",");
            var word = item[..posOfFirstComma];
            var replacementText = item[(posOfFirstComma + 1)..];
            return (word, replacementText);
        }

        private static string[] GenerateListOfValidReplacements(string replacementText)
        {
            return replacementText.Split(';')
                .Select(t => t.Trim().Trim('\"'))
                .ToArray();
        }

        private static IEnumerable<string> GenerateListOfIndividualWords(string word)
        {
            var words = word.Split('/');
            return words.Select(w => w.Trim()) //Trim
                .Where(w => !w.Contains(' ')) //if any part contains a space, remove it and log it
                .SelectMany(HandlePlurals);
        }



        private const string PluralMarker = "(s)";
        private static IEnumerable<string> HandlePlurals(string word)
        {
            if (word.EndsWith(PluralMarker))
            {
                var nonplural = word.Replace(PluralMarker, string.Empty);
                return new[] { nonplural, $"{nonplural}s" };
            }
            return new[] { word };
        }
    }
}
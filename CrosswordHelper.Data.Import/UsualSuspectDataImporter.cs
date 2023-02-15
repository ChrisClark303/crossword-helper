using CrosswordHelper.Data.Models;

namespace CrosswordHelper.Data.Import
{
    public class UsualSuspectDataImporter
    {
        private readonly ICrosswordHelperManagerRepository _repository;

        public UsualSuspectDataImporter(ICrosswordHelperManagerRepository repository)
        {
            _repository = repository;
        }

        public void Import(string[] data)
        {
            //break each row into word/replacement (tuple?)
            foreach (var item in data) //TODO : Change to select later
            {
                var usualSuspectData = SplitOutWordAndReplacementText(item);
                IEnumerable<string> singleWords = GenerateListOfIndividualWords(usualSuspectData.word);
                string[] allReplacements = GenerateListOfValidReplacements(usualSuspectData.replacementText);

                foreach (var usualSuspect in singleWords)
                {
                    _repository.AddAUsualSuspect(usualSuspect, allReplacements);
                }

            }
        }

        private static string[] GenerateListOfValidReplacements(string replacementText)
        {
            return replacementText.Split(';')
                .Select(t => t.Trim())
                .ToArray();
        }

        private static IEnumerable<string> GenerateListOfIndividualWords(string word)
        {
            var words = word.Split('/');
            return words.Select(w => w.Trim()) //Trim
                .Where(w => !w.Contains(' ')) //if any part contains a space, remove it and log it
                .SelectMany(HandlePlurals);
        }

        private static (string word, string replacementText) SplitOutWordAndReplacementText(string item)
        {
            var posOfFirstComma = item.IndexOf(",");
            var word = item.Substring(0, posOfFirstComma);
            var replacementText = item.Substring(posOfFirstComma + 1);
            return (word, replacementText);
        }

        private const string PluralMarker = "(s)";
        private static IEnumerable<string> HandlePlurals(string word)
        {
            if (word.EndsWith("(s)"))
            {
                var nonplural = word.Replace("(s)", "");
                return new[]{nonplural, $"{nonplural}s"};
            }
            return new[] { word };
        }
    }
}
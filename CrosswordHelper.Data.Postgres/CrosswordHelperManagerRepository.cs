using Npgsql;

namespace CrosswordHelper.Data.Postgres
{

    public class CrosswordHelperManagerRepository : CrosswordHelperRepositoryBase, ICrosswordHelperManagerRepository
    {
        public void AddAnagramIndictor(string word, string notes)
        {
            CallAddWordStoredProc(IndicatorWordType.Anagram, word, notes);
        }

        private void CallAddWordStoredProc(IndicatorWordType indicatorType, string word, string notes)
        {
            var cmdText = $"CALL public.\"Add{indicatorType}Indicators\"(:word,:notes)";
            Execute(cmdText, new[]
            {
                new NpgsqlParameter("word", word),
                new NpgsqlParameter("notes", notes)
            });
        }

        public void AddContainerIndicator(string word, string notes)
        {
            CallAddWordStoredProc(IndicatorWordType.Container, word, notes);
        }

        public void AddReversalIndicator(string word, string notes)
        {
            CallAddWordStoredProc(IndicatorWordType.Reversal, word, notes);
        }

        public void AddSeparator(string word)
        {
            throw new NotImplementedException();
        }

        public void AddRemovalIndicator(string word, string notes)
        {
            CallAddWordStoredProc(IndicatorWordType.Removal, word, notes);
        }

        public void AddAUsualSuspect(string original, params string[] replacements)
        {
            var cmdText = $"CALL public.\"AddUsualSuspect\"(:word,:replacements, :notes)";
            Execute(cmdText, new[]
            {
                new NpgsqlParameter("word", original),
                new NpgsqlParameter("replacements", replacements),
                new NpgsqlParameter("notes", "")
            });
        }

        public void AddLetterSelectionIndicator(string word, string notes)
        {
            CallAddWordStoredProc(IndicatorWordType.LetterSelection, word, notes);
        }

        public void AddHomophoneIndicator(string word, string notes)
        {
            CallAddWordStoredProc(IndicatorWordType.Homophone, word, notes);
        }

        public void AddSubstitutionIndicator(string word, string notes)
        {
            CallAddWordStoredProc(IndicatorWordType.Substitution, word, notes);
        }

        public void AddHiddenWordIndicator(string word, string notes = "")
        {
            CallAddWordStoredProc(IndicatorWordType.HiddenWord, word, notes);
        }
    }
}
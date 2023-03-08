﻿using Npgsql;

namespace CrosswordHelper.Data.Postgres
{

    public class CrosswordHelperManagerRepository : CrosswordHelperRepositoryBase, ICrosswordHelperManagerRepository
    {
        public void AddAnagramIndictor(string word, string notes)
        {
            CallAddWordStoredProc("AddAnagramIndicators", word, notes);
        }

        private void CallAddWordStoredProc(string procName, string word, string notes)
        {
            var cmdText = $"CALL public.\"{procName}\"(:word,:notes)";
            Execute(cmdText, new[]
            {
                new NpgsqlParameter("word", word),
                new NpgsqlParameter("notes", notes)
            });
        }

        public void AddContainerIndicator(string word, string notes)
        {
            CallAddWordStoredProc("AddContainerIndicators", word, notes);
        }

        public void AddReversalIndicator(string word, string notes)
        {
            CallAddWordStoredProc("AddReversalIndicators", word, notes);
        }

        public void AddSeparator(string word)
        {
            throw new NotImplementedException();
        }

        public void AddRemovalIndicator(string word, string notes)
        {
            CallAddWordStoredProc("AddRemovalIndicators", word, notes);
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
            CallAddWordStoredProc("AddLetterSelectionIndicators", word, notes);
        }

        public void AddHomophoneIndicator(string word, string notes)
        {
            CallAddWordStoredProc("AddHomophoneIndicators", word, notes);
        }

        public void AddSubstitutionIndicator(string word, string notes)
        {
            CallAddWordStoredProc("AddSubstitutionIndicators", word, notes);
        }
    }
}
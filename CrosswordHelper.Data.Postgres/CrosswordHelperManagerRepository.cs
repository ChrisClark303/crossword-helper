using Npgsql;

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
            using (var conn = Connect())
            {
                var cmdText = $"CALL public.\"{procName}\"(:word)";
                NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("word", word);
                cmd.Parameters.AddWithValue("notes", notes);
                cmd.ExecuteNonQuery();
            }
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
            using (var conn = Connect())
            {
                var cmdText = $"CALL public.AddUsualSuspect(:word,:replacements)";
                NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("word", original);
                cmd.Parameters.AddWithValue("replacements", replacements);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
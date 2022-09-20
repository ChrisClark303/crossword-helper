using Npgsql;

namespace CrosswordHelper.Data.Postgres
{

    public class CrosswordHelperManagerRepository : CrosswordHelperRepositoryBase, ICrosswordHelperManagerRepository
    {
        public void AddAnagramIndictor(string word)
        {
            CallAddWordStoredProc("AddAnagramIndicators", word);
        }

        private void CallAddWordStoredProc(string procName, string word)
        {
            using (var conn = Connect())
            {
                var cmdText = $"CALL public.\"{procName}\"(:word)";
                NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("word", word);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddContainerIndicator(string word)
        {
            CallAddWordStoredProc("AddContainerIndicators", word);
        }

        public void AddReversalIndicator(string word)
        {
            CallAddWordStoredProc("AddReversalIndicators", word);
        }

        public void AddSeparator(string word)
        {
            throw new NotImplementedException();
        }

        public void AddRemovalIndicator(string word)
        {
            CallAddWordStoredProc("AddRemovalIndicators", word);
        }
    }
}
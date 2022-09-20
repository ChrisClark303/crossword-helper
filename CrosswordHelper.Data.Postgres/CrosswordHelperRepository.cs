using Npgsql;
using System.Data;
using System.Xml;

namespace CrosswordHelper.Data.Postgres
{
    public class CrosswordHelperRepository : CrosswordHelperRepositoryBase, ICrosswordHelperRepository
    {
        private const string Value = "'{\"One\",\"Two\"}'";

        public IEnumerable<WordDetails> CheckWords(string[] words)
        {
            //TestArray();

            var wordDetails = MatchWords("checkcrosswordclue", words);
            return wordDetails;
        }

        private IEnumerable<WordDetails> MatchWords(string procName, string[] words)
        {
            using (var conn = Connect())
            {
                var cmdText = procName;
                NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("words", words);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var wordDetails = new WordDetails()
                    {
                        OriginalWord = reader.GetString("word"),
                        CouldBeAnagramIndicator = reader.GetBoolean("isanagram"),
                        CouldBeContainerIndicator = reader.GetBoolean("iscontainer"),
                        CouldBeReversalIndicator = reader.GetBoolean("isreversal"),
                        PotentialReplacements = reader["replacements"] as string[]
                    };

                    yield return wordDetails;
                };
            }
        }

        private void TestArray()
        {
            using (var conn = Connect())
            {
                NpgsqlCommand cmd = new NpgsqlCommand("ArrayTest", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("testarray", new[] {"One","Two" });
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var returnArray = reader[0] as string[];
                }
            }
        }
    }
}
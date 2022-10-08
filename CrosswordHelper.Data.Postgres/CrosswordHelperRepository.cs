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
            var wordDetails = MatchWords("checkcrosswordclue", words);
            return wordDetails;
        }

        public IEnumerable<string> GetAnagramIndicators()
        {
            return GetIndicatorWords("Anagram");
        }

        public IEnumerable<string> GetContainerIndicators()
        {
            return GetIndicatorWords("Container");
        }

        public IEnumerable<string> GetRemovalIndicators()
        {
            return GetIndicatorWords("Removal");
        }

        public IEnumerable<string> GetReversalIndicators()
        {
            return GetIndicatorWords("Reversal");
        }

        private IEnumerable<string> GetIndicatorWords(string indicatorType)
        {
            using (var conn = Connect())
            {
                var cmdText = $"get{indicatorType}Indicators";
                NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                NpgsqlDataReader reader = cmd.ExecuteReader();
                var listOfWords = new List<string>();
                while (reader.Read())
                {
                    var word = reader.GetString("word");
                    listOfWords.Add(word);
                }

                return listOfWords;
            }
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
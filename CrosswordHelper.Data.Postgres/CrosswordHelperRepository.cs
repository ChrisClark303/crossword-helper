using Npgsql;
using System.Data;

namespace CrosswordHelper.Data.Postgres
{
    public class CrosswordHelperRepository : CrosswordHelperRepositoryBase, ICrosswordHelperRepository
    {
        public WordDetails CheckWord(string word)
        {
            var wordDetails = MatchWords("getwordmatches", word);
            return wordDetails;
        }

        private WordDetails MatchWords(string procName, string word)
        {
            using (var conn = Connect())
            {
                var cmdText = procName;
                NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("word", word);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var wordDetails = new WordDetails()
                    {
                        CouldBeAnagramIndicator = reader.GetBoolean("isanagram"),
                        CouldBeContainerIndicator = reader.GetBoolean("iscontainer"),
                        CouldBeReversalIndicator = reader.GetBoolean("isreversal"),
                        PotentialReplacements = reader["replacements"] as string[]
                    };

                    return wordDetails;
                };
            }

            return null;
        }
    }
}
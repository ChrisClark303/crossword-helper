using CrosswordHelper.Data.Models;
using Npgsql;
using System.Data;
using System.Xml;

namespace CrosswordHelper.Data.Postgres
{
    public class CrosswordHelperRepository : CrosswordHelperRepositoryBase, ICrosswordHelperRepository
    {
        public IEnumerable<WordDetails> CheckWords(string[] words)
        {
            return MatchWords("checkcrosswordclue", words);
        }

        public IEnumerable<IndicatorWord> GetAnagramIndicators()
        {
            return GetIndicatorWords("Anagram");
        }

        public IEnumerable<IndicatorWord> GetContainerIndicators()
        {
            return GetIndicatorWords("Container");
        }

        public IEnumerable<IndicatorWord> GetRemovalIndicators()
        {
            return GetIndicatorWords("Removal");
        }

        public IEnumerable<IndicatorWord> GetReversalIndicators()
        {
            return GetIndicatorWords("Reversal");
        }


        private IEnumerable<IndicatorWord> GetIndicatorWords(string indicatorType)
        {
            //using (var conn = Connect())
            //{
            //    var cmdText = $"get{indicatorType}Indicators";
            //    NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    NpgsqlDataReader reader = cmd.ExecuteReader();
            //    var listOfWords = new List<string>();
            //    while (reader.Read())
            //    {
            //        var word = reader.GetString("word");
            //        listOfWords.Add(word);
            //    }

            //    return listOfWords;
            //}
            return Query<IndicatorWord>($"get{indicatorType}Indicators", (reader) =>
            {
                var word = reader.GetString("word");
                var notes = reader["notes"] as string;
                return new IndicatorWord(word, notes);
            });
        }

        private IEnumerable<T> Query<T>(string cmdText, Func<NpgsqlDataReader, T> resultAction, NpgsqlParameter[]? parameters = null)
        {
            using (var conn = Connect())
            {
                NpgsqlCommand cmd = new(cmdText, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    yield return resultAction(reader);
                }
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

        public IEnumerable<UsualSuspect> GetUsualSuspects()
        {
            using (var conn = Connect())
            {
                var cmdText = "getUsualSuspects";
                NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var usualSuspect = new UsualSuspect(reader.GetString("word"), (reader["replacements"] as string[])!);
                    yield return usualSuspect;
                };
            }
        }
    }
}
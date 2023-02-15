using CrosswordHelper.Data.Models;
using Npgsql;
using System.Data;
using System.Runtime.CompilerServices;
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
            return Query<IndicatorWord>($"get{indicatorType}Indicators", (reader) =>
            {
                var word = reader.GetString("word");
                var notes = reader["notes"] as string;
                return new IndicatorWord(word, notes);
            });
        }

        private IEnumerable<WordDetails> MatchWords(string procName, string[] words)
        {
            return Query<WordDetails>(procName, (reader) =>
            {
                var wordDetails = new WordDetails()
                {
                    OriginalWord = reader.GetString("word"),
                    CouldBeAnagramIndicator = reader.GetBoolean("isanagram"),
                    CouldBeContainerIndicator = reader.GetBoolean("iscontainer"),
                    CouldBeReversalIndicator = reader.GetBoolean("isreversal"),
                    PotentialReplacements = reader["replacements"] as string[]
                };
                return wordDetails;
            }, new NpgsqlParameter("words", words));
        }

        public IEnumerable<UsualSuspect> GetUsualSuspects()
        {
            return Query<UsualSuspect>("getUsualSuspects", (reader) =>
            {
                var usualSuspect = new UsualSuspect(reader.GetString("word"), (reader["replacements"] as string[])!);
                return usualSuspect;
            });
        }

        private IEnumerable<T> Query<T>(string cmdText, Func<NpgsqlDataReader, T> resultAction, params NpgsqlParameter[]? parameters)
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
    }
}
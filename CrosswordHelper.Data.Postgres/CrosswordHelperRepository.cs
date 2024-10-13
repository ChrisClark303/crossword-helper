using CrosswordHelper.Data.Models;
using Npgsql;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml;

namespace CrosswordHelper.Data.Postgres
{
    public class CrosswordHelperRepository : CrosswordHelperRepositoryBase, ICrosswordHelperRepository
    {
        public CrosswordHelperRepository(IConnectionStrings connectionStrings) : base(connectionStrings)
        {
        }

        public IEnumerable<WordDetails> CheckWords(string[] words)
        {
            return MatchWords("checkcrosswordclue", words);
        }

        public IEnumerable<IndicatorWord> GetAnagramIndicators()
        {
            return GetIndicatorWords(IndicatorWordType.Anagram);
        }

        public IEnumerable<IndicatorWord> GetContainerIndicators()
        {
            return GetIndicatorWords(IndicatorWordType.Container);
        }

        public IEnumerable<IndicatorWord> GetRemovalIndicators()
        {
            return GetIndicatorWords(IndicatorWordType.Removal);
        }

        public IEnumerable<IndicatorWord> GetReversalIndicators()
        {
            return GetIndicatorWords(IndicatorWordType.Reversal);
        }

        public IEnumerable<IndicatorWord> GetLetterSelectionIndicators()
        {
            return GetIndicatorWords(IndicatorWordType.LetterSelection);
        }

        public IEnumerable<IndicatorWord> GetHomophoneIndicators()
        {
            return GetIndicatorWords(IndicatorWordType.Homophone);
        }

        public IEnumerable<IndicatorWord> GetSubstitutionIndicators()
        {
            return GetIndicatorWords(IndicatorWordType.Substitution);
        }

        public IEnumerable<IndicatorWord> GetHiddenWordIndicators()
        {
            return GetIndicatorWords(IndicatorWordType.HiddenWord);
        }

        private IEnumerable<IndicatorWord> GetIndicatorWords(IndicatorWordType indicatorType)
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
                    CouldBeRemovalIndicator = reader.GetBoolean("isremoval"),
                    CouldBeHomophoneIndicator = reader.GetBoolean("ishomophone"),
                    CouldBeLetterSelectionIndicator = reader.GetBoolean("isletterselection"),
                    CouldBeHiddenWordIndicator = reader.GetBoolean("ishiddenword"),
                    CouldBeSubstitutionIndicator = reader.GetBoolean("issubstitution"),
                    PotentialReplacements = reader["replacements"] as string[]
                };
                return wordDetails;
            }, new NpgsqlParameter("words", words));
        }

        public IEnumerable<UsualSuspect> GetUsualSuspects()
        {
            return Query<UsualSuspect>("getUsualSuspects", (reader) =>
            {
                return new UsualSuspect(reader.GetString("word"), (reader["replacements"] as string[])!);
            });
        }
    }
}
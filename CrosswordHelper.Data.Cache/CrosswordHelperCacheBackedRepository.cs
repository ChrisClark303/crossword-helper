using CrosswordHelper.Data.Models;

namespace CrosswordHelper.Data.Cache
{
    public class CrosswordHelperCacheBackedRepository(ICrosswordDataCache cache) : ICrosswordHelperRepository
    {
        public IEnumerable<WordDetails> CheckWords(string[] words)
        {
            var q = from word in words
            join anagram in cache.Anagrams on word equals anagram.Word 
                into a from anagrams in a.DefaultIfEmpty()
            join container in cache.Containers on word equals container.Word
                into c from containers in c.DefaultIfEmpty()
            join hiddenWord in cache.HiddenWords on word equals hiddenWord.Word
                into h from hiddenWords in h.DefaultIfEmpty()
            join homophone in cache.Homophones on word equals homophone.Word
                into hp from homophones in hp.DefaultIfEmpty()
            join letterSelection in cache.LetterSelections on word equals letterSelection.Word
                into ls from letterSelections in ls.DefaultIfEmpty()
            join removal in cache.Removals on word equals removal.Word
                into r from removals in r.DefaultIfEmpty()
            join reversal in cache.Reversals on word equals reversal.Word
                into rev from reversals in rev.DefaultIfEmpty()
            join substitution in cache.Substitutions on word equals substitution.Word
                into s from substitutions in s.DefaultIfEmpty()
            join usualSuspect in cache.UsualSuspects on word equals usualSuspect.Word
                into us from usualSuspects in us.DefaultIfEmpty()
            select new WordDetails
            {
                OriginalWord = word,
                CouldBeAnagramIndicator = anagrams != null,
                CouldBeContainerIndicator = containers != null,
                CouldBeHiddenWordIndicator = hiddenWords != null,
                CouldBeHomophoneIndicator = homophones != null,
                CouldBeLetterSelectionIndicator = letterSelections != null,
                CouldBeRemovalIndicator = removals != null,
                CouldBeReversalIndicator = reversals != null,
                CouldBeSubstitutionIndicator = substitutions != null,
                PotentialReplacements = usualSuspects?.Replacements
            };

            return q.ToArray();
        }

        public IEnumerable<IndicatorWord> GetAnagramIndicators()
        {
            return cache.Anagrams;
        }

        public IEnumerable<IndicatorWord> GetContainerIndicators()
        {
            return cache.Containers;
        }

        public IEnumerable<IndicatorWord> GetHiddenWordIndicators()
        {
            return cache.HiddenWords;
        }

        public IEnumerable<IndicatorWord> GetHomophoneIndicators()
        {
            return cache.Homophones;
        }

        public IEnumerable<IndicatorWord> GetLetterSelectionIndicators()
        {
            return cache.LetterSelections;
        }

        public IEnumerable<IndicatorWord> GetRemovalIndicators()
        {
            return cache.Removals;
        }

        public IEnumerable<IndicatorWord> GetReversalIndicators()
        {
            return cache.Reversals;
        }

        public IEnumerable<IndicatorWord> GetSubstitutionIndicators()
        {
            return cache.Substitutions;
        }

        public IEnumerable<UsualSuspect> GetUsualSuspects()
        {
            return cache.UsualSuspects;
        }
    }
}

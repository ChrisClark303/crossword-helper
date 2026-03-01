using CrosswordHelper.Data.Models;

namespace CrosswordHelper.Data.Export
{
    public class CrosswordDataExtractionService(ICrosswordHelperRepository repository, ICrosswordDataCache cache) : ICrosswordDataExtractionService
    {
        public async Task ExportCrosswordDataToCache()
        {
            await ExportIndicatorWordsToCache(cache.Anagrams, repository.GetAnagramIndicators, cache.SetAnagrams);

            await ExportIndicatorWordsToCache(cache.Containers, repository.GetContainerIndicators, cache.SetContainers);

            await ExportIndicatorWordsToCache(cache.HiddenWords, repository.GetHiddenWordIndicators, cache.SetHiddenWords);

            await ExportIndicatorWordsToCache(cache.Homophones, repository.GetHomophoneIndicators, cache.SetHomophones);

            await ExportIndicatorWordsToCache(cache.LetterSelections, repository.GetLetterSelectionIndicators, cache.SetLetterSelections);

            await ExportIndicatorWordsToCache(cache.Removals, repository.GetRemovalIndicators, cache.SetRemovals);

            await ExportIndicatorWordsToCache(cache.Reversals, repository.GetReversalIndicators, cache.SetReversals);

            await ExportIndicatorWordsToCache(cache.Substitutions, repository.GetSubstitutionIndicators, cache.SetSubstitutions);

            var usualSuspects = repository.GetUsualSuspects();
            var usualSuspectsArray = usualSuspects.ToArray();
            var usualSuspectsUnion = cache.UsualSuspects.UnionBy(usualSuspectsArray, w => w.Word);
            //if size is greater, overwrite cache
            if (usualSuspectsUnion.Count() > cache.UsualSuspects.Length)
            {
                cache.SetUsualSuspects(usualSuspectsUnion.ToArray());
            }
        }

        private delegate IEnumerable<IndicatorWord> ExporterDelegate();
        private delegate void CacheSetDelegate(IndicatorWord[] words);
        private async Task ExportIndicatorWordsToCache(IndicatorWord[] cachedWords, ExporterDelegate exporter, CacheSetDelegate cacheSetter)
        {
            var indicatorWords = exporter();
            var wordsArray = indicatorWords.ToArray();
            if (wordsArray != null)
            {
                var wordUnion = cachedWords.UnionBy(wordsArray, w => w.Word);
                //if size is greater, overwrite cache
                if (wordUnion.Count() > cachedWords.Length)
                {
                    cacheSetter(wordUnion.ToArray());
                }
            }
            else
            {
                var words = cachedWords.ToArray();
            }
        }
    }
}

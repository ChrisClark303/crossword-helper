using CrosswordHelper.Data.Models;

namespace CrosswordHelper.Data
{
    public interface ICrosswordHelperRepository
    {
        IEnumerable<WordDetails> CheckWords(string[] words);
        IEnumerable<IndicatorWord> GetAnagramIndicators();
        IEnumerable<IndicatorWord> GetRemovalIndicators();
        IEnumerable<IndicatorWord> GetReversalIndicators();
        IEnumerable<IndicatorWord> GetContainerIndicators();
        IEnumerable<IndicatorWord> GetLetterSelectionIndicators();
        IEnumerable<IndicatorWord> GetHomophoneIndicators();
        IEnumerable<UsualSuspect> GetUsualSuspects();
    }
}
using CrosswordHelper.Data.Models;

namespace CrosswordHelper.Api
{
    public interface ICrosswordHelperService
    {
        IndicatorWord[] GetAnagramIndicators();
        IndicatorWord[] GetContainerIndicators();
        IndicatorWord[] GetRemovalIndicators();
        IndicatorWord[] GetReversalIndicators();
        UsualSuspect[] GetUsualSuspects();
        IndicatorWord[] GetLetterSelectionIndicators();
        IndicatorWord[] GetHomophoneIndicators();
        IndicatorWord[] GetSubstitutionIndicators();
        IndicatorWord[] GetHiddenWordIndicators();
        CrosswordHelperResult Help(string crosswordClue);
    }
}
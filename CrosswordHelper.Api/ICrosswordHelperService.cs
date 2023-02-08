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
        CrosswordHelperResult Help(string crosswordClue);
    }

    public interface ICrosswordHelperManagerService
    {
        void AddAnagramIndictor(string word);
        void AddContainerIndicator(string word);
        void AddSeparator(string word);
        void AddReversalIndicator(string word);
        void AddRemovalIndicator(string word);
    }
}
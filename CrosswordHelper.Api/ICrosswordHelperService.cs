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
        void AddAnagramIndictor(string word, string notes);
        void AddContainerIndicator(string word, string notes);
        void AddSeparator(string word, string notes);
        void AddReversalIndicator(string word, string notes);
        void AddRemovalIndicator(string word, string notes);
        void AddUsualSuspect(string word, string replacement);
    }
}
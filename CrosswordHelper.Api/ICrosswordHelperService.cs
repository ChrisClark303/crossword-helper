namespace CrosswordHelper.Api
{
    public interface ICrosswordHelperService
    {
        string[] GetAnagramIndicators();
        string[] GetContainerIndicators();
        string[] GetRemovalIndicators();
        string[] GetReversalIndicators();

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
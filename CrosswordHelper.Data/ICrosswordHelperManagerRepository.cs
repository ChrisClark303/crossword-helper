namespace CrosswordHelper.Data
{
    public interface ICrosswordHelperManagerRepository
    {
        void AddAnagramIndictor(string word);
        void AddContainerIndicator(string word);
        void AddSeparator(string word);
        void AddReversalIndicator(string word);
    }
}
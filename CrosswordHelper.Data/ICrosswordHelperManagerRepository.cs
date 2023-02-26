namespace CrosswordHelper.Data
{
    public interface ICrosswordHelperManagerRepository
    {
        void AddAnagramIndictor(string word, string notes);
        void AddContainerIndicator(string word, string notes);
        void AddSeparator(string word);
        void AddReversalIndicator(string word, string notes);
        void AddRemovalIndicator(string word, string notes);
        void AddLetterSelectionIndicator(string word, string notes);
        void AddHomophoneIndicator(string word, string notes);
        void AddAUsualSuspect(string original, params string[] replacements);
    }
}
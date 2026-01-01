namespace CrosswordHelper.Management.Api
{
    public interface ICrosswordHelperManagerService
    {
        void AddAnagramIndictor(string word, string notes);
        void AddContainerIndicator(string word, string notes);
        void AddSeparator(string word, string notes);
        void AddReversalIndicator(string word, string notes);
        void AddRemovalIndicator(string word, string notes);
        void AddLetterSelectionIndicator(string word, string notes);
        void AddHomophoneIndicator(string word, string notes);
        void AddSubstitutionIndicator(string word, string notes);
        void AddUsualSuspect(string word, string replacement);
        void AddHiddenWordIndicator(string word, string notes);
        string GetConnectionHealth();
    }
}
using CrosswordHelper.Data;

namespace CrosswordHelper.Api
{
    public class CrosswordHelperManagementService : ICrosswordHelperManagerService
    {
        private readonly ICrosswordHelperManagerRepository _helperManagerRepository;

        public CrosswordHelperManagementService(ICrosswordHelperManagerRepository helperManagerRepository)
        {
            _helperManagerRepository = helperManagerRepository;
        }

        private void SplitWordsAndExecute(string word, string notes, Action<string,string> executor)
        {
            foreach (var w in word.Split(','))
            {
                executor(w.Trim(), notes);
            }
        }

        public void AddAnagramIndictor(string word, string notes)
        {
            SplitWordsAndExecute(word, notes, _helperManagerRepository.AddAnagramIndictor);
        }

        public void AddContainerIndicator(string word, string notes)
        {
            SplitWordsAndExecute(word, notes, _helperManagerRepository.AddContainerIndicator);
        }

        public void AddReversalIndicator(string word, string notes)
        {
            SplitWordsAndExecute(word, notes, _helperManagerRepository.AddReversalIndicator);
        }

        public void AddRemovalIndicator(string word, string notes)
        {
            SplitWordsAndExecute(word, notes, _helperManagerRepository.AddRemovalIndicator);
        }

        public void AddSeparator(string word, string notes)
        {
            throw new NotImplementedException();
        }

        public void AddUsualSuspect(string word, string replacement)
        {
            _helperManagerRepository.AddAUsualSuspect(word, replacement);
        }

        public void AddLetterSelectionIndicator(string word, string notes)
        {
            SplitWordsAndExecute(word, notes, _helperManagerRepository.AddLetterSelectionIndicator);
        }

        public void AddHomophoneIndicator(string word, string notes)
        {
            SplitWordsAndExecute(word, notes, _helperManagerRepository.AddHomophoneIndicator);
        }
    }
}

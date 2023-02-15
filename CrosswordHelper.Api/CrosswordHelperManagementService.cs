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

        public void AddAnagramIndictor(string word, string notes)
        {
            _helperManagerRepository.AddAnagramIndictor(word, notes);
        }

        public void AddContainerIndicator(string word, string notes)
        {
            _helperManagerRepository.AddContainerIndicator(word, notes);
        }

        public void AddReversalIndicator(string word, string notes)
        {
            _helperManagerRepository.AddReversalIndicator(word, notes);
        }

        public void AddRemovalIndicator(string word, string notes)
        {
            _helperManagerRepository.AddRemovalIndicator(word, notes);
        }

        public void AddSeparator(string word, string notes)
        {
            throw new NotImplementedException();
        }

        public void AddUsualSuspect(string word, string replacement)
        {
            _helperManagerRepository.AddAUsualSuspect(word, replacement);
        }
    }
}

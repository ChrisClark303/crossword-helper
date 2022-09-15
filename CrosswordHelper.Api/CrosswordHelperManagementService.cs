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

        public void AddAnagramIndictor(string word)
        {
            _helperManagerRepository.AddAnagramIndictor(word);
        }

        public void AddContainerIndicator(string word)
        {
            _helperManagerRepository.AddContainerIndicator(word);
        }

        public void AddReversalIndicator(string word)
        {
            _helperManagerRepository.AddReversalIndicator(word);
        }

        public void AddSeparator(string word)
        {
            throw new NotImplementedException();
        }
    }
}

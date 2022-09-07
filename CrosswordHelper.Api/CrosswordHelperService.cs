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
            throw new NotImplementedException();
        }

        public void AddContainerIndicator(string word)
        {
            throw new NotImplementedException();
        }

        public void AddReversalIndicator(string word)
        {
            throw new NotImplementedException();
        }

        public void AddSeparator(string word)
        {
            throw new NotImplementedException();
        }
    }

    public class CrosswordHelperService : ICrosswordHelperService
    {
        private readonly ICrosswordHelperRepository _helperRepository;

        public CrosswordHelperService(ICrosswordHelperRepository helperRepository)
        {
            _helperRepository = helperRepository;
        }

        public CrosswordHelperResult Help(string crosswordClue)
        {
            return new CrosswordHelperResult(crosswordClue);
        }
    }

    public class CrosswordHelperResult
    {
        public CrosswordHelperResult(string originalClue)
        {
            OriginalClue = originalClue;
        }

        public string OriginalClue { get; }
    }

    public class WordDetails
    {
        public string OriginalWord { get; set; }
        public string[] PotentialReplacements { get; set; }
        public bool CouldBeAnagramIndicator { get; set; }
        public bool CouldBeContainerIndicator { get; set; }
        public bool CouldBeSeparator { get; set; }
    }
}

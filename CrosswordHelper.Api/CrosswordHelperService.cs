using CrosswordHelper.Data;

namespace CrosswordHelper.Api
{

    public class CrosswordHelperService : ICrosswordHelperService
    {
        private readonly ICrosswordHelperRepository _helperRepository;

        public CrosswordHelperService(ICrosswordHelperRepository helperRepository)
        {
            _helperRepository = helperRepository;
        }

        public CrosswordHelperResult Help(string crosswordClue)
        {
            IEnumerable<WordDetails> details = _helperRepository.CheckWords(crosswordClue.Split(" "));
            return new CrosswordHelperResult(crosswordClue, details);
        }

        public string[] GetAnagramIndicators()
        {
            return _helperRepository.GetAnagramIndicators().ToArray();
        }

        public string[] GetContainerIndicators()
        {
            return _helperRepository.GetContainerIndicators().ToArray();
        }

        public string[] GetRemovalIndicators()
        {
            return _helperRepository.GetRemovalIndicators().ToArray();
        }

        public string[] GetReversalIndicators()
        {
            return _helperRepository.GetReversalIndicators().ToArray();
        }
    }

    public class CrosswordHelperResult
    {
        public WordDetails[] WordDetails { get; }
        public string OriginalClue { get; }

        public CrosswordHelperResult(string originalClue, IEnumerable<WordDetails> wordDetails)
        {
            OriginalClue = originalClue;
            WordDetails = wordDetails.ToArray();
        }
    }
}

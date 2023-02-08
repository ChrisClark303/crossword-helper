using CrosswordHelper.Data;
using CrosswordHelper.Data.Models;

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

        public IndicatorWord[] GetAnagramIndicators()
        {
            return _helperRepository.GetAnagramIndicators().ToArray();
        }

        public IndicatorWord[] GetContainerIndicators()
        {
            return _helperRepository.GetContainerIndicators().ToArray();
        }

        public IndicatorWord[] GetRemovalIndicators()
        {
            return _helperRepository.GetRemovalIndicators().ToArray();
        }

        public IndicatorWord[] GetReversalIndicators()
        {
            return _helperRepository.GetReversalIndicators().ToArray();
        }

        public UsualSuspect[] GetUsualSuspects()
        {
            return _helperRepository.GetUsualSuspects().ToArray();
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

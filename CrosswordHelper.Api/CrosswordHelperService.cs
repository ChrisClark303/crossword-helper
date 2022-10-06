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

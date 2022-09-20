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
            var details = _helperRepository.CheckWords(crosswordClue.Split(" "));
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
}

using CrosswordHelper.Data;
using CrosswordHelper.Data.Models;
using System.Diagnostics;

namespace CrosswordHelper.Api
{

    public class CrosswordHelperService : ICrosswordHelperService
    {
        private readonly ICrosswordHelperRepository _helperRepository;
        private ILogger<ICrosswordHelperService> _logger;

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

        public IndicatorWord[] GetLetterSelectionIndicators()
        {
            return _helperRepository.GetLetterSelectionIndicators().ToArray();
        }

        public IndicatorWord[] GetHomophoneIndicators()
        {
            return _helperRepository.GetHomophoneIndicators().ToArray();
        }

        public IndicatorWord[] GetSubstitutionIndicators()
        {
            return _helperRepository.GetSubstitutionIndicators().ToArray();
        }

        public IndicatorWord[] GetHiddenWordIndicators()
        {
            return _helperRepository.GetHiddenWordIndicators().ToArray();
        }
    }

    public class CrosswordHelperResult
    {
        public WordDetailsResponse[] WordDetails { get; }
        public string OriginalClue { get; }

        public CrosswordHelperResult(string originalClue, IEnumerable<WordDetails> wordDetails)
        {
            OriginalClue = originalClue;
            WordDetails = wordDetails.
                Select(wd => new WordDetailsResponse
                {
                    CouldBeAnagramIndicator = wd.CouldBeAnagramIndicator,
                    CouldBeContainerIndicator = wd.CouldBeContainerIndicator,
                    CouldBeHiddenWordIndicator = wd.CouldBeHiddenWordIndicator,
                    CouldBeHomophoneIndicator = wd.CouldBeHomophoneIndicator,
                    CouldBeLetterSelectionIndicator = wd.CouldBeLetterSelectionIndicator,
                    CouldBeRemovalIndicator = wd.CouldBeRemovalIndicator,
                    CouldBeReversalIndicator = wd.CouldBeReversalIndicator,
                    CouldBeSubstitutionIndicator = wd.CouldBeSubstitutionIndicator,
                    OriginalWord = wd.OriginalWord,
                    PotentialReplacements = wd.PotentialReplacements?.Select(pr => 
                    {
                        var pieces = pr.Split("(");
                        return new ReplacementsResponse
                        {
                            ReplacementWord = pieces[0].Trim(),
                            Description = pieces.Count() > 1 ? pieces[1].Trim(' ', ')') : string.Empty
                        };
                    }).ToArray() ?? Array.Empty<ReplacementsResponse>()
                })
                .ToArray();
        }
    }
}

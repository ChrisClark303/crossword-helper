using CrosswordHelper.Data;
using CrosswordHelper.Data.Models;

namespace CrosswordHelper.Api;


public class CrosswordHelperService(ICrosswordHelperRepository helperRepository) : ICrosswordHelperService
{
    private ILogger<ICrosswordHelperService> _logger;

    public CrosswordHelperResult Help(string crosswordClue)
    {
        IEnumerable<WordDetails> details = helperRepository.CheckWords(crosswordClue.Split(" "));
        return new CrosswordHelperResult(crosswordClue, details);
    }

    public IndicatorWord[] GetAnagramIndicators()
    {
        return helperRepository.GetAnagramIndicators().ToArray();
    }

    public IndicatorWord[] GetContainerIndicators()
    {
        return helperRepository.GetContainerIndicators().ToArray();
    }

    public IndicatorWord[] GetRemovalIndicators()
    {
        return helperRepository.GetRemovalIndicators().ToArray();
    }

    public IndicatorWord[] GetReversalIndicators()
    {
        return helperRepository.GetReversalIndicators().ToArray();
    }

    public UsualSuspect[] GetUsualSuspects()
    {
        return helperRepository.GetUsualSuspects().ToArray();
    }

    public IndicatorWord[] GetLetterSelectionIndicators()
    {
        return helperRepository.GetLetterSelectionIndicators().ToArray();
    }

    public IndicatorWord[] GetHomophoneIndicators()
    {
        return helperRepository.GetHomophoneIndicators().ToArray();
    }

    public IndicatorWord[] GetSubstitutionIndicators()
    {
        return helperRepository.GetSubstitutionIndicators().ToArray();
    }

    public IndicatorWord[] GetHiddenWordIndicators()
    {
        return helperRepository.GetHiddenWordIndicators().ToArray();
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

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
    public string OriginalClue { get; }
    public WordDetailsResponse[] WordDetails { get; }

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
                PotentialReplacements = BuildReplacementsResponse(wd)
            })
            .ToArray();
    }

    private static ReplacementsResponse[] BuildReplacementsResponse(WordDetails wd)
    {
        if (wd.PotentialReplacements == null || wd.PotentialReplacements.Length == 0)
        {
            return [];
        }

        var replacementTuples = wd.PotentialReplacements.Select(pr =>
        {
            var pieces = pr.Split("(");
            var replacement = pieces[0].Trim();
            var description = pieces.Length > 1 ? pieces[1].Trim(' ', ')') : string.Empty;
            return new Tuple<string, string>(replacement, description);
        }).ToArray();

        return replacementTuples.GroupBy(t => t.Item1).Select(g => new ReplacementsResponse()
        {
            ReplacementWord = g.Key,
            Description = string.Join(", ", g.Select(t => t.Item2).Where(d => !string.IsNullOrEmpty(d)))
        }).ToArray();
    }
}

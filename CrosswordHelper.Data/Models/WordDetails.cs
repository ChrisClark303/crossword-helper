namespace CrosswordHelper.Data.Models
{
    public class WordDetails
    {
        public string? OriginalWord { get; init; }
        public string[]? PotentialReplacements { get; init; }
        public bool CouldBeAnagramIndicator { get; init; }
        public bool CouldBeContainerIndicator { get; init; }
        public bool CouldBeReversalIndicator { get; init; }
        public bool CouldBeRemovalIndicator { get;  init; }
        public bool CouldBeLetterSelectionIndicator { get; init; }
        public bool CouldBeHomophoneIndicator { get; init; }
        public bool CouldBeSeparator { get; init; }
        public bool CouldBeHiddenWordIndicator { get; init; }
        public bool CouldBeSubstitutionIndicator { get; init; }
    }

    public record UsualSuspect(string Word, string[] Replacements);

    public record IndicatorWord(string Word, string? Notes);
}

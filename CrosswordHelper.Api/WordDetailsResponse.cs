namespace CrosswordHelper.Api
{
    public class WordDetailsResponse
    {
        public string OriginalWord { get; init; }
        public ReplacementsResponse[] PotentialReplacements { get; init; }
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

    public class ReplacementsResponse
    {
        public string ReplacementWord { get; init; }
        public string Description { get; init; }
    }
}

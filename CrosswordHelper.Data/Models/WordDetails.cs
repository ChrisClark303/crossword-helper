namespace CrosswordHelper.Data.Models
{
    public class WordDetails
    {
        public string OriginalWord { get; set; }
        public string[] PotentialReplacements { get; set; }
        public bool CouldBeAnagramIndicator { get; set; }
        public bool CouldBeContainerIndicator { get; set; }
        public bool CouldBeReversalIndicator { get; set; }
        public bool CouldBeLetterSelectionIndicator { get; set; }
        public bool CouldBeHomophoneIndicator { get; set; }
        public bool CouldBeSeparator { get; set; }
    }

    public record UsualSuspect(string Word, string[] Replacements);

    public record IndicatorWord(string Word, string? Notes);
}

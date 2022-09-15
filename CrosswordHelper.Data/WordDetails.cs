namespace CrosswordHelper.Data
{
    public class WordDetails
    {
        public string OriginalWord { get; set; }
        public string[] PotentialReplacements { get; set; }
        public bool CouldBeAnagramIndicator { get; set; }
        public bool CouldBeContainerIndicator { get; set; }
        public bool CouldBeReversalIndicator { get; set; }
        public bool CouldBeSeparator { get; set; }
    }
}

namespace CrosswordHelper.Api
{
    public class CrosswordHelperService : ICrosswordHelperService
    {
        public CrosswordHelperResult Help(string crosswordClue)
        {
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

    public class WordDetails
    {
        public string OriginalWord { get; set; }
        public string[] PotentialReplacements { get; set; }
        public bool CouldBeAnagramIndicator { get; set; }
        public bool CouldBeContainerIndicator { get; set; }
    }
}

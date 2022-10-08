namespace CrosswordHelper.Data
{
    public interface ICrosswordHelperRepository
    {
        IEnumerable<WordDetails> CheckWords(string[] words);
        IEnumerable<string> GetAnagramIndicators();
        IEnumerable<string> GetRemovalIndicators();
        IEnumerable<string> GetReversalIndicators();
        IEnumerable<string> GetContainerIndicators();
    }
}
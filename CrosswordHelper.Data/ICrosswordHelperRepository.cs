namespace CrosswordHelper.Data
{
    public interface ICrosswordHelperRepository
    {
        IEnumerable<WordDetails> CheckWords(string[] words);
    }
}
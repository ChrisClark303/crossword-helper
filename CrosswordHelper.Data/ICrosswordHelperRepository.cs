namespace CrosswordHelper.Data
{
    public interface ICrosswordHelperRepository
    {
        WordDetails CheckWord(string word);
    }
}
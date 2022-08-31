namespace CrosswordHelper.Api
{
    public interface ICrosswordHelperService
    {
        CrosswordHelperResult Help(string crosswordClue);
    }
}
namespace CrosswordHelper.Data.Import
{
    public interface IUrlBuilder
    {
        string[] GetUrls(ScrapeType scrapeType);
    }
}
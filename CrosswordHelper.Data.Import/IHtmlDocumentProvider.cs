using HtmlAgilityPack;

namespace CrosswordHelper.Data.Import;

public interface IHtmlDocumentProvider
{
    Task<IEnumerable<HtmlDocument>> GetDocuments(ScrapeType scrapeType);
}

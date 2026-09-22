using HtmlAgilityPack;

namespace CrosswordHelper.Data.Import;

public abstract class HtmlDocumentProviderBase : IHtmlDocumentProvider
{
    public abstract Task<IEnumerable<HtmlDocument>> GetDocuments(ScrapeType scrapeType);

    protected HtmlDocument LoadHtmlDocument(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        return doc;
    }
}

using HtmlAgilityPack;

namespace CrosswordHelper.Data.Import;

public class CrypticsFandomDataScraper(ICrosswordHelperManagerRepository managerRepository, IHtmlDocumentProvider documentProvider)
    : DataScraperBase(managerRepository, documentProvider), ICrypticsFandomDataScraper
{
    protected override ScrapeType scrapeType => ScrapeType.CrypticsFandom;

    protected override void ScrapeWords(HtmlDocument doc, List<WordData> words)
    {
        //var text = doc.DocumentNode.InnerHtml;
        var ulNodes = doc.DocumentNode.SelectNodes("//h1/following-sibling::ul[following::h1]");
        foreach (var node in ulNodes)
        {
            var liNodes = node.SelectNodes(".//li");
            foreach (var liNode in liNodes)
            {
                var html = liNode.InnerHtml;
                var text = liNode.InnerText;
                text = text.Replace("&nbsp;", "");
                var parts = text.Split(':');
                var word = parts[0].Trim();
                var abbreviations = parts[1]
                    .Split(',')
                    .Select(s => s.Trim())
                    .ToArray();
                var wordData = new WordData
                {
                    Word = word,
                    WordType = WordType.UsualSuspect,
                    Description = "From Cryptics Fandom",
                    Substitutions = abbreviations
                };
                words.Add(wordData);
            }
        }
    }
}

public class HtmlStaticDocumentProvider(IDictionary<ScrapeType, string> staticDocumentContent) : HtmlDocumentProviderBase
{
    public override Task<IEnumerable<HtmlDocument>> GetDocuments(ScrapeType scrapeType)
    {
        var content = staticDocumentContent[scrapeType];
        var document = LoadHtmlDocument(content);

        return Task.FromResult<IEnumerable<HtmlDocument>>([document]);
    }
}

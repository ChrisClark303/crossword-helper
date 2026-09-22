using HtmlAgilityPack;
using System.Diagnostics;

namespace CrosswordHelper.Data.Import;

public class BestForPuzzlesUsualSuspectDataScraper(ICrosswordHelperManagerRepository managerRepository, IHtmlDocumentFromUrlProvider documentProvider) 
    : DataScraperBase(managerRepository, documentProvider), IBestForPuzzlesUsualSuspectDataScraper
{
    private readonly Dictionary<string, WordType> _wordTypeMaps = new()
    {
        {"anagram indicator", WordType.Anagram },
        {"bits-and-pieces indicator", WordType.UsualSuspect },
        {"bits and pieces indicator", WordType.UsualSuspect  },
        {"reversal indicator", WordType.Reversal },
        {"container-and-contents indicator", WordType.Container },
        {"container and contents indicator", WordType.Container },
        {"subtraction indicator", WordType.Removal },
        {"hidden word indicator", WordType.Hidden },
        {"NATO Phonetic Alphabet", WordType.None },
        {"Please let us know", WordType.None },
        {"Symbols for Chemical Elements", WordType.None },
        {"homophone indicators", WordType.Homophone },
        {"homophone indicator", WordType.Homophone },
        {"cryptic definition", WordType.None },
        {"Drinks", WordType.None },
        {"BIRDS", WordType.None },
        {"Flowers", WordType.None },
        {"Rivers of the British Isles", WordType.None },
        {"Islands of the British Isles", WordType.None },
        {"International Vehicle Registrations", WordType.None },
        {"Lists of Fish", WordType.None },
        { "", WordType.UsualSuspect }
    };

    protected override ScrapeType scrapeType => ScrapeType.BestForPuzzles;

    protected override void ScrapeWords(HtmlDocument doc, List<WordData> words)
    {
        HtmlNodeCollection h2Nodes = GetH2Nodes(doc);
        if (h2Nodes == null) return;

        foreach (var h2 in h2Nodes)
        {
            var word = h2.InnerText.ToLower();
            HtmlNode nextNode = h2;
            while (nextNode != null && (nextNode = nextNode.NextSibling)?.Name != "h2")
            {
                if (nextNode?.Name == "p")
                {
                    var wordData = GenerateWordDataFromNode(word, nextNode);
                    words.Add(wordData);
                }
            }
        }
    }

    private WordData GenerateWordDataFromNode(string word, HtmlNode node)
    {
        Debug.WriteLine(word);
        var description = node.InnerText;
        var anchor = node.SelectSingleNode("./a");
        var wordType = anchor?.InnerText ?? "";
        var substitutions = node.SelectNodes("./strong");
        return new WordData()
        {
            Word = word,
            WordType = _wordTypeMaps[wordType],
            Description = description,
            Substitutions = substitutions?.Select(n => n.InnerText)?.ToArray()
        };
    }

    private static HtmlNodeCollection GetH2Nodes(HtmlDocument doc)
    {
        var node = doc.DocumentNode.SelectSingleNode("//div[@class='col-md-8']");
        var h2Nodes = node.SelectNodes("//h2");
        return h2Nodes;
    }
}

public class WordData
{
    public string? Word { get; set; }
    public WordType WordType { get; set; }
    public string? Description { get; set; }
    public string[]? Substitutions { get; set; }
}

public enum WordType
{
    None,
    Anagram,
    UsualSuspect,
    Reversal,
    Removal,
    Container,
    Hidden,
    Homophone
}

public class UrlBuilder : IUrlBuilder
{
    private const string alphabet = "abcdefghijklmnopqrstuvwxyz";

    public string[] GetUrls(ScrapeType scrapeType)
    {
        return scrapeType switch
        {
            ScrapeType.BestForPuzzles => alphabet.Select(letter => $"{letter}.html").ToArray(),
            ScrapeType.CrypticsFandom => [""],
            _ => throw new ArgumentOutOfRangeException(nameof(scrapeType), scrapeType, null)
        };
    }
}

public enum ScrapeType
{
    BestForPuzzles,
    CrypticsFandom
}

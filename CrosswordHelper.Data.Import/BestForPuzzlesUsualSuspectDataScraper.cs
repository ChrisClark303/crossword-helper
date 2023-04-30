using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CrosswordHelper.Data.Import
{
    public class BestForPuzzlesUsualSuspectDataScraper
    {
        private Dictionary<string, WordType> _wordTypeMaps = new()
        {
            {"anagram indicator", WordType.Anagram  },
            {"bits-and-pieces indicator", WordType.UsualSuspect },
            {"reversal indicator", WordType.Reversal },
            {"container-and-contents indicator", WordType.Container },
            {"subtraction indicator", WordType.Removal },
            {"hidden word indicator", WordType.Hidden },
            {"NATO Phonetic Alphabet", WordType.None },
            {"Please let us know", WordType.None }
        };

        private readonly HttpClient _client;
        private readonly ICrosswordHelperManagerRepository _managerRepository;
        private readonly IUrlBuilder _urlBuilder;

        public BestForPuzzlesUsualSuspectDataScraper(HttpClient client, ICrosswordHelperManagerRepository managerRepository, IUrlBuilder urlBuilder)
        {
            _client = client;
            _managerRepository = managerRepository;
            _urlBuilder = urlBuilder;
        }

        public async Task Scrape()
        {
            var words = new List<WordData>();
            var urls = _urlBuilder.GetUrls();
            foreach (string url in urls)
            {
                HtmlDocument doc = await LoadHtmlDocumentFromUrl(url);
                HtmlNodeCollection h2Nodes = GetH2Nodes(doc);
                if (h2Nodes == null) continue;

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

            AddIndicatorsByType(words, WordType.Anagram, _managerRepository.AddAnagramIndictor);
            AddIndicatorsByType(words, WordType.Reversal, _managerRepository.AddReversalIndicator);
            AddIndicatorsByType(words, WordType.Removal, _managerRepository.AddRemovalIndicator);
            AddIndicatorsByType(words, WordType.Container, _managerRepository.AddContainerIndicator);
            AddIndicatorsByType(words, WordType.Hidden, _managerRepository.AddHiddenWordIndicator);
            AddIndicatorsByType(words, WordType.UsualSuspect, _managerRepository.AddAUsualSuspect);
        }

        private WordData GenerateWordDataFromNode(string word, HtmlNode node)
        {
            var description = node.InnerText;
            var anchor = node.SelectSingleNode("./a");
            var wordType = anchor.InnerText;
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

        private async Task<HtmlDocument> LoadHtmlDocumentFromUrl(string url)
        {
            var response = await _client.GetAsync(url);
            var page = await response.Content.ReadAsStringAsync();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);
            return doc;
        }

        private void AddIndicatorsByType(List<WordData> words, WordType wordType, Action<string,string> dataHandler)
        {
            var indicators = words.Where(w => w.WordType == wordType);
            foreach (var indicator in indicators)
            {
                dataHandler(indicator.Word!, indicator.Description!);
            }
        }

        private void AddIndicatorsByType(List<WordData> words, WordType wordType, Action<string,string[]> dataHandler)
        {
            var indicators = words.Where(w => w.WordType == wordType);
            foreach (var indicator in indicators)
            {
                if (indicator.Substitutions != null) 
                {
                    dataHandler(indicator.Word!, indicator.Substitutions);
                }
            }
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
        Hidden
    }

    public class UrlBuilder : IUrlBuilder
    {
        private const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public string[] GetUrls()
        {
            return alphabet.Select(letter => $"{letter}.html")
                .ToArray();
        }
    }
}

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
                var response = await _client.GetAsync(url);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                
                var page = await response.Content.ReadAsStringAsync();
                doc.LoadHtml(page);
                var node = doc.DocumentNode.SelectSingleNode("//div[@class='col-md-8']");
                var h2Nodes = node.SelectNodes("//h2");
                if (h2Nodes == null) continue;
                foreach (var h2 in h2Nodes)
                {
                    var word = h2.InnerText.ToLower();
                    HtmlNode nextNode = h2;
                    while (nextNode != null && (nextNode = nextNode.NextSibling)?.Name != "h2")
                    {
                        if (nextNode?.Name == "p")
                        {
                            var description = nextNode.InnerText;
                            var anchor = nextNode.SelectSingleNode("./a");
                            var wordType = anchor.InnerText;
                            words.Add(new WordData()
                            {
                                Word = word,
                                WordType = _wordTypeMaps[wordType],
                                Description = description
                            });
                        }
                    }
                }
            }

            AddIndicatorsByType(words, WordType.Anagram, (indicator) => _managerRepository.AddAnagramIndictor(indicator.Word, indicator.Description));
            AddIndicatorsByType(words, WordType.Reversal, (indicator) => _managerRepository.AddReversalIndicator(indicator.Word, indicator.Description));
            AddIndicatorsByType(words, WordType.Removal, (indicator) => _managerRepository.AddRemovalIndicator(indicator.Word, indicator.Description));

            //var anagramIndicators = words.Where(w => w.WordType == WordType.Anagram);
            //foreach (var anagramIndicator in anagramIndicators)
            //{
            //    _managerRepository.AddAnagramIndictor(anagramIndicator.Word, anagramIndicator.Description);
            //}

            //var reversalIndicators = words.Where(w => w.WordType == WordType.Reversal);
            //foreach (var reversalIndicator in reversalIndicators)
            //{
            //    _managerRepository.AddReversalIndicator(reversalIndicator.Word, reversalIndicator.Description);
            //}
        }

        private void AddIndicatorsByType(List<WordData> words, WordType wordType, Action<WordData> dataHandler)
        {
            var indicators = words.Where(w => w.WordType == wordType);
            foreach (var indicator in indicators)
            {
                dataHandler(indicator);
                //_managerRepository.AddAnagramIndictor(anagramIndicator.Word, anagramIndicator.Description);
            }
        }
    }

    public class WordData
    {
        public string? Word { get; set; }
        public WordType WordType { get; set; }
        public string? Description { get; set; }
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

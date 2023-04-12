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

        public BestForPuzzlesUsualSuspectDataScraper(HttpClient client, ICrosswordHelperManagerRepository managerRepository)
        {
            _client = client;
            _managerRepository = managerRepository;
        }

        private const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public async Task Scrape()
        {
            var words = new List<WordData>();
            foreach (char letter in alphabet)
            {
                var response = await _client.GetAsync($"{letter.ToString()}.html");
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

            var anagramIndicators = words.Where(w => w.WordType == WordType.Anagram);
            foreach (var anagramIndicator in anagramIndicators)
            {
                _managerRepository.AddAnagramIndictor(anagramIndicator.Word, anagramIndicator.Description);
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
}

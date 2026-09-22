using HtmlAgilityPack;
using System.Collections;

namespace CrosswordHelper.Data.Import;

public class HtmlDocumentFromUrlProvider(HttpClient client, IUrlBuilder urlBuilder): HtmlDocumentProviderBase, IHtmlDocumentFromUrlProvider
{
    private class HtmlDocumentProviderEnumerable : IEnumerable<HtmlDocument>
    {
        private readonly string[] _urls;
        private readonly Func<string, HtmlDocument> _documentResolver;
        public HtmlDocumentProviderEnumerable(string[] urls, Func<string, HtmlDocument> documentResolver)
        {
            _urls = urls;
            _documentResolver = documentResolver;
        }
        public IEnumerator<HtmlDocument> GetEnumerator()
        {
            return new HtmlDocumentProviderEnumerator(_urls, _documentResolver);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    private class HtmlDocumentProviderEnumerator : IEnumerator<HtmlDocument>
    {
        private Dictionary<string, HtmlDocument?> _dictionary;
        private int pointer = -1;
        private readonly Func<string, HtmlDocument> _documentResolver;

        public HtmlDocument Current
        {
            get
            {
                var element = _dictionary.ElementAt(pointer);
                var doc = element.Value;
                if (doc == null)
                {
                    doc = _dictionary[element.Key] = _documentResolver(element.Key);
                }
                return doc;
            }
        }

        object IEnumerator.Current => Current;

        public HtmlDocumentProviderEnumerator(string[] urls, Func<string, HtmlDocument> documentResolver)
        {
            _dictionary = urls.ToDictionary(url => url, _ => (HtmlDocument?)null);
            _documentResolver = documentResolver;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            pointer++;
            return pointer < _dictionary.Count;
        }

        public void Reset()
        {
            pointer = 0;
        }
    }

    public override async Task<IEnumerable<HtmlDocument>> GetDocuments(ScrapeType scrapeType)
    {
        var urls = urlBuilder.GetUrls(scrapeType);
        return new HtmlDocumentProviderEnumerable(urls, url => LoadHtmlDocumentFromUrl(url).Result);
    }

    private async Task<HtmlDocument> LoadHtmlDocumentFromUrl(string url)
    {
        var response = await client.GetAsync(url);
        var page = await response.Content.ReadAsStringAsync();
        return LoadHtmlDocument(page);
    }
}

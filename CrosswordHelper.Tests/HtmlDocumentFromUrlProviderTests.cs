using CrosswordHelper.Data.Import;
using Moq;

namespace CrosswordHelper.Tests
{
    public class HtmlDocumentFromUrlProviderTests
    {
        [Test]
        public async Task GetDocuments_Returns_HtmlDocuments_For_Urls()
        {
            var urls = new[] { "a", "b", "c" };
            var mockUrlBuilder = new Mock<IUrlBuilder>();
            mockUrlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles)).Returns(urls);

            var dict = new Dictionary<string, string>
            {
                { "a", "<html><body>Test A</body></html>" },
                { "b", "<html><body>Test B</body></html>" },
                { "c", "<html><body>Test C</body></html>" }
            };

            var stubMessageHandler = new StubMessageHandler(dict);
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://localhost")
            };
            var documentProvider = new HtmlDocumentFromUrlProvider(httpClient, mockUrlBuilder.Object);
            var docEnumerable = await documentProvider.GetDocuments(ScrapeType.BestForPuzzles);
            var documents = docEnumerable.ToList();
            Assert.That(documents, Has.Count.EqualTo(3));

            Assert.That(documents[0].DocumentNode.InnerText, Is.EqualTo("Test A"));
            Assert.That(documents[1].DocumentNode.InnerText, Is.EqualTo("Test B"));
            Assert.That(documents[2].DocumentNode.InnerText, Is.EqualTo("Test C"));
        }
    }
}
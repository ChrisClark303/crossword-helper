using CrosswordHelper.Data;
using CrosswordHelper.Data.Import;
using Moq;
using System.Diagnostics.Eventing.Reader;

namespace CrosswordHelper.Tests
{
    public class UsualSuspectDataImporterTests
    {
        [Test]
        public void SingleLine_SingleReplacementPerWord_GeneratesSingleUsualSuspect()
        {
            var mockedRepo = new Mock<ICrosswordHelperManagerRepository>();
            var usualSuspectDataImporter = new UsualSuspectDataImporter(mockedRepo.Object);

            usualSuspectDataImporter.Import(new[] { "beer,ALE" });

            mockedRepo.Verify(repo => repo.AddAUsualSuspect("beer", "ALE"));
        }

        [Test]
        public void SingleLine_MultipleWordsPerLine_GeneratesMultipleUsualSuspects()
        {
            var mockedRepo = new Mock<ICrosswordHelperManagerRepository>();
            var usualSuspectDataImporter = new UsualSuspectDataImporter(mockedRepo.Object);

            usualSuspectDataImporter.Import(new[] { "a / an,PER" });

            mockedRepo.Verify(repo => repo.AddAUsualSuspect("a", "PER"));
            mockedRepo.Verify(repo => repo.AddAUsualSuspect("an", "PER"));
        }

        [Test]
        public void SingleLine_WordIsPlural_GeneratesMultipleUsualSuspects()
        {
            var mockedRepo = new Mock<ICrosswordHelperManagerRepository>();
            var usualSuspectDataImporter = new UsualSuspectDataImporter(mockedRepo.Object);

            usualSuspectDataImporter.Import(new[] { "debt(s),IOU" });

            mockedRepo.Verify(repo => repo.AddAUsualSuspect("debt", "IOU"));
            mockedRepo.Verify(repo => repo.AddAUsualSuspect("debts", "IOU"));
        }

        [Test]
        public void SingleLine_MultipleReplacementsPerWord_GeneratesSingleUsualSuspect_WithMultiple_Replacements()
        {
            var mockedRepo = new Mock<ICrosswordHelperManagerRepository>();
            var usualSuspectDataImporter = new UsualSuspectDataImporter(mockedRepo.Object);

            usualSuspectDataImporter.Import(new[] { "fool,ASS; CLOT; NIT; TWIT" });

            mockedRepo.Verify(repo => repo.AddAUsualSuspect("fool", It.Is<string[]>(s => ArrayMatches(s, new[] { "ASS","CLOT","NIT","TWIT" }))));
        }

        private bool ArrayMatches(string[] actual, string[] expected)
        {
            var intersects = actual.Intersect(expected);
            var intersectsExactly = intersects.Count() == 4;

            return intersectsExactly;
        }

        [Test]
        public void MultipleLines_SingleReplacementPerWord_GeneratesSingleUsualSuspect()
        {
            var mockedRepo = new Mock<ICrosswordHelperManagerRepository>();
            var usualSuspectDataImporter = new UsualSuspectDataImporter(mockedRepo.Object);

            usualSuspectDataImporter.Import(new[] { "beer,ALE", "communist,RED" });

            mockedRepo.Verify(repo => repo.AddAUsualSuspect("beer", "ALE"));
            mockedRepo.Verify(repo => repo.AddAUsualSuspect("communist", "RED"));
        }
    }

    public class BestForPuzzlesUsualSuspectDataScraperTests
    {
        [Test]
        public async Task Scrape_Correctly_Extracts_AnagramIndicators()
        {
            var mockRepository = new Mock<ICrosswordHelperManagerRepository>();
            var mockUrlBuilder = new Mock<IUrlBuilder>();
            mockUrlBuilder.Setup(builder => builder.GetUrls())
                .Returns(new[] { "w.html" });

            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(httpClient, mockRepository.Object, mockUrlBuilder.Object);
            await scraper.Scrape();

            mockRepository.Verify(repo => repo.AddAnagramIndictor(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(19));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("wander", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("wandering", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("warped", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("wayward", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("weave", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("weaving", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("weird", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("wild", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("wildly", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("wilder", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("worked", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("working", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("worried", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("wound", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("woven", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("wrecked", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("writhing", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("wrong", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddAnagramIndictor("wrongly", It.IsAny<string>()));
        }

        [Test]
        public async Task Scrape_Correctly_Extracts_ReversalIndicators()
        {
            var mockRepository = new Mock<ICrosswordHelperManagerRepository>();
            var mockUrlBuilder = new Mock<IUrlBuilder>();
            mockUrlBuilder.Setup(builder => builder.GetUrls())
                .Returns(new[] { "w.html" });

            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(httpClient, mockRepository.Object, mockUrlBuilder.Object);
            await scraper.Scrape();

            mockRepository.Verify(repo => repo.AddReversalIndicator(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockRepository.Verify(repo => repo.AddReversalIndicator("westbound", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddReversalIndicator("written up", It.IsAny<string>()));
        }

        [Test]
        public async Task Scrape_Correctly_Extracts_RemovalIndicators()
        {
            var mockRepository = new Mock<ICrosswordHelperManagerRepository>();
            var mockUrlBuilder = new Mock<IUrlBuilder>();
            mockUrlBuilder.Setup(builder => builder.GetUrls())
                .Returns(new[] { "w.html" });

            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(httpClient, mockRepository.Object, mockUrlBuilder.Object);
            await scraper.Scrape();

            mockRepository.Verify(repo => repo.AddRemovalIndicator(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
            mockRepository.Verify(repo => repo.AddRemovalIndicator("wingless", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddRemovalIndicator("without end", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddRemovalIndicator("without limits", It.IsAny<string>()));
        }
    }

    public class StubMessageHandler : DelegatingHandler
    {
        private readonly string _content;

        public StubMessageHandler(string content)
        {
            _content = content;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage() { Content = new StringContent(_content) });
        }
    }
}
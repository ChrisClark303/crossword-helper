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

        [Test]
        public async Task Scrape_Correctly_Extracts_ContainerIndicators()
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

            mockRepository.Verify(repo => repo.AddContainerIndicator(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(6));
            mockRepository.Verify(repo => repo.AddContainerIndicator("within", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddContainerIndicator("without", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddContainerIndicator("wrap", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddContainerIndicator("wrapping", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddContainerIndicator("wrapped", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddContainerIndicator("wraps", It.IsAny<string>()));
        }

        [Test]
        public async Task Scrape_Correctly_Extracts_UsualSuspectIndicators()
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

            mockRepository.Verify(repo => repo.AddAUsualSuspect(It.IsAny<string>(), It.IsAny<string[]>()), Times.Exactly(31));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wales", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wander", It.Is<string[]>(s => s.Intersect(new[] { "ERR" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("watt", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wed", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wednesday", It.Is<string[]>(s => s.Intersect(new[] { "W","WED" }).Count() == 2)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("week", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("weekend", It.Is<string[]>(s => s.Intersect(new[] { "K" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("weight", It.Is<string[]>(s => s.Intersect(new[] { "W","OZ","LB","DRAM","TON" }).Count() == 5)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("welsh", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("welshman", It.Is<string[]>(s => s.Intersect(new[] { "DAI" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("west", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("western", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("west indies", It.Is<string[]>(s => s.Intersect(new[] { "WI" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("whiskey", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wicket", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wide", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("width", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wife", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wingtip", It.Is<string[]>(s => s.Intersect(new[] { "G" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("with", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wizard place", It.Is<string[]>(s => s.Intersect(new[] { "OZ" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wolfram", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("woman", It.Is<string[]>(s => s.Intersect(new[] { "HER", "SHE" }).Count() == 2)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("won", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("work", It.Is<string[]>(s => s.Intersect(new[] { "W", "OP" }).Count() == 2)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("worker", It.Is<string[]>(s => s.Intersect(new[] { "ANT", "BEE" }).Count() == 2)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wrath", It.Is<string[]>(s => s.Intersect(new[] { "IRE" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("write", It.Is<string[]>(s => s.Intersect(new[] { "PEN" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("writer", It.Is<string[]>(s => s.Intersect(new[] { "PEN" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("writing", It.Is<string[]>(s => s.Intersect(new[] { "MS" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wrong", It.Is<string[]>(s => s.Intersect(new[] { "X", "SIN", "TORT" }).Count() == 3)));
        }

        [Test]
        public async Task Scrape_Correctly_Extracts_HiddenWordIndicators()
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

            mockRepository.Verify(repo => repo.AddHiddenWordIndicator(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockRepository.Verify(repo => repo.AddHiddenWordIndicator("within", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddHiddenWordIndicator("wrapped", It.IsAny<string>()));
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
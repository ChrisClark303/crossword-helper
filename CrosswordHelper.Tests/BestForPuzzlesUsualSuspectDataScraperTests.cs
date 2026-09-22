using CrosswordHelper.Data;
using CrosswordHelper.Data.Import;
using Moq;

namespace CrosswordHelper.Tests
{
    public class BestForPuzzlesUsualSuspectDataScraperTests
    {
        [Test]
        public async Task Scrape_Correctly_Extracts_AnagramIndicators()
        {
            var mockRepository = new Mock<ICrosswordHelperManagerRepository>();

            var urlBuilder = new Mock<IUrlBuilder>();
            urlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles))
                .Returns(["w.html"]);


            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };

            var docProvider = new HtmlDocumentFromUrlProvider(httpClient, urlBuilder.Object);
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(mockRepository.Object, docProvider);
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

            var urlBuilder = new Mock<IUrlBuilder>();
            urlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles))
                .Returns(["w.html"]);


            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };

            var docProvider = new HtmlDocumentFromUrlProvider(httpClient, urlBuilder.Object);
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(mockRepository.Object, docProvider);
            await scraper.Scrape();

            mockRepository.Verify(repo => repo.AddReversalIndicator(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockRepository.Verify(repo => repo.AddReversalIndicator("westbound", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddReversalIndicator("written up", It.IsAny<string>()));
        }

        [Test]
        public async Task Scrape_Correctly_Extracts_RemovalIndicators()
        {
            var mockRepository = new Mock<ICrosswordHelperManagerRepository>();

            var urlBuilder = new Mock<IUrlBuilder>();
            urlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles))
                .Returns(["w.html"]);


            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };

            var docProvider = new HtmlDocumentFromUrlProvider(httpClient, urlBuilder.Object);
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(mockRepository.Object, docProvider);
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

            var urlBuilder = new Mock<IUrlBuilder>();
            urlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles))
                .Returns(["w.html"]);


            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };

            var docProvider = new HtmlDocumentFromUrlProvider(httpClient, urlBuilder.Object);
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(mockRepository.Object, docProvider);
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

            var urlBuilder = new Mock<IUrlBuilder>();
            urlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles))
                .Returns(["w.html"]);


            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };

            var docProvider = new HtmlDocumentFromUrlProvider(httpClient, urlBuilder.Object);
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(mockRepository.Object, docProvider);
            await scraper.Scrape();

            mockRepository.Verify(repo => repo.AddAUsualSuspect(It.IsAny<string>(), It.IsAny<string[]>()), Times.Exactly(31));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wales", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wander", It.Is<string[]>(s => s.Intersect(new[] { "ERR" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("watt", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wed", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("wednesday", It.Is<string[]>(s => s.Intersect(new[] { "W", "WED" }).Count() == 2)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("week", It.Is<string[]>(s => s.Intersect(new[] { "W" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("weekend", It.Is<string[]>(s => s.Intersect(new[] { "K" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("weight", It.Is<string[]>(s => s.Intersect(new[] { "W", "OZ", "LB", "DRAM", "TON" }).Count() == 5)));
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

            var urlBuilder = new Mock<IUrlBuilder>();
            urlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles))
                .Returns(["w.html"]);


            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };

            var docProvider = new HtmlDocumentFromUrlProvider(httpClient, urlBuilder.Object);
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(mockRepository.Object, docProvider);
            await scraper.Scrape();

            mockRepository.Verify(repo => repo.AddHiddenWordIndicator(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockRepository.Verify(repo => repo.AddHiddenWordIndicator("within", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddHiddenWordIndicator("wrapped", It.IsAny<string>()));
        }
    }

    public class CrypticFandomDataScraperTests
    {
        //[Test]
        public async Task Scrape_Correctly_Extracts_AnagramIndicators()
        {
            var mockRepository = new Mock<ICrosswordHelperManagerRepository>();

            var urlBuilder = new Mock<IUrlBuilder>();
            urlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles))
                .Returns(["w.html"]);


            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };

            var docProvider = new HtmlDocumentFromUrlProvider(httpClient, urlBuilder.Object);
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(mockRepository.Object, docProvider);
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

        //[Test]
        public async Task Scrape_Correctly_Extracts_ReversalIndicators()
        {
            var mockRepository = new Mock<ICrosswordHelperManagerRepository>();

            var urlBuilder = new Mock<IUrlBuilder>();
            urlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles))
                .Returns(["w.html"]);


            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };

            var docProvider = new HtmlDocumentFromUrlProvider(httpClient, urlBuilder.Object);
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(mockRepository.Object, docProvider);
            await scraper.Scrape();

            mockRepository.Verify(repo => repo.AddReversalIndicator(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockRepository.Verify(repo => repo.AddReversalIndicator("westbound", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddReversalIndicator("written up", It.IsAny<string>()));
        }

        //[Test]
        public async Task Scrape_Correctly_Extracts_RemovalIndicators()
        {
            var mockRepository = new Mock<ICrosswordHelperManagerRepository>();

            var urlBuilder = new Mock<IUrlBuilder>();
            urlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles))
                .Returns(["w.html"]);


            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };

            var docProvider = new HtmlDocumentFromUrlProvider(httpClient, urlBuilder.Object);
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(mockRepository.Object, docProvider);
            await scraper.Scrape();

            mockRepository.Verify(repo => repo.AddRemovalIndicator(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
            mockRepository.Verify(repo => repo.AddRemovalIndicator("wingless", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddRemovalIndicator("without end", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddRemovalIndicator("without limits", It.IsAny<string>()));
        }

        //[Test]
        public async Task Scrape_Correctly_Extracts_ContainerIndicators()
        {
            var mockRepository = new Mock<ICrosswordHelperManagerRepository>();

            var urlBuilder = new Mock<IUrlBuilder>();
            urlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles))
                .Returns(["w.html"]);


            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };

            var docProvider = new HtmlDocumentFromUrlProvider(httpClient, urlBuilder.Object);
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(mockRepository.Object, docProvider);
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

            var docProvider = new HtmlStaticDocumentProvider(new Dictionary<ScrapeType, string> { { ScrapeType.CrypticsFandom, File.ReadAllText("TestData2.html") } });
            var scraper = new CrypticsFandomDataScraper(mockRepository.Object, docProvider);
            await scraper.Scrape();

            mockRepository.Verify(repo => repo.AddAUsualSuspect("About", It.Is<string[]>(s => s.Intersect(new[] { "re", "on", "c", "ca" }).Count() == 4)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Abstainer", It.Is<string[]>(s => s.Intersect(new[] { "tt" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Account", It.Is<string[]>(s => s.Intersect(new[] { "ac" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Accountant", It.Is<string[]>(s => s.Intersect(new[] { "ca" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Ace", It.Is<string[]>(s => s.Intersect(new[] { "a" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Acre", It.Is<string[]>(s => s.Intersect(new[] { "a" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Adult", It.Is<string[]>(s => s.Intersect(new[] { "a" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Afternoon", It.Is<string[]>(s => s.Intersect(new[] { "pm" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Afterthought", It.Is<string[]>(s => s.Intersect(new[] { "ps" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Again", It.Is<string[]>(s => s.Intersect(new[] { "re" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Alien", It.Is<string[]>(s => s.Intersect(new[] { "et" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("All", It.Is<string[]>(s => s.Intersect(new[] { "a" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Bachelor", It.Is<string[]>(s => s.Intersect(new[] { "ba", "Ba" }).Count() == 2)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Base", It.Is<string[]>(s => s.Intersect(new[] { "e" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Bearing", It.Is<string[]>(s => s.Intersect(new[] { "any compass point" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Before", It.Is<string[]>(s => s.Intersect(new[] { "ere" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Beginner", It.Is<string[]>(s => s.Intersect(new[] { "L" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Bend", It.Is<string[]>(s => s.Intersect(new[] { "z", "u" }).Count() == 2)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Bible", It.Is<string[]>(s => s.Intersect(new[] { "ot", "nt", "rv" }).Count() == 3)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Big", It.Is<string[]>(s => s.Intersect(new[] { "os" }).Count() == 1)));
            mockRepository.Verify(repo => repo.AddAUsualSuspect("Bill", It.Is<string[]>(s => s.Intersect(new[] { "ac", "ad" }).Count() == 2)));
        }

        [Test]
        public async Task Scrape_Correctly_Extracts_HiddenWordIndicators()
        {
            var mockRepository = new Mock<ICrosswordHelperManagerRepository>();

            var urlBuilder = new Mock<IUrlBuilder>();
            urlBuilder.Setup(builder => builder.GetUrls(ScrapeType.BestForPuzzles))
                .Returns(["w.html"]);


            var stubMessageHandler = new StubMessageHandler(File.ReadAllText("TestData.html"));
            var httpClient = new HttpClient(stubMessageHandler)
            {
                BaseAddress = new Uri("http://127.0.0.1")
            };

            var docProvider = new HtmlDocumentFromUrlProvider(httpClient, urlBuilder.Object);
            var scraper = new BestForPuzzlesUsualSuspectDataScraper(mockRepository.Object, docProvider);
            await scraper.Scrape();

            mockRepository.Verify(repo => repo.AddHiddenWordIndicator(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockRepository.Verify(repo => repo.AddHiddenWordIndicator("within", It.IsAny<string>()));
            mockRepository.Verify(repo => repo.AddHiddenWordIndicator("wrapped", It.IsAny<string>()));
        }
    }
}
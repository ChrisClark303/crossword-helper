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
}
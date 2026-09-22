using CrosswordHelper.Data.Import;
using Microsoft.AspNetCore.Mvc;

namespace CrosswordHelper.Management.Api.Controllers
{
    [ApiController]
    public class ImportController(
        IUsualSuspectDataImporter dataImporter, 
        IBestForPuzzlesUsualSuspectDataScraper bfpScraper,
        ICrypticsFandomDataScraper crypticsFandomDataScraper,
        IDictionary<ScrapeType, string> staticDocumentContent) : ControllerBase
    {
        private readonly IUsualSuspectDataImporter _dataImporter = dataImporter;
        private readonly IBestForPuzzlesUsualSuspectDataScraper _bfpDataScraper = bfpScraper;
        private readonly ICrypticsFandomDataScraper _crypticsFandomDataScraper = crypticsFandomDataScraper;

        [HttpPost("/import/usual-suspects")]
        public IActionResult ImportUsualSuspect(IFormFile file)
        {
            var stream = file.OpenReadStream();
            var sReader = new StreamReader(stream);
            var lines = sReader.ReadAllLines().ToArray();
            _dataImporter.Import(lines);

            return Ok();
        }

        [HttpPatch("/import/best-for-puzzles")]
        public async Task<IActionResult> ImportFromBestForPuzzles()
        {
            //var bestForPuzzlesDataScraper = new BestForPuzzlesUsualSuspectDataScraper(new HttpClient()
            //{
            //    BaseAddress = new Uri("https://bestforpuzzles.com/cryptic-crossword-dictionary/")
            //}, null, new UrlBuilder());
            //await bestForPuzzlesDataScraper.Scrape();
            await _bfpDataScraper.Scrape();
            return Ok();
        }

        [HttpPatch("/import/cryptics-fandoms-abbreviations")]
        public async Task<IActionResult> ImportFromCrypticsFandom(IFormFile file)
        {
            var stream = file.OpenReadStream();
            var sReader = new StreamReader(stream);
            var fileContents = sReader.ReadToEnd();

            staticDocumentContent.Add(ScrapeType.CrypticsFandom, fileContents);

            await _crypticsFandomDataScraper.Scrape();
            return Ok();
        }
    }
}

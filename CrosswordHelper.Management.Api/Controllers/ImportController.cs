using CrosswordHelper.Data.Import;
using Microsoft.AspNetCore.Mvc;

namespace CrosswordHelper.Management.Api.Controllers
{
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IUsualSuspectDataImporter _dataImporter;
        private readonly IBestForPuzzlesUsualSuspectDataScraper _dataScraper;

        public ImportController(IUsualSuspectDataImporter dataImporter, IBestForPuzzlesUsualSuspectDataScraper scraper)
        {
            _dataImporter = dataImporter;
            _dataScraper = scraper;
        }

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
            await _dataScraper.Scrape();
            return Ok();
        }
    }
}

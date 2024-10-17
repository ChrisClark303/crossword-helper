using CrosswordHelper.Data.Import;
using Microsoft.AspNetCore.Mvc;

namespace CrosswordHelper.Api.Controllers
{
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IUsualSuspectDataImporter _dataImporter;

        public ImportController(IUsualSuspectDataImporter dataImporter)
        {
            _dataImporter = dataImporter;
        }

        [HttpPost("/import/usual-suspects")]
        public IActionResult ImportUsualSuspect(IFormFile file)
        {
            var stream = file.OpenReadStream();
            StreamReader sReader = new StreamReader(stream);
            var lines = sReader.ReadAllLines().ToArray();
            _dataImporter.Import(lines);

            return Ok();
        }

        [HttpPatch("/import/best-for-puzzles")]
        public async Task<IActionResult> ImportFromBestForPuzzles()
        {
            var bestForPuzzlesDataScraper = new BestForPuzzlesUsualSuspectDataScraper(new HttpClient()
            {
                BaseAddress = new Uri("https://bestforpuzzles.com/cryptic-crossword-dictionary/")
            }, null, new UrlBuilder());
            await bestForPuzzlesDataScraper.Scrape();
            return Ok();
        }
    }
}

using CrosswordHelper.Api;
using CrosswordHelper.Data.Import;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrosswordHelper.Api.Controllers
{
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly ICrosswordHelperManagerService _helperService;

        public ManagementController(ICrosswordHelperManagerService helperService)
        {
            _helperService = helperService;
        }

        [HttpPost("/help/anagram-indicators/{word}")]
        public IActionResult AddAnagramIndicator(string word, string notes = "")
        {
            _helperService.AddAnagramIndictor(word, notes);
            return Ok();
        }

        [HttpPost("/help/container-indicators/{word}")]
        public IActionResult AddContainerIndicator(string word, string notes = "")
        {
            _helperService.AddContainerIndicator(word, notes);
            return Ok();
        }

        [HttpPost("/help/reversal-indicators/{word}")]
        public IActionResult AddReversalIndicator(string word, string notes = "")
        {
            _helperService.AddReversalIndicator(word, notes);
            return Ok();
        }

        [HttpPost("/help/removal-indicators/{word}")]
        public IActionResult AddRemovalIndicator(string word, string notes = "")
        {
            _helperService.AddRemovalIndicator(word, notes);
            return Ok();
        }

        [HttpPost("/help/homophone-indicators/{word}")]
        public IActionResult AddHomophoneIndicator(string word, string notes = "")
        {
            _helperService.AddHomophoneIndicator(word, notes);
            return Ok();
        }

        [HttpPost("/help/letter-selection-indicators/{word}")]
        public IActionResult AddLetterSelectionIndicator(string word, string notes = "")
        {
            _helperService.AddLetterSelectionIndicator(word, notes);
            return Ok();
        }
    }

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
    }
}

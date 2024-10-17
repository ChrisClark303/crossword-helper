using CrosswordHelper.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrosswordHelper.Api.Controllers
{
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly ICrosswordHelperManagerService _helperService;
        private readonly ILogger<ManagementController> _logger;

        public ManagementController(ICrosswordHelperManagerService helperService, ILogger<ManagementController> logger)
        {
            _helperService = helperService;
            _logger = logger;
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

        [HttpPost("/help/substitution-indicators/{word}")]
        public IActionResult AddSubstitutionIndicator(string word, string notes = "")
        {
            _helperService.AddSubstitutionIndicator(word, notes);
            return Ok();
        }

        [HttpPost("/help/hidden-word-indicators/{word}")]
        public IActionResult AddHiddenWordIndicator(string word, string notes = "")
        {
            _helperService.AddHiddenWordIndicator(word, notes);
            return Ok();
        }

        [HttpGet("/health/db")]
        public IActionResult TestConnection()
        {
            string connHealth = _helperService.GetConnectionHealth();
            if (string.IsNullOrEmpty(connHealth))
            {
                _logger.LogInformation("Connection test successful.");
            }
            else 
            {
                _logger.LogError($"Error testing connection: {connHealth}");
            }
            return Ok(connHealth);
        }
    }
}

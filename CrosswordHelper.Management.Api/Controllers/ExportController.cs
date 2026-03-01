using CrosswordHelper.Data.Export;
using Microsoft.AspNetCore.Mvc;

namespace CrosswordHelper.Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController(ICrosswordDataExtractionService service) : ControllerBase
    {
        [HttpPatch]
        public async Task<IActionResult> Export()
        {
            await service.ExportCrosswordDataToCache();

            return Ok();
        }
    }
}

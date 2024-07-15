using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Models.NewFolder;
using Models.View;

namespace Excel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactCORS")]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelService _excelService;

        public ExcelController(IExcelService excelService) {
            _excelService = excelService;
        }
        [HttpGet("GetAllExcelData")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExcelView>> GetAllLatestVersionData()
        {
            var datas = await _excelService.FetchAllExcelData();
            if (datas != null)
            {
                return Ok(datas);
            }
            return BadRequest("Unable to fetch datas");

        }
        [HttpPost("AddExcelData")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddData(List<ExcelRequest> excelRequests)
        {
            if (await _excelService.Add(excelRequests))
            {
                return Ok("User");
            }
            else
            {
                return BadRequest("Failed to add excel data");
            }
        }
        [HttpPost("NewAddExcelData")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewAddData()
        {
            var httpRequest = HttpContext.Request;
            var result= httpRequest.Form.Files[0];
            if (await _excelService.NewAdd(result))
            {
                return Ok("User");
            }
            else
            {
                return BadRequest("Failed to add excel data");
            }
        }
    }
}

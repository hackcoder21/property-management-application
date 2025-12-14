using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.API.Models.DTO;
using PropertyManagement.API.Services;

namespace PropertyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpPost("{userId:Guid}/{format}")]
        public async Task<IActionResult> GenerateReport(Guid userId, string format)
        {
            var result = await reportService.GeneratePropertyPortfolioReport(userId, format);

            return File(result.Content, result.ContentType, result.FileName);
        }
    }
}
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

        [HttpPost]
        [Route("{userId:Guid}")]
        public async Task<IActionResult> GenerateReport([FromRoute] Guid userId)
        {
            var contentBytes = await reportService.GeneratePropertyPortfolioReport(userId);

            var fileName = $"PropertyPortfolio_{DateTime.Now:yyyyMMdd}.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(contentBytes, contentType, fileName);
        }
    }
}
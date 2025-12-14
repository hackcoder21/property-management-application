using PropertyManagement.API.Models.DTO;

namespace PropertyManagement.API.Services
{
    public interface IReportService
    {
        Task<ReportFileResult> GeneratePropertyPortfolioReport(Guid userId, string format);
    }
}
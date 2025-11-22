namespace PropertyManagement.API.Services
{
    public interface IReportService
    {
        Task<byte[]> GeneratePropertyPortfolioReport(Guid userId);
    }
}
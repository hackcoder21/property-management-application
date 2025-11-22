using PropertyManagement.API.Repositories;

namespace PropertyManagement.API.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository reportRepository;
        private readonly ICloudStorageService cloudStorageService;

        public ReportService(IReportRepository reportRepository, ICloudStorageService cloudStorageService)
        {
            this.reportRepository = reportRepository;
            this.cloudStorageService = cloudStorageService;
        }

        public Task<byte[]> GeneratePropertyPortfolioReport(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
using OfficeOpenXml;
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

        public async Task<byte[]> GeneratePropertyPortfolioReport(Guid userId)
        {
            // Download template
            const string TemplatePath = "templates/ReportTemplate.xltx";
            
            var templateStream = await cloudStorageService.DownloadFileAsync(TemplatePath);

            templateStream.Position = 0;

            // Create Excel package using stream
            using var package = new ExcelPackage(templateStream);

            // Create report method
            CreateReport(package, userId);

            // Get generated file as byte array
            byte[] fileBytes = package.GetAsByteArray();

            // Set file name
            var fileName = $"PropertyPortfolioReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            // Upload file to Cloudinary
            using var uploadStream = new MemoryStream(fileBytes);
            
            IFormFile formFile = new FormFile(uploadStream, 0, uploadStream.Length, fileName, fileName);

            var uploadedPublicId = await cloudStorageService.UploadFileAsync(formFile, "uploads");

            // Return bytes
            return fileBytes;
        }

        private void CreateReport(ExcelPackage package, Guid userId)
        {
            // Select raw worksheets
            var inputRawSheet = package.Workbook.Worksheets["INPUT_Raw"];
            var inputPortfolioRawSheet = package.Workbook.Worksheets["Portfolio_Raw"];
            var inputPropertyRawSheet = package.Workbook.Worksheets["Property_Raw"];

            // Select worksheets
            var inputPortfolioSheet = package.Workbook.Worksheets["Portfolio"];
            var inputPropertySheet = package.Workbook.Worksheets["Property"];

            // Hide raw sheets
            foreach (var ws in package.Workbook.Worksheets)
            {
                inputPortfolioSheet.Select();

                if (ws.Name.EndsWith("_Raw", StringComparison.OrdinalIgnoreCase))
                {
                    ws.Hidden = eWorkSheetHidden.Hidden;
                }
            }
        }
    }
}
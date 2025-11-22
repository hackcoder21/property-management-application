using OfficeOpenXml;
using PropertyManagement.API.Repositories;
using System.Data;

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
            // Variables
            string spPortfolio = "dbo.sp_GetPortfolioData";
            string spProperty = "dbo.sp_GetPropertyData";

            // Select raw worksheets
            var inputRawSheet = package.Workbook.Worksheets["INPUT_Raw"];
            var portfolioRawSheet = package.Workbook.Worksheets["Portfolio_Raw"];
            var propertyRawSheet = package.Workbook.Worksheets["Property_Raw"];

            // Select worksheets
            var portfolioSheet = package.Workbook.Worksheets["Portfolio"];
            var propertySheet = package.Workbook.Worksheets["Property"];

            // Hide raw sheets
            foreach (var ws in package.Workbook.Worksheets)
            {
                portfolioSheet.Select();

                if (ws.Name.EndsWith("_Raw", StringComparison.OrdinalIgnoreCase))
                {
                    ws.Hidden = eWorkSheetHidden.Hidden;
                }
            }

            // Populate Portfolio data
            PopulatePortfolioData(package.Workbook, portfolioRawSheet, portfolioSheet, spPortfolio, userId);

            // Populate Property data
            PopulatePropertyData(package.Workbook, propertyRawSheet, propertySheet, spProperty, userId);
        }

        private void PopulatePortfolioData(ExcelWorkbook workbook, ExcelWorksheet portfolioRawSheet, ExcelWorksheet portfolioSheet, string spPortfolio, Guid userId)
        {
            try
            {
                using var ds = reportRepository.GetDataSet(spPortfolio, userId);
                var dt1 = ds.Tables[0];
                var dt2 = ds.Tables[1];

                // Portfolio KPIs
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    var data = dt1.AsEnumerable().Select(row => dt1.Columns.Cast<DataColumn>().Select(col => row[col]).ToArray()).ToArray();

                    var startRow = 2;
                    var startCol = 1;
                    portfolioRawSheet.Cells[startRow, startCol].LoadFromArrays(data);
                }

                // Portfolio Chart
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    var data = dt2.AsEnumerable().Select(row => dt2.Columns.Cast<DataColumn>().Select(col => row[col]).ToArray()).ToArray();

                    var startRow = 2;
                    var startCol = 4;
                    portfolioRawSheet.Cells[startRow, startCol].LoadFromArrays(data);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void PopulatePropertyData(ExcelWorkbook workbook, ExcelWorksheet propertyRawSheet, ExcelWorksheet propertySheet, string spProperty, Guid userId)
        {
            try
            {
                using var dt = reportRepository.GetDataTable(spProperty, userId);

                // Property data
                if (dt != null && dt.Rows.Count > 0)
                {
                    var data = dt.AsEnumerable().Select(row => dt.Columns.Cast<DataColumn>().Select(col => row[col]).ToArray()).ToArray();

                    var startRow = 2;
                    var startCol = 1;
                    propertyRawSheet.Cells[startRow, startCol].LoadFromArrays(data);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
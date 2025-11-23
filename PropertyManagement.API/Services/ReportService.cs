using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
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
                // Get data set
                using var ds = reportRepository.GetDataSet(spPortfolio, userId);
                var dt1 = ds.Tables[0];
                var dt2 = ds.Tables[1];

                // Portfolio KPIs
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    // Add data to raw sheet
                    var data = dt1.AsEnumerable().Select(row => dt1.Columns.Cast<DataColumn>().Select(col => row[col]).ToArray()).ToArray();

                    var startRow = 2;
                    var startCol = 1;
                    portfolioRawSheet.Cells[startRow, startCol].LoadFromArrays(data);

                    // Add named ranges to values
                    var keyColumn = 1;
                    var valColumn = 2;

                    for (var i = 0; i < data.Length; i++)
                    {
                        var currentRow = startRow + i;
                        var rawKey = portfolioRawSheet.Cells[currentRow, keyColumn].Text;
                        var key = rawKey.Trim();
                        var valueRangeName = "O_" + key;

                        if (!workbook.Names.Any(n => n.Name == valueRangeName))
                        {
                            workbook.Names.Add(valueRangeName, portfolioRawSheet.Cells[currentRow, valColumn]);
                        }
                    }

                    // Add data to main sheet
                    FormatAndPasteValues(workbook, portfolioRawSheet, "O_portfolioValue", portfolioSheet, "Portfolio_PortfolioValue", "numeric");
                    FormatAndPasteValues(workbook, portfolioRawSheet, "O_totalProperties", portfolioSheet, "Portfolio_TotalProperties", "numeric");
                    FormatAndPasteValues(workbook, portfolioRawSheet, "O_totalCarpetArea", portfolioSheet, "Portfolio_TotalCarpetArea", "numeric");
                    FormatAndPasteValues(workbook, portfolioRawSheet, "O_averagePropertyValue", portfolioSheet, "Portfolio_AvgPropertyValue", "numeric");
                    FormatAndPasteValues(workbook, portfolioRawSheet, "O_averagePricePerSqft", portfolioSheet, "Portfolio_AvgPricePerSqft", "numeric");
                    FormatAndPasteValues(workbook, portfolioRawSheet, "O_oldestYear", portfolioSheet, "Portfolio_OldestProperty", "string");
                    FormatAndPasteValues(workbook, portfolioRawSheet, "O_newestYear", portfolioSheet, "Portfolio_NewestProperty", "string");
                    FormatAndPasteValues(workbook, portfolioRawSheet, "O_owner", portfolioSheet, "Portfolio_Owner", "string");
                    FormatAndPasteValues(workbook, portfolioRawSheet, "O_email", portfolioSheet, "Portfolio_Email", "string");
                    FormatAndPasteValues(workbook, portfolioRawSheet, "O_description", portfolioSheet, "Portfolio_Description", "string");
                }

                // Portfolio Chart
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    // Add data to raw sheet
                    var data = dt2.AsEnumerable().Select(row => dt2.Columns.Cast<DataColumn>().Select(col => row[col]).ToArray()).ToArray();

                    var startRow = 2;
                    var startCol = 4;
                    portfolioRawSheet.Cells[startRow, startCol].LoadFromArrays(data);

                    // Dynamically populate chart
                    var endRow = startRow + dt2.Rows.Count - 1;
                    var sheetName = portfolioRawSheet.Name;

                    var categoryRange = string.Format("'{0}'!$E${1}:$E${2}", sheetName, startRow, endRow);
                    var percentageRange = string.Format("'{0}'!$G${1}:$G${2}", sheetName, startRow, endRow);

                    var chart = portfolioSheet.Drawings["PortfolioChart"] as ExcelPieChart;
                    ExcelPieChartSerie? series = null;

                    if (chart!.Series.Count > 0)
                    {
                        series = chart.Series[0];
                        series.XSeries = categoryRange;
                        series.Series = percentageRange;
                    }
                    else
                    {
                        series = chart.Series.Add(percentageRange, categoryRange);
                    }
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
                // Get data table
                using var dt = reportRepository.GetDataTable(spProperty, userId);

                // Add data to raw sheet
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

        private void FormatAndPasteValues(ExcelWorkbook workbook, ExcelWorksheet rawSheet, string rawRangeName, ExcelWorksheet destSheet, string destRangeName, string type)
        {
            try
            {
                var valueSourceRange = 
                    workbook.Names.ContainsKey(rawRangeName) 
                    ? workbook.Names[rawRangeName] 
                    : rawSheet.Names.ContainsKey(rawRangeName)
                    ? rawSheet.Names[rawRangeName]
                    : null;
                var valueSourceCell = valueSourceRange.Worksheet.Cells[valueSourceRange.Address];
                var valueSource = valueSourceCell.Value;

                var valueTargetRange =
                    workbook.Names.ContainsKey(destRangeName)
                    ? workbook.Names[destRangeName]
                    : destSheet.Names.ContainsKey(destRangeName)
                    ? destSheet.Names[destRangeName]
                    : null;
                var valueTargetCell = valueTargetRange.Worksheet.Cells[valueTargetRange.Address];

                if (valueSourceCell.Value == null || string.IsNullOrWhiteSpace(valueSourceCell.Value.ToString()))
                {
                    valueTargetCell.Value = "-";
                    return;
                }

                switch (type)
                {
                    case "string":
                        valueTargetCell.Value = valueSource.ToString();
                        break;

                    case "numeric":
                        if (double.TryParse(valueSource.ToString(), out var numericValue))
                        {
                            valueTargetCell.Value = numericValue;
                            valueTargetCell.Style.Numberformat.Format = "#,##0";
                        }
                        break;

                    case "decimal":
                        if (double.TryParse(valueSource.ToString(), out var decimalValue))
                        {
                            valueTargetCell.Value = Math.Round(decimalValue, 2, MidpointRounding.AwayFromZero);
                            valueTargetCell.Style.Numberformat.Format = "#,##0.00";
                        }
                        break;

                    case "percentage":
                        if (double.TryParse(valueSource.ToString(), out var percentValue))
                        {
                            valueTargetCell.Value = percentValue;
                            valueTargetCell.Style.Numberformat.Format = "0.0%";
                        }
                        break;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
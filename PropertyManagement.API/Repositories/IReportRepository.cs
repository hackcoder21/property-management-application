using System.Data;

namespace PropertyManagement.API.Repositories
{
    public interface IReportRepository
    {
        DataTable GetDataTable(string sp, string userId);
        DataSet GetDataSet(string sp, string userId);
    }
}
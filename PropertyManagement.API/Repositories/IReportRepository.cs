using System.Data;

namespace PropertyManagement.API.Repositories
{
    public interface IReportRepository
    {
        DataTable GetDataTable(string sp, Guid userId);
        DataSet GetDataSet(string sp, Guid userId);
    }
}
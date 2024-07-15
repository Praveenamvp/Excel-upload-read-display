
using DataLayer.Interfaces;
using Models.Entity;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Models.View;
using Mapper.Interfaces;
using Mapper.Implementations;

namespace DataLayer.Implementations
{
    public class ExcelRepo : IExceRepo
    {
        private readonly IConnection _connection;
        private readonly IDataLayerMapper _dataLayerMapper;

        public ExcelRepo( IConnection connection,IDataLayerMapper dataLayerMapper)
        {
            _connection = connection;
            _dataLayerMapper= dataLayerMapper;
        }
    

        public async Task<List<ExcelView>> GetAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("FetchAllExcelData", _connection.GetConnection());
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            DataTable table = new DataTable();
            adapter.Fill(table);
            List<ExcelView> excelViews = new List<ExcelView>();
            foreach (DataRow row in table.Rows)
            {
                excelViews.Add(await _dataLayerMapper.FetchUserDetails(row));

            }
            return excelViews;
        }
    }
}

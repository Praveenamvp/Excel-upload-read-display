using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Models.NewFolder;
using Models.View;
using OfficeOpenXml;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using OfficeOpenXml;



namespace BusinessLayer.Implementations
{
    public class ExcelService : IExcelService
    {
        private readonly IExceRepo _exceRepo;
        private readonly IConnection _connection;

        public ExcelService(IExceRepo exceRepo, IConnection connection) {
            _exceRepo = exceRepo;
            _connection = connection;
        }

        public async Task<bool> Add(List<ExcelRequest> excelRequest)
        {
            string s = @"Data Source=KANINI-LTP-625\SQLEXPRESS;Integrated Security=true;Initial Catalog=Excel";
            DataTable dataTable = CreateDataTable(excelRequest);
            SqlConnection connection = new SqlConnection(s);
            connection.Open();
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection);
            sqlBulkCopy.DestinationTableName = "[data].[ExcelDatas]";
            sqlBulkCopy.ColumnMappings.Add("UID", "UID");
            sqlBulkCopy.ColumnMappings.Add("FeatureID", "FeatureID");
            sqlBulkCopy.ColumnMappings.Add("Name", "Name");
            sqlBulkCopy.ColumnMappings.Add("Description", "Description");
            sqlBulkCopy.ColumnMappings.Add("VersionNo", "VersionNo");
            sqlBulkCopy.BatchSize = 10;
            sqlBulkCopy.WriteToServer(dataTable);
            return true;
        }

        public async Task<bool> NewAdd(IFormFile excelFile)
        {
            string s = @"Data Source=KANINI-LTP-625\SQLEXPRESS;Integrated Security=true;Initial Catalog=Excel";


            if (excelFile != null && excelFile.Length > 0)
            {
                DataTable dataTable = NewCreateDataTable(excelFile);
                SqlConnection connection = new SqlConnection(s);

                connection.Open();
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection);
                sqlBulkCopy.DestinationTableName = "[data].[ExcelDatas]";
                sqlBulkCopy.ColumnMappings.Add("UID", "UID");
                sqlBulkCopy.ColumnMappings.Add("FeatureID", "FeatureID");
                sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                sqlBulkCopy.ColumnMappings.Add("VersionNo", "VersionNo");
                sqlBulkCopy.BatchSize = 10;
                sqlBulkCopy.WriteToServer(dataTable);
                return true;


            }
            else
            {
                return false;
            }
        }

    
        public DataTable CreateDataTable(List<ExcelRequest> excelRequest)
        {
            DataTable dataTable= new DataTable();
            dataTable.Columns.Add("UID", typeof(Guid));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("FeatureID", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("VersionNo", typeof(int));
            foreach(var list in excelRequest)
            {
                DataRow dataRow= dataTable.NewRow();
                dataRow["FeatureID"] = list.FeatureID;
                dataRow["UID"]=Guid.NewGuid().ToString();
                dataRow["FeatureID"] = list.FeatureID;
                dataRow["Name"] = list.Name;
                dataRow["Description"] = list.Description;
                dataRow["VersionNo"] = list.VersionNo;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }
        public DataTable NewCreateDataTable(IFormFile excelFile)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("UID", typeof(Guid));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("FeatureID", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("VersionNo", typeof(int));

            var package = new ExcelPackage(excelFile.OpenReadStream());
            
                var worksheet = package.Workbook.Worksheets[0]; 

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {

                    DataRow dataRow = dataTable.NewRow();
                    dataRow["UID"] = Guid.NewGuid();
                    dataRow["FeatureID"] = worksheet.Cells[row, 1].Value?.ToString(); 
                    dataRow["Name"] = worksheet.Cells[row, 2].Value?.ToString(); 
                    dataRow["Description"] = worksheet.Cells[row, 3].Value?.ToString(); 
                    dataRow["VersionNo"] = Convert.ToInt32(worksheet.Cells[row, 4].Value);
                    dataTable.Rows.Add(dataRow);
                }
            
            return dataTable;
        }

        public Task<List<ExcelView>> FetchAllExcelData()
        {
            return _exceRepo.GetAll();
        }
      
    }
}

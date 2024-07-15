using Mapper.Interfaces;
using Models.View;
using System.Data;

namespace Mapper.Implementations
{
    public class DataLayerMapper : IDataLayerMapper
    {
        public async Task<ExcelView> FetchUserDetails(DataRow row)
        {
            ExcelView excelView = new ExcelView();
            excelView.UID = Guid.Parse(row[0].ToString());
            excelView.FeatureID = row[1].ToString();
            excelView.Name = row[2].ToString();
            excelView.Description = row[3].ToString();
            excelView.VersionNo = int.Parse(row[4].ToString());
            return excelView;

        }
    }
}

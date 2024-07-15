using Models.View;
using System.Data;


namespace Mapper.Interfaces
{
    public interface IDataLayerMapper
    {
        public Task<ExcelView> FetchUserDetails(DataRow row);

    }
}

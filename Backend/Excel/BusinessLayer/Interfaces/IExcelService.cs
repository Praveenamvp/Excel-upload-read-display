using Microsoft.AspNetCore.Http;
using Models.NewFolder;
using Models.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IExcelService
    {
        Task<List<ExcelView>> FetchAllExcelData();
        Task<bool> Add(List<ExcelRequest> excelRequest);
        Task<bool> NewAdd(IFormFile excelRequest);
    }
}

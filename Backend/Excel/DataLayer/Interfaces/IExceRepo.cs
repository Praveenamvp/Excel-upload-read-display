

using Models.Entity;
using Models.View;

namespace DataLayer.Interfaces
{
    public interface IExceRepo
    {
        public Task<List<ExcelView>> GetAll();

    }
}

using System.Linq;
using eCar.Applicaton.Models.Service.Entities;

namespace eCar.Applicaton.Models.Service.Interfaces
{
    public interface IAutoService:IObjectService<Auto>
    {
        IQueryable<Auto> BrowseDepartmentCategory(int? categoryId, int? departmentId);
    }
}

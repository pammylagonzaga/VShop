using VShop.Web.Models;

namespace VShop.Web.Service.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategories();
    }
}

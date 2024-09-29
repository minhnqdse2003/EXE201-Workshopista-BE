using Service.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.ICategory
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseModel>> GetCategories();
        Task<CategoryResponseModel> GetCategory(Guid id);
        Task CreateCategory(CategoryRequestModel model);
        Task UpdateCategory(Guid id, CategoryRequestModel model);
        Task SoftRemove(Guid id, string status);
    }
}

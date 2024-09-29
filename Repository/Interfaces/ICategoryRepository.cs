using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetAllCategories();
        Task<List<Category>> GetActiveCategories();
        Task<Category?> GetCategoryById(Guid id);
    }
}

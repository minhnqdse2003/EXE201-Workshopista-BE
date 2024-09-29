using Microsoft.EntityFrameworkCore;
using Repository.Consts;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetActiveCategories()
        {
            return await _context.Categories.Where(c => c.Status.Equals(StatusConst.Active)).ToListAsync();
        }

        public async Task<Category?> GetCategoryById(Guid id)
        {
            return await _context.Categories.Where(c => c.CategoryId == id).FirstOrDefaultAsync();
        }
    }
}

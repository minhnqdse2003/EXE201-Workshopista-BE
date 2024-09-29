using AutoMapper;
using Repository.Consts;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces.ICategory;
using Service.Models.Category;
using Service.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponseModel>> GetCategories()
        {
            var result = await _unitOfWork.Categories.GetAllCategories();
            return result.Select(c => new CategoryResponseModel
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Slug = c.Slug,
                Description = c.Description
            }).ToList();
        }

        public async Task<CategoryResponseModel> GetCategory(Guid id)
        {
            var result = await _unitOfWork.Categories.GetCategoryById(id);
            return new CategoryResponseModel
            {
                CategoryId = result.CategoryId,
                Name = result.Name,
                Slug = result.Slug,
                Description = result.Description
            };
        }

        public async Task CreateCategory(CategoryRequestModel model)
        {
            var categoryList = await _unitOfWork.Categories.GetActiveCategories();
            var nameList = categoryList.Select(c => c.Name.ToLower()).ToList();
            if (nameList.Contains(model.Name.ToLower()))
            {
                throw new CustomException("The input name is duplicated!");
            }
            Category newCategory = _mapper.Map<Category>(model);
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.CreatedAt = DateTime.Now;
            newCategory.Status = StatusConst.Active;
            await _unitOfWork.Categories.Add(newCategory);
        }

        public async Task UpdateCategory(Guid id, CategoryRequestModel model)
        {
            var category = await _unitOfWork.Categories.GetCategoryById(id);
            if (category == null)
            {
                throw new CustomException("The category is not existed!");
            }
            _mapper.Map(model, category);
            await _unitOfWork.Categories.Update(category);
        }

        public async Task SoftRemove(Guid id, string status)
        {
            var category = await _unitOfWork.Categories.GetCategoryById(id);
            if (category == null)
            {
                throw new CustomException("The category is not existed!");
            }
            category.Status = status;
            await _unitOfWork.Categories.Update(category);
        }
    }
}

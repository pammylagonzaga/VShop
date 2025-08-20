using AutoMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories;

namespace VShop.ProductApi.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categoriesEntity = await _categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        {
            var categoriesEntity = await _categoryRepository.GetCategoriesProducts();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }
        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var categoriesEntity = await _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDTO>(categoriesEntity);
        }
        public async Task AddCategory(CategoryDTO categoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.Create(categoryEntity);
            categoryDto.CategoryId = categoryEntity.CategoryId;
        }
        public async Task UpdateCategory(CategoryDTO categoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.Update(categoryEntity);
        }
        public async Task RemoveCategory(int id)
        {
            var categoryEntity = _categoryRepository.GetById(id).Result;
            await _categoryRepository.Delete(categoryEntity.CategoryId);
        }
    }
}

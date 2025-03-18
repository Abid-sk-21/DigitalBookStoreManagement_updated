using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Repository;

namespace DigitalBookStoreManagement.Service
{
    public class CategoryService : ICategoryService

    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _repository.GetAllCategoriesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(string categoryId)
        {
            return await _repository.GetCategoryByIdAsync(categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _repository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _repository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(string categoryId)
        {
            await _repository.DeleteCategoryAsync(categoryId);
        }
    }

}

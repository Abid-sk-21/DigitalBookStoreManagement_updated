using DigitalBookStoreManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookStoreManagement.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreDBContext _context;

        public CategoryRepository(BookStoreDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(string categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(category.CategoryID);

            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {category.CategoryID} not found.");
            }

            _context.Entry(existingCategory).State = EntityState.Detached;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(string categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}

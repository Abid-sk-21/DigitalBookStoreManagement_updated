using DigitalBookStoreManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookStoreManagement.Repository
{
    public class BookManagementRepository : IBookManagementRepository
    {
        private readonly BookStoreDBContext _context;

        public BookManagementRepository(BookStoreDBContext context)
        {
            _context = context;
        }

        // **************** BOOK OPERATIONS ****************

        public async Task<IEnumerable<BookManagement>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ToListAsync();
        }

        public async Task<BookManagement?> GetBookByIdAsync(int bookId)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BookID == bookId);
        }

        public async Task<IEnumerable<BookManagement>> SearchBooksByTitleAsync(string title)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.Title.Contains(title))
                .ToListAsync();
        }

        public async Task<IEnumerable<BookManagement>> GetBooksByAuthorNameAsync(string authorName)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.Author.AuthorName == authorName)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookManagement>> GetBooksByCategoryNameAsync(string categoryName)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.Category.CategoryName == categoryName)
                .ToListAsync();
        }

        public async Task<BookManagement> AddBookAsync(BookManagement book)
        {
            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorName == book.Author.AuthorName);
            if (existingAuthor != null)
            {
                book.AuthorID = existingAuthor.AuthorID;
                book.Author = null;
            }

            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == book.Category.CategoryName);
            if (existingCategory != null)
            {
                book.CategoryID = existingCategory.CategoryID;
                book.Category = null;
            }

            _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task UpdateBookAsync(BookManagement book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

            }
        }
    }
}

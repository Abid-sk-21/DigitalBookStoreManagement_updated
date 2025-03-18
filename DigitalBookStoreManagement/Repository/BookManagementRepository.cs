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

        public async Task AddBookAsync(BookManagement book)
        {
            _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBookAsync(BookManagement book)
        {
            //var existingBook = await _context.Books.FindAsync(book.BookID);
            //if (existingBook == null) return false;

            //existingBook.Title = book.Title;
            //existingBook.AuthorID = book.AuthorID;
            //existingBook.CategoryID = book.CategoryID;
            //existingBook.Price = book.Price;
            //existingBook.StockQuantity = book.StockQuantity;
            //existingBook.ImageURL = book.ImageURL;

            //_context.Books.Update(existingBook);
            //return true;
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











        //public async Task<string> GetStockAvailabilityAsync(int bookId)
        //{
        //    var inventory = await
        //        _context.Inventories.FirstOrDefaultAsync(i=>i.BookID == bookId);
        //    if (inventory == null || inventory.Quantity == 0)
        //        return "Not Available";
        //    else if (inventory.Quantity > 0 && inventory.Quantity <= 5)
        //        return "Only a few books are left";
        //    else
        //        return "Available";
        //}
    }
}

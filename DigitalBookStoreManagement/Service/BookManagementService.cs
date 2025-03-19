using DigitalBookStoreManagement.Exceptions;
using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Repository;

namespace DigitalBookStoreManagement.Service
{
    public class BookManagementService : IBookManagementService
    {
        private readonly IBookManagementRepository _repository;

        public BookManagementService(IBookManagementRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookManagement>> GetAllBooksAsync()
        {
            var books = await _repository.GetAllBooksAsync();
            if (!books.Any())
            {
                throw new NotFoundException("No book records found.");
            }
            return books;
        }

        public async Task<BookManagement?> GetBookByIdAsync(int bookId)
        {
            var book = await _repository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {bookId} not found.");
            }
            return book;
        }

        public async Task<IEnumerable<BookManagement>> SearchBooksByTitleAsync(string title)
        {
            var book = await _repository.SearchBooksByTitleAsync(title);
            if (!book.Any())
            {
                throw new NotFoundException($"{title} Book not found.");
            }
            return book;
        }

        public async Task<IEnumerable<BookManagement>> GetBooksByAuthorNameAsync(string authorName)
        {
            return await _repository.GetBooksByAuthorNameAsync(authorName);
        }

        public async Task<IEnumerable<BookManagement>> GetBooksByCategoryNameAsync(string categoryName)
        {
            return await _repository.GetBooksByCategoryNameAsync(categoryName);
        }

        public async Task<BookManagement> AddBookAsync(BookManagement book)
        {
            var existingBook = await _repository.SearchBooksByTitleAsync(book.Title);
            if (existingBook == null)
            {
                throw new AlreadyExistsException($"Book {book.Title} already exists.");
            }
            return await _repository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(BookManagement book)
        {
            var existingBook = await _repository.GetBookByIdAsync(book.BookID);
            if (existingBook == null)
            {
                throw new NotFoundException($"Book with BookID {book.BookID} not found.");
            }
            await _repository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _repository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                throw new NotFoundException($"Book with BookID {bookId} not found.");
            }
            await _repository.DeleteBookAsync(bookId);
        }
    }
}

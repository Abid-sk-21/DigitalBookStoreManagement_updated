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
            return books;
        }

        public async Task<BookManagement?> GetBookByIdAsync(int bookId)
        {
            return await _repository.GetBookByIdAsync(bookId);
        }

        public async Task<IEnumerable<BookManagement>> SearchBooksByTitleAsync(string title)
        {
            return await _repository.SearchBooksByTitleAsync(title);
        }

        public async Task<IEnumerable<BookManagement>> GetBooksByAuthorNameAsync(string authorName)
        {
            return await _repository.GetBooksByAuthorNameAsync(authorName);
        }

        public async Task<IEnumerable<BookManagement>> GetBooksByCategoryNameAsync(string categoryName)
        {
            return await _repository.GetBooksByCategoryNameAsync(categoryName);
        }

        public async Task AddBookAsync(BookManagement book)
        {
            await _repository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(BookManagement book)
        {
            await _repository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int bookId)
        {
            await _repository.DeleteBookAsync(bookId);
        }







        //private string GetStockAvailability(int quantity)
        //{
        //    if (quantity == 0)
        //        return "Not Available";
        //    else if (quantity < 5)
        //        return "Only a few books are left";
        //    else
        //        return "Available";
        //}
    }
}

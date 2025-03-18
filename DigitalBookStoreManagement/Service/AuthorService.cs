using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Repository;

namespace DigitalBookStoreManagement.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _repository.GetAllAuthorsAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int authorId)
        {
            return await _repository.GetAuthorByIdAsync(authorId);
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _repository.AddAuthorAsync(author);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            await _repository.UpdateAuthorAsync(author);
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            await _repository.DeleteAuthorAsync(authorId);
        }
    }
}

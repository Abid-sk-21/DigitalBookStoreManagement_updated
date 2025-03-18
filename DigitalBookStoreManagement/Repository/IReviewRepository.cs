using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Repository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task<Review> AddReviewAsync(Review review);
        Task UpdateReviewAsync(int id, Review review);
        Task DeleteReviewAsync(int id);
        Task ApproveReviewAsync(int id);
    }
}

using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Service
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task<Review> AddReviewAsync(Review review);
        Task UpdateReviewAsync(int id, Review review);
        Task DeleteReviewAsync(int id);
        Task ApproveReviewAsync(int id);
    }
}

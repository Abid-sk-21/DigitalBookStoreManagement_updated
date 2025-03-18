using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Repository;

namespace DigitalBookStoreManagement.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllReviewsAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _reviewRepository.GetReviewByIdAsync(id);
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            return await _reviewRepository.AddReviewAsync(review);
        }

        public async Task UpdateReviewAsync(int id, Review review)
        {
            await _reviewRepository.UpdateReviewAsync(id, review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            await _reviewRepository.DeleteReviewAsync(id);
        }
        public async Task ApproveReviewAsync(int id)
        {
            await _reviewRepository.ApproveReviewAsync(id);
        }
    }
}

using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Repository
{
    public interface I_NotificationRepository
    {
        public Task AddNotificationAsync(string message);
        public Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        public Task AddorUpdateNotificationAsync(int bookId, string title, int inventoryId, int notifylimit);
        public Task DeleteNotificationAsync(int notificationId);
    }
}

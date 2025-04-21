using DigitalBookStoreManagement.Exceptions;
using DigitalBookStoreManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookStoreManagement.Repository
{
    public class NotificationRepository:I_NotificationRepository
    {
        private readonly BookStoreDBContext
            _context;
        public NotificationRepository(BookStoreDBContext context)
        {
            _context = context;
        }

        public async Task AddNotificationAsync(string message)
        {
            var notification = new Notification { Message = message };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task AddorUpdateNotificationAsync(int bookId, string title, int inventoryId, int notifylimit)
        {
            var existingNotification = await _context.Notifications.FirstOrDefaultAsync(n => n.Message.Contains($"InventoryID {inventoryId}") && n.Message.Contains($"BookID {bookId}"));

            if (existingNotification == null)
            {
                var notificaiton = new Notification
                {
                    Message = $"The stock for the book '{title}' is below the notify limit of {notifylimit}. Please restock."
                };
                _context.Notifications.Add(notificaiton);
            }
            else
            {
                existingNotification.Message = $"The stock for the book '{title}' is below the notify limit of {notifylimit}. Please restock.";
                _context.Notifications.Update(existingNotification);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _context.Set<Notification>().FindAsync(notificationId);
            if (notification == null)
            {
                throw new NotFoundException($"Notification with ID {notificationId} not found.");
            }

            _context.Set<Notification>().Remove(notification);
            await _context.SaveChangesAsync();
        }
    }
}

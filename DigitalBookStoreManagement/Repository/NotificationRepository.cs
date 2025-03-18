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
                    Message = $"The book containing in Inventory {inventoryId} with BookID {bookId} of '{title}' is less than the notifylimit {notifylimit}. Kindly re-stock the book."
                };
                _context.Notifications.Add(notificaiton);
            }
            else
            {
                existingNotification.Message = $"The book containing in Inventory {inventoryId} with BookID {bookId} of '{title}' is less than the notifylimit {notifylimit}. Kindly re-stock the book.";
                _context.Notifications.Update(existingNotification);
            }

            await _context.SaveChangesAsync();
        }
    }
}

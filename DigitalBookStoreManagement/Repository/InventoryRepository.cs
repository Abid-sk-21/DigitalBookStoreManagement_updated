using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Repository;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookstoreManagement.Repository
{
    public class InventoryRepository : I_InventoryRepository
    {
        private readonly BookStoreDBContext _context;
        private readonly I_NotificationRepository _notificationRepository;


        public InventoryRepository(BookStoreDBContext context, I_NotificationRepository notificationRepository)
        {
            _context = context;
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            return await _context.Inventories
                .Include(b => b.BookManagement).ThenInclude(i => i.Author)
                .Include(b => b.BookManagement).ThenInclude(i => i.Category).ToListAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int id)
        {
            return await _context.Inventories
                .Include(b => b.BookManagement).ThenInclude(i => i.Author)
                .Include(b => b.BookManagement).ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(i => i.InventoryID == id);
        }

        public async Task<Inventory> GetInventoryByBookIdAsync(int bookId)
        {
            return await _context.Inventories
                .Include(b => b.BookManagement).ThenInclude(i => i.Author)
                .Include(b => b.BookManagement).ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(i => i.BookID == bookId);
        }

        public async Task<Inventory> AddInventoryAsync(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            await CheckStockAndNotifyAdminAsync(inventory.BookID);
            return inventory;
        }

        public async Task<Inventory> UpdateInventoryAsync(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
            await CheckStockAndNotifyAdminAsync(inventory.BookID);
            return inventory;
        }

        public async Task DeleteInventoryAsync(int inventoryId)
        {
            var inventory = await _context.Inventories.FindAsync(inventoryId);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
                await _context.SaveChangesAsync();
            }
            //var maxId = await _context.Inventories.MaxAsync(i => (int?)i.InventoryID) ?? 0;
            //await _context.Database.ExecuteSqlRawAsync($"DBCC CHECKIDENT('Inventories', RESEED, {maxId})");
        }

        public async Task<bool> IsStockAvailableAsync(int bookId, int quantity)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.BookID == bookId);
            return inventory != null && inventory.Quantity >= quantity;
        }

        public async Task UpdateStockOnOrderAsync(int bookId, int orderedQuantity)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.BookID == bookId);
            if (inventory != null)
            {
                inventory.Quantity -= orderedQuantity;
                await _context.SaveChangesAsync();

                // Check stock and notify admin if needed
                await CheckStockAndNotifyAdminAsync(bookId);
            }
        }

        public async Task CheckStockAndNotifyAdminAsync(int bookId)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.BookID == bookId);
            if (inventory != null)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == bookId);
                if (book != null)
                {
                    if (inventory.Quantity == 0)
                        book.StockQuantity = "Not Available";
                    else if (inventory.Quantity <= 5)
                        book.StockQuantity = "Only few books are left";
                    else
                        book.StockQuantity = "Available";

                    _context.Books.Update(book);
                    await _context.SaveChangesAsync();

                    // 🔔 Send Email Notification if stock is below NotifyLimit
                    if (inventory.Quantity <= inventory.NotifyLimit && inventory.Quantity > 0)
                    {
                        await _notificationRepository.AddorUpdateNotificationAsync(bookId, book.Title, inventory.InventoryID, inventory.NotifyLimit);
                    }

                    else if (inventory.Quantity == 0)
                    {
                        string message = $"The book containing in Inventory {inventory.InventoryID} with BookID {book.BookID} of '{book.Title}' is out of stock. Kindly re-stock the book.";
                        await _notificationRepository.AddNotificationAsync(message);
                        Console.WriteLine($"ALERT: {message}");
                    }
                }
            }
        }

        public async Task<bool> AddStockAsync(int bookId, int quantity)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.BookID == bookId);

            if (inventory == null)
            {
                return false;
            }
            inventory.Quantity += quantity;
            _context.Update(inventory);
            await _context.SaveChangesAsync();
            await CheckStockAndNotifyAdminAsync(bookId);

            return true;
        }

        public void DetachEntity(Inventory inventory)
        {
            _context.Entry(inventory).State = EntityState.Detached;
        }
    }
}
using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Service
{
    public interface I_InventoryService
    {
        public Task<IEnumerable<Inventory>> GetAllInventoriesAsync();
        public Task<Inventory> GetInventoryByIdAsync(int inventoryId);
        public Task<Inventory> GetInventoryByBookIdAsync(int bookId);
        public Task AddInventoryAsync(Inventory inventory);
        public Task UpdateInventoryAsync(Inventory inventory);
        public Task DeleteInventoryAsync(int inventoryId);
        public Task CheckStockAndNotifyAdminAsync(int bookId);
        public Task<bool> IsStockAvailableAsync(int bookId, int quantity);
        public Task UpdateStockOnOrderAsync(int bookId, int orderedQuantity);
        public Task<bool> AddStockAsync(int bookId, int quantity);
    }
}

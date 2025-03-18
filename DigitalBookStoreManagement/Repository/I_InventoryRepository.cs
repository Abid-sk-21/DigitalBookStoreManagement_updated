using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Repository
{
    public interface I_InventoryRepository
    {
        public Task<IEnumerable<Inventory>> GetAllInventoriesAsync();
        public Task<Inventory> GetInventoryByIdAsync(int id);
        public Task<Inventory> GetInventoryByBookIdAsync(int bookId);
        public Task<Inventory> AddInventoryAsync(Inventory inventory);
        public Task<Inventory> UpdateInventoryAsync(Inventory inventory);
        public Task DeleteInventoryAsync(int inventoryId);
        public Task<bool> IsStockAvailableAsync(int bookId, int quantity);
        public Task UpdateStockOnOrderAsync(int bookId, int orderedQuantity);
        public Task CheckStockAndNotifyAdminAsync(int bookId);
        public Task<bool> AddStockAsync(int bookId, int quantity);
    }
}

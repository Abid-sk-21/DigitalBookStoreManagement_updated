
using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Repository;
using DigitalBookStoreManagement.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalBookstoreManagement.Services
{
    public class InventoryService : I_InventoryService
    {
        private readonly I_InventoryRepository _inventoryRepository;

        public InventoryService(I_InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        // ✅ 1. Get all inventory records
        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            return await _inventoryRepository.GetAllInventoriesAsync();
        }

        // ✅ 2. Get inventory by InventoryID
        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            return await _inventoryRepository.GetInventoryByIdAsync(inventoryId);
        }

        // ✅ 3. Get inventory by BookID
        public async Task<Inventory> GetInventoryByBookIdAsync(int bookId)
        {
            return await _inventoryRepository.GetInventoryByBookIdAsync(bookId);
        }

        // ✅ 4. Add new inventory
        public async Task AddInventoryAsync(Inventory inventory)
        {
            await _inventoryRepository.AddInventoryAsync(inventory);
        }

        // ✅ 5. Update inventory
        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            await _inventoryRepository.UpdateInventoryAsync(inventory);
        }

        // ✅ 6. Delete inventory
        public async Task DeleteInventoryAsync(int inventoryId)
        {
            await _inventoryRepository.DeleteInventoryAsync(inventoryId);
        }

        // ✅ 7. Check stock and notify admin if needed
        public async Task CheckStockAndNotifyAdminAsync(int bookId)
        {
            await _inventoryRepository.CheckStockAndNotifyAdminAsync(bookId);
        }

        // ✅ 8. Check if stock is available for order
        public async Task<bool> IsStockAvailableAsync(int bookId, int quantity)
        {
            var inventory = await _inventoryRepository.GetInventoryByBookIdAsync(bookId);
            return inventory != null && inventory.Quantity >= quantity;
        }

        // ✅ 9. Update stock when an order is placed
        public async Task UpdateStockOnOrderAsync(int bookId, int orderedQuantity)
        {
            var inventory = await _inventoryRepository.GetInventoryByBookIdAsync(bookId);
            if (inventory != null)
            {
                inventory.Quantity -= orderedQuantity;
                await _inventoryRepository.UpdateInventoryAsync(inventory);
                await CheckStockAndNotifyAdminAsync(bookId); // Check stock after updating
            }
        }

        public async Task<bool> AddStockAsync(int bookId, int quantity)
        {
            var inventory = await _inventoryRepository.AddStockAsync(bookId, quantity);
            return (inventory);
        }
    }
}
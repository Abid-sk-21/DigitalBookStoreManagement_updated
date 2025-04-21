
using DigitalBookStoreManagement.Exceptions;
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
        private readonly IBookManagementRepository _bookManagementRepository;

        public InventoryService(I_InventoryRepository inventoryRepository, IBookManagementRepository bookManagementRepository)
        {
            _inventoryRepository = inventoryRepository;
            _bookManagementRepository = bookManagementRepository;
        }

        // ✅ 1. Get all inventory records
        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            var inventories = await _inventoryRepository.GetAllInventoriesAsync();
            if (!inventories.Any())
            {
                throw new NotFoundException("No inventory records found.");
            }
            return inventories;
        }

        // ✅ 2. Get inventory by InventoryID
        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            var inventory = await _inventoryRepository.GetInventoryByIdAsync(inventoryId);
            if (inventory == null)
            {
                throw new NotFoundException($"Inventory with ID {inventoryId} not found.");
            }
            return inventory;
        }

        // ✅ 3. Get inventory by BookID
        public async Task<Inventory> GetInventoryByBookIdAsync(int bookId)
        {
            var inventory = await _inventoryRepository.GetInventoryByBookIdAsync(bookId);
            if (inventory == null)
            {
                throw new NotFoundException($"Inventory with BookID {bookId} not found.");
            }
            return inventory;
        }

        // ✅ 4. Add new inventory
        public async Task AddInventoryAsync(Inventory inventory)
        {
            var existingInventory = await _inventoryRepository.GetInventoryByBookIdAsync(inventory.BookID);
            if (existingInventory != null)
            {
                throw new AlreadyExistsException($"Inventory for BookID {inventory.BookID} already exists.");
            }

            var bookExists = await _bookManagementRepository.GetBookByIdAsync(inventory.BookID);
            if (bookExists == null)
            {
                throw new NotFoundException($"Book with BookID {inventory.BookID} not found. Cannot add inventory");
            }

            await _inventoryRepository.AddInventoryAsync(inventory);

            var addedInventory = await _inventoryRepository.GetInventoryByBookIdAsync(inventory.BookID);
            if (addedInventory == null)
            {
                throw new System.Exception($"Failed to add inventory for BookID {inventory.BookID}.");
            }
        }

        // ✅ 5. Update inventory
        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            var existingInventory = await _inventoryRepository.GetInventoryByIdAsync(inventory.InventoryID);
            if (existingInventory == null)
            {
                throw new NotFoundException($"Inventory for ID {inventory.InventoryID} not found.");
            }

            _inventoryRepository.DetachEntity(existingInventory);
            await _inventoryRepository.UpdateInventoryAsync(inventory);
        }

        // ✅ 6. Delete inventory
        public async Task DeleteInventoryAsync(int inventoryId)
        {
            var inventory = await _inventoryRepository.GetInventoryByIdAsync(inventoryId);
            if (inventory == null)
            {
                throw new NotFoundException($"Inventory for ID {inventoryId} not found.");
            }

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
            if (inventory == null)
            {
                throw new NotFoundException($"No inventory found for BookID {bookId}.");
            }
            return inventory != null && inventory.Quantity >= quantity;
        }

        // ✅ 9. Update stock when an order is placed
        public async Task UpdateStockOnOrderAsync(int bookId, int orderedQuantity)
        {
            var inventory = await _inventoryRepository.GetInventoryByBookIdAsync(bookId);

            if (inventory == null)
            {
                throw new NotFoundException($"No inventory found for BookID {bookId}.");
            }

            if (inventory.Quantity < orderedQuantity)
            {
                throw new InvalidOperationException($"Insufficient stock for BookID {bookId}. Available: {inventory.Quantity}, Requested: {orderedQuantity}");
            }
            await _inventoryRepository.UpdateStockOnOrderAsync(bookId, orderedQuantity);
            await CheckStockAndNotifyAdminAsync(bookId);
        }

        public async Task<bool> AddStockAsync(int bookId, int quantity)
        {
            var inventory = await _inventoryRepository.GetInventoryByBookIdAsync(bookId);
            if (inventory == null)
            {
                throw new NotFoundException($"No inventory found for BookID {bookId}.");
            }
            inventory.Quantity += quantity;
            await _inventoryRepository.UpdateInventoryAsync(inventory);
            return true;
        }
    }
}
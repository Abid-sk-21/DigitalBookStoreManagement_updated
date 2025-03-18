using DigitalBookStoreManagement.Authentication;
using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBookStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly I_InventoryService _inventoryService;
        private readonly IAuth jwtAuth;
        public InventoryController(I_InventoryService inventoryService,IAuth jwt)
        {
            _inventoryService = inventoryService;
            jwtAuth = jwt;
        }

        // ✅ 1. Get all inventory items
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetAllInventories()
        {
            var inventories = await _inventoryService.GetAllInventoriesAsync();
            return Ok(inventories);
        }

        // ✅ 2. Get inventory by InventoryID
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventoryById(int id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (inventory == null)
                return NotFound($"Inventory with ID {id} not found.");

            return Ok(inventory);
        }

        // ✅ 3. Get inventory by BookID
        [Authorize(Roles = "Admin")]
        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<Inventory>> GetInventoryByBookId(int bookId)
        {
            var inventory = await _inventoryService.GetInventoryByBookIdAsync(bookId);
            if (inventory == null)
                return NotFound($"Inventory for BookID {bookId} not found.");

            return Ok(inventory);
        }

        // ✅ 4. Add new inventory
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Inventory>> AddInventory([FromBody] Inventory inventory)
        {
            if (inventory == null)
                return BadRequest("Invalid inventory data.");

            //  inventory.Book = null;

            await _inventoryService.AddInventoryAsync(inventory);
            return CreatedAtAction(nameof(GetInventoryById), new { id = inventory.InventoryID }, inventory);
        }

        // ✅ 5. Update inventory
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, [FromBody] Inventory inventory)
        {
            if (inventory == null || id != inventory.InventoryID)
                return BadRequest("Inventory ID mismatch or invalid data.");

            //  inventory.Book = null;

            await _inventoryService.UpdateInventoryAsync(inventory);
            return NoContent();
        }

        //Add Stock in Inventory
        [HttpPost("add-stock")]
        public async Task<IActionResult> AddStock(int bookId, int quantity)
        {
            bool success = await _inventoryService.AddStockAsync(bookId, quantity);
            //bool isAvailable = await _inventoryService.IsStockAvailableAsync(bookId, quantity);

            if (!success)
            {
                return BadRequest("Stock update failed. Inventory item not found.");
            }

            return Ok("Stock added successfully.");
        }

        // ✅ 6. Delete inventory
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var existingInventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (existingInventory == null)
                return NotFound("Inventory not found!");

            await _inventoryService.DeleteInventoryAsync(id);
            return NoContent();
        }

        [HttpPut("update-stock")]
        public async Task<IActionResult> UpdateStockOnOrder(int bookId, int orderedQuantity)
        {
            bool isAvailable = await _inventoryService.IsStockAvailableAsync(bookId, orderedQuantity);
            if (!isAvailable)
                return BadRequest("Insufficient stock!");

            await _inventoryService.UpdateStockOnOrderAsync(bookId, orderedQuantity);
            return Ok("Stock updated successfully!");
        }

        // ✅ 7. Check Stock and Notify Admin
        [HttpPost("check-stock/{bookId}")]
        public async Task<IActionResult> CheckStockAndNotifyAdminAsync(int bookId)
        {
            await _inventoryService.CheckStockAndNotifyAdminAsync(bookId);
            return Ok($"Stock check completed for BookID {bookId}.");
        }
    }
}

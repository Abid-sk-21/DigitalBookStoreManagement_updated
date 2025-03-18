using Microsoft.AspNetCore.Mvc;
using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Repositories;
using Microsoft.AspNetCore.Authorization;

using DigitalBookStoreManagement.Expections;
namespace DigitalBookStoreManagement.Controllers

{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }


        [Authorize(Roles ="Admin, Customer")]
        [HttpGet("get-cart-by-id/{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _cartRepository.GetCartByID(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [Authorize(Roles = "Customer ,Admin")]
        [HttpPost("add-item-to-cart/{userId}")]
        public IActionResult AddItemToCart(int userId, CartItem newItem)
        {
            var CheckIfAdded = _cartRepository.AddItemsToCart(userId, newItem);
            if (CheckIfAdded != null)
            {
                return Ok(CheckIfAdded);
            }
            return BadRequest();
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("checkout/{cartId}")]
        public IActionResult Checkout(int cartId)
        {
            var OrderCreated = _cartRepository.CheckOutCart(cartId);
            if (OrderCreated != null)
            {
                return Ok(OrderCreated);
            }
            return BadRequest();
        }





        [Authorize(Roles = "Customer")]
        [HttpDelete("delete-cart/{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _cartRepository.GetCartByID(id);
            if (cart == null)
            {
                throw new OrderNotFoundException(id);
            }

            await _cartRepository.DeleteCart(id);
            return NoContent();
        }




    }
}

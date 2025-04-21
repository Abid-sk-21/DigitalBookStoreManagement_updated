//using Microsoft.AspNetCore.Mvc;
//using DigitalBookStoreManagement.Models;
//using DigitalBookStoreManagement.Repositories;
//using Microsoft.AspNetCore.Authorization;

//using DigitalBookStoreManagement.Model;
//using DigitalBookStoreManagement.Expections;
//namespace DigitalBookStoreManagement.Controllers

//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CartController : ControllerBase
//    {
//        private readonly ICartRepository _cartRepository;

//        public CartController(ICartRepository cartRepository)
//        {
//            _cartRepository = cartRepository;
//        }


//        //[Authorize(Roles = "Admin, Customer")]
//        [AllowAnonymous]
//        [HttpGet("get-cart-by-id/{id}")]
//        public async Task<ActionResult<Cart>> GetCart(int id)
//        {
//            var cart = await _cartRepository.GetCartByID(id);
//            if (cart == null)
//            {
//                return NotFound($"Cart with id {id} does not exists.");
//            }
//            return Ok(cart);
//        }

//        //[Authorize(Roles = "Customer")]
//        [AllowAnonymous]
//        [HttpPost("add-item-to-cart/{userId}")]
//        public IActionResult AddItemToCart(int userId, CartItem newItem)
//        {
//            var CheckIfAdded = _cartRepository.AddItemsToCart(userId, newItem);
//            if (CheckIfAdded != null)
//            {
//                return Ok(CheckIfAdded);
//            }
//            return BadRequest($"Book with BookId {newItem.BookID} does not exists");
//        }

//        [Authorize(Roles = "Customer")]
//        [HttpPost("checkout/{cartId}")]
//        public IActionResult Checkout(int cartId ,string Address , string Payment_Status)
//        {
//            var OrderCreated = _cartRepository.CheckOutCart(cartId , Address , Payment_Status) ;
//            if (OrderCreated != null)
//            {
//                return Ok(OrderCreated);
//            }
//            return BadRequest();
//        }





//        [Authorize(Roles = "Customer")]
//        [HttpDelete("delete-cart/{id}")]
//        public async Task<IActionResult> DeleteCart(int id)
//        {

//            try
//            {
//                var cart = await _cartRepository.GetCartByID(id);
//                await _cartRepository.DeleteCart(id);



//            }
//            catch (OrderNotFoundException ex)
//            {
//                return Conflict();
//            }


//            return NoContent();
//        }




//    }
//}

//using Microsoft.AspNetCore.Mvc;
//using DigitalBookStoreManagement.Models;
//using DigitalBookStoreManagement.Repositories;
//using Microsoft.AspNetCore.Authorization;
//using DigitalBookStoreManagement.Model;
//using DigitalBookStoreManagement.Expections;

//namespace DigitalBookStoreManagement.Controllers
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CartController : ControllerBase
//    {
//        private readonly ICartRepository _cartRepository;

//        public CartController(ICartRepository cartRepository)
//        {
//            _cartRepository = cartRepository;
//        }

//        [HttpGet("get-cart-by-id/{id}")]
//        [AllowAnonymous]
//        public ActionResult<Cart> GetCart(int id)
//        {
//            var cart = _cartRepository.GetCartByUserID(id);
//            if (cart == null)
//            {
//                return NotFound($"Cart with id {id} does not exist.");
//            }
//            return Ok(cart);
//        }

//        [HttpPost("add-item-to-cart/{userId}")]
//        [AllowAnonymous]
//        public IActionResult AddItemToCart(int userId, CartItem newItem)
//        {
//            var CheckIfAdded = _cartRepository.AddItemsToCart(userId, newItem);
//            if (CheckIfAdded != null)
//            {
//                return Ok(CheckIfAdded);
//            }
//            return BadRequest($"Book with BookId {newItem.BookID} does not exist.");
//        }

//        [HttpPost("checkout/{userID}")]
//        [AllowAnonymous]
//        public IActionResult Checkout(int userID, string Address, string Payment_Status)
//        {
//            var OrderCreated = _cartRepository.CheckOutCart(userID, Address, Payment_Status);
//            if (OrderCreated != null)
//            {
//                return Ok(OrderCreated);
//            }
//            return BadRequest();
//        }

//        [HttpDelete("delete-cart-by-userid/{id}")]
//        [AllowAnonymous]
//        public IActionResult DeleteCart(int id)
//        {
//            try
//            {
//                _cartRepository.DeleteCart(id);
//            }
//            catch (OrderNotFoundException ex)
//            {
//                return Conflict();
//            }
//            return NoContent();
//        }


//        [AllowAnonymous]
//        [HttpDelete("remove-item-from-cart/{cartId}/{cartItemId}")]
//        public IActionResult RemoveItemFromCart(int cartId, int cartItemId)
//        {
//            var result = _cartRepository.RemoveItemFromCart(cartId, cartItemId);
//            if (result)
//            {
//                return NoContent();
//            }
//            return NotFound($"Item with id {cartItemId} does not exist in cart {cartId}.");
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using DigitalBookStoreManagement.Models;
using DigitalBookStoreManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using DigitalBookStoreManagement.Model;
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

        [HttpGet("get-cart-by-id/{id}")]
        [AllowAnonymous]
        public ActionResult<Cart> GetCart(int id)
        {
            var cart = _cartRepository.GetCartByUserID(id);
            if (cart == null)
            {
                return NotFound($"Cart with id {id} does not exist.");
            }
            return Ok(cart);
        }

        [HttpPost("add-item-to-cart/{userId}")]
        [AllowAnonymous]
        public IActionResult AddItemToCart(int userId, CartItem newItem)
        {
            var CheckIfAdded = _cartRepository.AddItemsToCart(userId, newItem);
            if (CheckIfAdded != null)
            {
                return Ok(CheckIfAdded);
            }
            return BadRequest($"Book with BookId {newItem.BookID} does not exist.");
        }

        [HttpPost("checkout/{userID}")]
        [AllowAnonymous]
        public IActionResult Checkout(int userID, string Address, string Payment_Status)
        {
            var OrderCreated = _cartRepository.CheckOutCart(userID, Address, Payment_Status);
            if (OrderCreated != null)
            {
                return Ok(OrderCreated);
            }
            return BadRequest();
        }

        [HttpDelete("delete-cart-by-userid/{id}")]
        [AllowAnonymous]
        public IActionResult DeleteCart(int id)
        {
            try
            {
                _cartRepository.DeleteCart(id);
            }
            catch (OrderNotFoundException ex)
            {
                return Conflict();
            }
            return NoContent();
        }


        [AllowAnonymous]
        [HttpDelete("remove-item-from-cart/{cartItemId}")]
        public IActionResult RemoveItemFromCart(int cartItemId)
        {
            var result = _cartRepository.RemoveItemFromCart(cartItemId);
            if (result)
            {
                return NoContent();
            }
            return NotFound($"Item with id {cartItemId} does not exist in cart.");
        }
    }
}








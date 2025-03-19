using DigitalBookStoreManagement.Expections;
using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace DigitalBookStoreManagement.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly BookStoreDBContext _context;

        public CartRepository(BookStoreDBContext context)
        {
            _context = context;
        }



        public async Task<Cart> GetCartByID(int id)
        {
            return await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.CartID == id);
        }



        public Cart AddItemsToCart(int userId, CartItem newItem)
        {

            var CheckCartExists = _context.Carts.Include(ci => ci.CartItems).FirstOrDefault(c => c.UserID == userId);
            if (CheckCartExists == null)
            {
                CheckCartExists = new Cart { UserID = userId, CreatedDate = DateTime.Now };
                _context.Carts.Add(CheckCartExists);
                _context.SaveChanges();



            }
            var checkQuantityAvailable = _context.Inventories.FirstOrDefault(QA => QA.BookID == newItem.BookID);
            if (checkQuantityAvailable.Quantity <= newItem.Quantity)
            {
                throw new QuantityNotAvailable(newItem.BookID);
            }


            var cartItemToAdd = new CartItem
            {
                CartID = CheckCartExists.CartID,
                BookID = newItem.BookID,
                Quantity = newItem.Quantity,
                Price = newItem.Price,
                TotalAmount = newItem.TotalAmount,


            };
            checkQuantityAvailable.Quantity = checkQuantityAvailable.Quantity - newItem.Quantity;
            _context.CartItems.Add(cartItemToAdd);
            _context.SaveChanges();
            return CheckCartExists;


        }



        public async Task DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public Order CheckOutCart(int cartId)
        {
            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.CartID == cartId);
            if (cart == null)
            {
                return null;
            }


            Order NewOrder = new Order
            {
                UserID = cart.UserID,
                OrderStatus = "Pending",
                OrderDate = DateTime.Now,
                TotalAmount = cart.CartItems.Sum(item => item.TotalAmount),
                PaymentStatus = "Not Paid",
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    BookID = ci.BookID,
                    Price = ci.Price,
                    Quantity = ci.Quantity,
                    TotalAmount = ci.Price * ci.Quantity
                }).ToList()


            };
            _context.Orders.Add(NewOrder);
            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return NewOrder;
        }
    }
}

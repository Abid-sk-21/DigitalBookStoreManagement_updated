//using DigitalBookStoreManagement.Expections;
//using DigitalBookStoreManagement.Model;
//using DigitalBookStoreManagement.Models;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;

//using Microsoft.EntityFrameworkCore;

//namespace DigitalBookStoreManagement.Repositories
//{
//    public class CartRepository : ICartRepository
//    {
//        private readonly BookStoreDBContext _context;

//        public CartRepository(BookStoreDBContext context)
//        {
//            _context = context;
//        }



//        public async Task<Cart> GetCartByID(int id)
//        {
//            return await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.CartID == id);
//        }



//        public Cart AddItemsToCart(int userId, CartItem newItem)
//        {

//            var CheckCartExists = _context.Carts.Include(ci => ci.CartItems).FirstOrDefault(c => c.UserID == userId);

//            var BookToAdd = _context.Books.FirstOrDefault(b => b.BookID == newItem.BookID);
//            if (CheckCartExists == null)
//            {
//                CheckCartExists = new Cart { UserID = userId, CreatedDate = DateTime.Now };
//                _context.Carts.Add(CheckCartExists);
//                _context.SaveChanges();



//            }

//            var checkQuantityAvailable = _context.Inventories.FirstOrDefault(QA => QA.BookID == newItem.BookID);

//            if(checkQuantityAvailable == null)
//            {
//                return null;
//            }
//            if (checkQuantityAvailable.Quantity <= newItem.Quantity)
//            {
//                throw new QuantityNotAvailable(newItem.BookID);
//            }


//            var cartItemToAdd = new CartItem
//            {
//                CartID = CheckCartExists.CartID,
//                BookID = newItem.BookID,
//                Quantity = newItem.Quantity,
//                Price = (double)BookToAdd.Price,
//                TotalAmount = (double)(BookToAdd.Price*newItem.Quantity),


//            };
//            checkQuantityAvailable.Quantity = checkQuantityAvailable.Quantity - newItem.Quantity;
//            _context.CartItems.Add(cartItemToAdd);
//            _context.SaveChanges();
//            return CheckCartExists;


//        }



//        public async Task DeleteCart(int id)
//        {
//            var cart = await _context.Carts.FindAsync(id);
//            if (cart != null)
//            {
//                _context.Carts.Remove(cart);
//                await _context.SaveChangesAsync();
//            }
//        }

//        public Order CheckOutCart(int cartId , string Address , string Payment_Status)
//        {
//            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.CartID == cartId);
//            if (cart == null)
//            {
//                return null;
//            }


//            Order NewOrder = new Order
//            {
//                UserID = cart.UserID,
//                OrderStatus = "Pending",
//                OrderDate = DateTime.Now,
//                TotalAmount = cart.CartItems.Sum(item => item.TotalAmount),
//                PaymentStatus = Payment_Status,
//                DeliveryAddress = Address,
//                OrderItems = cart.CartItems.Select(ci => new OrderItem
//                {
//                    BookID = ci.BookID,
//                    Price = ci.Price,
//                    Quantity = ci.Quantity,
//                    TotalAmount = ci.Price * ci.Quantity
//                }).ToList()


//            };
//            _context.Orders.Add(NewOrder);
//            _context.Carts.Remove(cart);
//            _context.SaveChanges();
//            return NewOrder;
//        }
//    }
//}

//using DigitalBookStoreManagement.Expections;

//using DigitalBookStoreManagement.Model;

//using DigitalBookStoreManagement.Models;

//using Microsoft.AspNetCore.Http.HttpResults;

//using Microsoft.AspNetCore.Mvc;

//using Microsoft.EntityFrameworkCore;

//namespace DigitalBookStoreManagement.Repositories

//{

//    public class CartRepository : ICartRepository

//    {

//        private readonly BookStoreDBContext _context;

//        public CartRepository(BookStoreDBContext context)

//        {

//            _context = context;

//        }


//        public Cart GetCartByID(int id)

//        {

//            return _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.CartID == id);

//        }

//        public Cart GetCartByUserID(int id)

//        {

//            return _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.UserID == id);

//        }


//        public Cart CheckCartExistsOrNot(int userId)

//        {

//            var CheckCartExists = _context.Carts.Include(ci => ci.CartItems).FirstOrDefault(c => c.UserID == userId);

//            return CheckCartExists;

//        }



//        public Cart CreateNewCart(int userId)

//        {

//            return new Cart { UserID = userId, CreatedDate = DateTime.Now };

//        }



//        public BookManagement CheckBookAvailableOrNot(int BookID)

//        {

//            return _context.Books.FirstOrDefault(b => b.BookID == BookID);

//        }



//        public Inventory CheckBookQuantityAvailableOrNot(int BookID)

//        {

//            return _context.Inventories.FirstOrDefault(QA => QA.BookID == BookID);

//        }



//        public Cart AddItemsToCart(int userId, CartItem newItem)

//        {

//            var CheckCartExists = CheckCartExistsOrNot(userId);

//            if (CheckCartExists == null)

//            {

//                CheckCartExists = CreateNewCart(userId);

//                _context.Carts.Add(CheckCartExists);

//                _context.SaveChanges();

//            }

//            var BookToAdd = CheckBookAvailableOrNot(newItem.BookID);

//            //if(BookToAdd == null)

//            //{

//            //    throw new 

//            //}


//            var checkQuantityAvailable = CheckBookQuantityAvailableOrNot(newItem.BookID);

//            if (checkQuantityAvailable == null)

//            {

//                throw new QuantityNotAvailable(newItem.BookID);

//            }

//            var cartItemToAdd = new CartItem

//            {

//                CartID = CheckCartExists.CartID,

//                BookID = newItem.BookID,

//                Quantity = newItem.Quantity,

//                Price = (double)BookToAdd.Price,

//                TotalAmount = (double)(BookToAdd.Price * newItem.Quantity),

//            };

//            checkQuantityAvailable.Quantity = checkQuantityAvailable.Quantity - newItem.Quantity;

//            _context.CartItems.Add(cartItemToAdd);

//            _context.SaveChanges();

//            return CheckCartExists;


//        }




//        public async Task DeleteCart(int id)

//        {

//            var cart = GetCartByUserID(id);

//            if (cart != null)

//            {

//                _context.Carts.Remove(cart);

//                await _context.SaveChangesAsync();

//                Console.WriteLine($"Cart with UserID {id} deleted successfully."); // Debugging log

//            }

//            else

//            {

//                Console.WriteLine($"Cart with UserID {id} not found."); // Debugging log

//            }

//        }



//        public Order CheckOutCart(int userID, string Address, string Payment_Status)

//        {

//            var cart = GetCartByUserID(userID);

//            if (cart == null)

//            {

//                return null;

//            }


//            Order NewOrder = new Order

//            {

//                UserID = cart.UserID,

//                OrderStatus = "Ordered",

//                OrderDate = DateTime.Now,

//                TotalAmount = cart.CartItems.Sum(item => item.TotalAmount),

//                PaymentStatus = Payment_Status,

//                DeliveryAddress = Address,

//                OrderItems = cart.CartItems.Select(ci => new OrderItem

//                {

//                    BookID = ci.BookID,

//                    Price = ci.Price,

//                    Quantity = ci.Quantity,

//                    TotalAmount = ci.Price * ci.Quantity

//                }).ToList()


//            };

//            _context.Orders.Add(NewOrder);

//            _context.Carts.Remove(cart);

//            _context.SaveChanges();

//            return NewOrder;

//        }


//        public bool RemoveItemFromCart(int cartId, int cartItemId)

//        {

//            var cart = GetCartByID(cartId);

//            if (cart == null) return false;

//            var item = cart.CartItems.FirstOrDefault(ci => ci.CartItemID == cartItemId);

//            if (item == null) return false;

//            _context.CartItems.Remove(item);

//            _context.SaveChanges();

//            return true;

//        }

//    }

//}



using DigitalBookstoreManagement.Repository;
using DigitalBookStoreManagement.Expections;
using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Models;
using DigitalBookStoreManagement.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace DigitalBookStoreManagement.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly BookStoreDBContext _context;
        private readonly I_NotificationRepository _notificationRepository;
        private readonly I_InventoryRepository _inventoryRepository;

        public CartRepository(BookStoreDBContext context, I_NotificationRepository notificationRepository, I_InventoryRepository inventoryRepository)
        {
            _context = context;
            _notificationRepository = notificationRepository;
            _inventoryRepository = inventoryRepository;
        }


        public Cart GetCartByID(int id)
        {
            return _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.CartID == id);
        }
        public Cart GetCartByUserID(int id)
        {
            return _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.UserID == id);
        }


        public Cart CheckCartExistsOrNot(int userId)
        {
            var CheckCartExists = _context.Carts.Include(ci => ci.CartItems).FirstOrDefault(c => c.UserID == userId);
            return CheckCartExists;
        }



        public Cart CreateNewCart(int userId)
        {
            return new Cart { UserID = userId, CreatedDate = DateTime.Now };
        }



        public BookManagement CheckBookAvailableOrNot(int BookID)
        {
            return _context.Books.FirstOrDefault(b => b.BookID == BookID);
        }



        public Inventory CheckBookQuantityAvailableOrNot(int BookID)
        {
            return _context.Inventories.FirstOrDefault(QA => QA.BookID == BookID);
        }



        public Cart AddItemsToCart(int userId, CartItem newItem)
        {
            var CheckCartExists = CheckCartExistsOrNot(userId);
            if (CheckCartExists == null)
            {
                CheckCartExists = CreateNewCart(userId);
                _context.Carts.Add(CheckCartExists);
                _context.SaveChanges();

            }
            var BookToAdd = CheckBookAvailableOrNot(newItem.BookID);

            //if(BookToAdd == null)
            //{
            //    throw new 
            //}


            var checkQuantityAvailable = CheckBookQuantityAvailableOrNot(newItem.BookID);

            if (checkQuantityAvailable == null)
            {
                throw new QuantityNotAvailable(newItem.BookID);
            }

            var cartItemToAdd = new CartItem
            {
                CartID = CheckCartExists.CartID,
                BookID = newItem.BookID,
                Quantity = newItem.Quantity,
                Price = (double)BookToAdd.Price,
                TotalAmount = (double)(BookToAdd.Price * newItem.Quantity),

            };
            checkQuantityAvailable.Quantity = checkQuantityAvailable.Quantity - newItem.Quantity;
            // Check if the inventory quantity is below the notification limit
            if (checkQuantityAvailable.Quantity <= checkQuantityAvailable.NotifyLimit)
            {
                // Call the notification repository to add or update the notification
                _notificationRepository.AddorUpdateNotificationAsync(
                    newItem.BookID,
                    BookToAdd.Title,
                    checkQuantityAvailable.InventoryID,
                    checkQuantityAvailable.NotifyLimit
                ).Wait(); // Use `.Wait()` to handle the async call in a synchronous method
            }

            _context.CartItems.Add(cartItemToAdd);
            _context.SaveChanges();
            _inventoryRepository.CheckStockAndNotifyAdminAsync(newItem.BookID);
            return CheckCartExists;


        }




        public async Task DeleteCart(int id)
        {
            var cart = GetCartByUserID(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Cart with UserID {id} deleted successfully."); // Debugging log
            }
            else
            {
                Console.WriteLine($"Cart with UserID {id} not found."); // Debugging log
            }
        }



        public Order CheckOutCart(int userID, string Address, string Payment_Status)
        {
            var cart = GetCartByUserID(userID);
            if (cart == null)
            {
                return null;
            }


            Order NewOrder = new Order
            {
                UserID = cart.UserID,
                OrderStatus = "Ordered",
                OrderDate = DateTime.Now,
                TotalAmount = cart.CartItems.Sum(item => item.TotalAmount),
                PaymentStatus = Payment_Status,
                DeliveryAddress = Address,
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


        public bool RemoveItemFromCart(int cartItemId)
        {

            var item = _context.CartItems.FirstOrDefault(ci => ci.CartItemID == cartItemId);
            if (item == null) return false;

            _context.CartItems.Remove(item);
            _context.SaveChanges();
            return true;
        }

    }
}

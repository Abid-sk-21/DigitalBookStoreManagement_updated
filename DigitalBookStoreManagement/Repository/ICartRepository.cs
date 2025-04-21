//using DigitalBookStoreManagement.Model;
//using DigitalBookStoreManagement.Models;

//namespace DigitalBookStoreManagement.Repositories
//{
//    public interface ICartRepository
//    {
//        public Task<Cart> GetCartByID(int id);
//        Cart AddItemsToCart(int userId, CartItem newItem);
//        Task DeleteCart(int id);

//        Order CheckOutCart(int cartId, string Address, string Payment_Status);
//    }
//}

using DigitalBookStoreManagement.Model;

using DigitalBookStoreManagement.Models;



namespace DigitalBookStoreManagement.Repositories

{

    public interface ICartRepository

    {

        public Cart GetCartByID(int id);

        public Cart GetCartByUserID(int id);

        Cart AddItemsToCart(int userId, CartItem newItem);

        Task DeleteCart(int id);

        bool RemoveItemFromCart(int cartItemId);

        Order CheckOutCart(int userID, string Address, string Payment_Status);

    }

}

 




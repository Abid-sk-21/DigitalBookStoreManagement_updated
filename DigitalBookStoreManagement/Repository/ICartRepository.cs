using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Models;

namespace DigitalBookStoreManagement.Repositories
{
    public interface ICartRepository
    {
        public Task<Cart> GetCartByID(int id);
        Cart AddItemsToCart(int userId, CartItem newItem);
        Task DeleteCart(int id);

        Order CheckOutCart(int cartId);
    }
}

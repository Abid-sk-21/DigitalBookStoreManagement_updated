using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByID(int id);
        Cart AddItemsToCart(int userId, CartItem newItem);
        Task DeleteCart(int id);

        Order CheckOutCart(int cartId);
    }
}

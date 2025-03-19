using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Models;

namespace DigitalBookStoreManagement.Repository
{
    public interface IOrderRepository
    {
        public Order? GetOrderByOrderId(int orderID);
        public IEnumerable<Order> GetAllOrder();

        public IEnumerable<Order> GetOrderByUserId(int userID);

        public Order PlaceOrder(Order order);
        public bool CancelOrder(int orderID);

        public bool UpdateStatus(int orderID, String orderStatus);
        public bool UpdateOrderTotal(int orderId);



    }
}

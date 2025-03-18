using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Repository
{
    public interface IOrderRepository
    {
        public Order GetOrderByOrderId(int orderID);
        public List<Order> GetAllOrder();

        public List<Order> GetOrderByUserId(int userID);

        Order PlaceOrder(Order order);
        bool CancelOrder(int orderID);

        bool UpdateStatus(int orderID, String orderStatus);
        //bool UpdateOrderTotal(int orderId);



    }
}

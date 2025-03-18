

using DigitalBookStoreManagement.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using DigitalBookStoreManagement.Expections;
using DigitalBookStoreManagement.Model;

public class OrderRepository : IOrderRepository
{
    private readonly BookStoreDBContext _context;

    public OrderRepository(BookStoreDBContext context)
    {
        _context = context;
    }

    public List<Order> GetAllOrder()
    {
        return _context.Orders.Include(o => o.OrderItems).ToList();
    }

    public Order GetOrderByOrderId(int orderId)
    {


        var orders = _context.Orders.Include(o => o.OrderItems)
                              .FirstOrDefault(o => o.OrderID == orderId);
        return orders;
    }

    public List<Order> GetOrderByUserId(int userId)
    {
        return _context.Orders.Include(o => o.OrderItems)
                              .Where(o => o.UserID == userId).ToList();
    }

    public Order PlaceOrder(Order order)
    {


        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));

        }

        
        _context.Orders.Add(order);
        _context.SaveChanges();
        return order;



    }

    public bool CancelOrder(int orderId)
    {

        var order = _context.Orders
       .Include(o => o.OrderItems)
       .FirstOrDefault(o => o.OrderID == orderId);

        if (order == null)
        {
            throw new OrderNotFoundException(orderId);
        }


        _context.Orders.Remove(order);

        _context.SaveChanges();

        return true;

    }

    public bool UpdateStatus(int orderId, string status)
    {
        var order = _context.Orders.Find(orderId);
        if (order != null)
        {
            order.OrderStatus = status;
            return _context.SaveChanges() > 0;
        }
        return false;
    }



    //public bool UpdateOrderTotal(int orderId)
    //{
    //    Order order = GetOrderByOrderId(orderId);
    //    if (order != null)
    //    {
    //        order.TotalAmount = order.OrderItems.Sum(item => item.TotalAmount);
    //        return _context.SaveChanges() > 0;
    //    }
    //    return false;


    //}
}

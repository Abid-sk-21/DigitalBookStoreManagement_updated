using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Models;
using DigitalBookStoreManagement.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class OrderRepository : IOrderRepository
{
    private readonly BookStoreDBContext _context;

    public OrderRepository(BookStoreDBContext context)
    {
        _context = context;
    }

    // Get all orders
    public IEnumerable<Order> GetAllOrder()
    {
        return _context.Orders.Include(o => o.OrderItems).ToList();
    }

    // Get order by Order ID
    public Order? GetOrderByOrderId(int orderId)
    {
        return _context.Orders
                       .Include(o => o.OrderItems)
                       .FirstOrDefault(o => o.OrderID == orderId);
    }

    // Get orders by User ID
    public IEnumerable<Order> GetOrderByUserId(int userId)
    {
        return _context.Orders
                       .Include(o => o.OrderItems)
                       .Where(o => o.UserID == userId)
                       .ToList();
    }

    // Place a new order
    public Order PlaceOrder(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order), "Order cannot be null.");
        }

        _context.Orders.Add(order);
        _context.SaveChanges();
        return order;
    }

    // Cancel an order
    public bool CancelOrder(int orderId)
    {
        var order = _context.Orders
                            .Include(o => o.OrderItems)
                            .FirstOrDefault(o => o.OrderID == orderId);

        if (order == null)
        {
            return false; // Order not found
        }

        _context.Orders.Remove(order);
        return _context.SaveChanges() > 0; // Returns true if changes were made
    }

    // Update order status
    public bool UpdateStatus(int orderId, string status)
    {
        var order = _context.Orders.Find(orderId);
        if (order == null) return false;

        order.OrderStatus = status;
        return _context.SaveChanges() > 0;
    }

    // Update order total
    public bool UpdateOrderTotal(int orderId)
    {
        var order = _context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.OrderID == orderId);
        if (order == null) return false;

        order.TotalAmount = order.OrderItems.Sum(item => item.TotalAmount);
        return _context.SaveChanges() > 0;
    }
}
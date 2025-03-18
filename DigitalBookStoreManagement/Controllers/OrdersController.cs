

using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DigitalBookStoreManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [AllowAnonymous]
        [HttpGet("get-orders")]
        public ActionResult<List<Order>> GetOrders()
        {
            var order = _orderRepository.GetAllOrder();
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }


        [Authorize(Roles = "Customer , Admin")]
        [HttpGet("{orderId}")]
        public ActionResult<Order> GetOrderByOrderId(int orderId)
        {
            var order = _orderRepository.GetOrderByOrderId(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("user/{userId}")]
        public ActionResult<List<Order>> GetOrderByUserId(int userId)
        {
            var order = _orderRepository.GetOrderByUserId(userId);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }


        [Authorize(Roles = "Customer")]
        [HttpPost("place-order")]
        public ActionResult<bool> PlaceOrder(Order order)
        {
            var result = _orderRepository.PlaceOrder(order);
            if (result != null)
            {
                return CreatedAtAction(nameof(GetOrderByOrderId), new { orderId = order.OrderID }, order);
            }
            return BadRequest();
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("cancel-order/{orderId}")]
        public ActionResult<bool> CancelOrder(int orderId)
        {
            var result = _orderRepository.CancelOrder(orderId);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId}/order-status")]
        public ActionResult<Order> UpdateStatus(int orderId, [FromBody] string status)
        {
            var result = _orderRepository.UpdateStatus(orderId, status);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }


    

    }
}


using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Models;
using DigitalBookStoreManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            try
            {
                var orders = _orderRepository.GetAllOrder();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet("{orderId}")]
        public ActionResult<Order> GetOrderByOrderId(int orderId)
        {
            try
            {
                var order = _orderRepository.GetOrderByOrderId(orderId);
                if (order == null)
                {
                    return NotFound($"Order with ID {orderId} not found.");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<Order>> GetOrderByUserId(int userId)
        {
            try
            {
                var orders = _orderRepository.GetOrderByUserId(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "Customer")]
        [HttpPost("place-order")]
        public ActionResult<Order> PlaceOrder(Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Invalid order data.");
                }

                var createdOrder = _orderRepository.PlaceOrder(order);
                return CreatedAtAction(nameof(GetOrderByOrderId), new { orderId = createdOrder.OrderID }, createdOrder);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Bad request: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "Customer")]
        [HttpDelete("cancel-order/{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            try
            {
                var result = _orderRepository.CancelOrder(orderId);
                if (!result)
                {
                    return NotFound($"Order with ID {orderId} not found or already canceled.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId}/order-status")]
        public IActionResult UpdateStatus(int orderId, [FromBody] string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(status))
                {
                    return BadRequest("Order status cannot be empty.");
                }

                var result = _orderRepository.UpdateStatus(orderId, status);
                if (!result)
                {
                    return NotFound($"Order with ID {orderId} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPut("{id}/update-total")]
        public IActionResult UpdateOrderTotal(int id)
        {
            try
            {
                var updated = _orderRepository.UpdateOrderTotal(id);
                if (!updated)
                {
                    return NotFound($"Order with ID {id} not found or update failed.");
                }

                var updatedOrder = _orderRepository.GetOrderByOrderId(id);
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
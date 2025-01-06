using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using OrderAPI.DTOs;
using OrderAPI.Extensions;
using OrderAPI.Interfaces;
using OrderAPI.RabbitMQ;
using Org.BouncyCastle.Bcpg;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // [Authorize(Roles = "User")]
        [HttpGet("User")]
        public async Task<IActionResult> GetUserOrders()
        {
            var userId = User.GetUserId();
            var orders = await _orderService.GetUsersOrderAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        // [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto productDto)
        {
            var userId = User.GetUserId();
            var order = await _orderService.CreateOrderAsync(productDto, userId);
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatusUpdateDto updateDto, string id)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateOrderStatusAsync(updateDto, id);
                return Ok(updatedOrder);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("payment")]
        public async Task<bool> PaymentEvent([FromBody] PaymentEventDTO request)
        {
            // Process the orderId here (e.g., update order status, log success, etc.)
            var order = await _orderService.UpdateOrderConfirmationStatusAsync(request.orderId, request.eventType);
            return order;
        }
    }
}
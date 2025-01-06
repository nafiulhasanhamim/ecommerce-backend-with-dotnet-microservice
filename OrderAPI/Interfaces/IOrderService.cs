using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderAPI.DTOs;

namespace OrderAPI.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync();
        Task<OrderReadDto?> GetOrderByIdAsync(string orderId);
        Task<IEnumerable<OrderReadDto?>> GetUsersOrderAsync(string userId);
        Task<PaymentResponseDTO> CreateOrderAsync(OrderCreateDto orderDto, string userId);
        Task<OrderReadDto> UpdateOrderStatusAsync(OrderStatusUpdateDto updateDto, string id);
        Task<bool> UpdateOrderConfirmationStatusAsync(string id, string eventType);

        // Task<bool> EventHandling(OrderReadDto orderData, string eventType);
        // Task<bool> FinalizeOrder(OrderReadDto orderId);

        }
    }
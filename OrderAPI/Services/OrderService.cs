using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderAPI.data;
using OrderAPI.DTOs;
using OrderAPI.Interfaces;
using OrderAPI.Models;
using OrderAPI.RabbitMQ;

namespace OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;
        private readonly IPaymentService _paymentService;
        private readonly IRabbmitMQCartMessageSender _messageBus;
        public OrderService(AppDbContext context, IMapper mapper, IProductService productService, ICouponService couponService, IPaymentService paymentService, IRabbmitMQCartMessageSender messageBus)
        {
            _context = context;
            _mapper = mapper;
            _productService = productService;
            _couponService = couponService;
            _paymentService = paymentService;
            _messageBus = messageBus;
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderedProducts).ToListAsync();

            return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        }
        public async Task<PaymentResponseDTO> CreateOrderAsync(OrderCreateDto orderDto, string userId)
        {
            CouponReadDto coupon = await _couponService.GetCouponByNameAsync(orderDto.CouponName);
            OrderResultDto products = await _productService.ProcessOrderAsync(orderDto.Products);

            var totalPrice = products.TotalOrderPrice;
            if (coupon != null)
            {
                totalPrice -= coupon.DiscountPercentage / 100 * totalPrice;
            }

            var order = _mapper.Map<Order>(orderDto);
            order.OrderId = Guid.NewGuid().ToString();
            order.UserId = userId;
            order.OrderConfirmationStatus = false;
            order.OrderStatus = "pending";
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = totalPrice;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var orderedProductDto in products.OrderDetails)
            {
                var orderedProduct = new OrderedProduct
                {
                    OrderedProductId = Guid.NewGuid().ToString(),
                    OrderId = order.OrderId,
                    ProductId = orderedProductDto.ProductId,
                    Quantity = orderedProductDto.OrderedQuantity,
                    UnitProductPrice = orderedProductDto.Price,
                    DiscountPrice = orderedProductDto.Discount,
                    TotalPrice = orderedProductDto.TotalItemPrice
                };
                await _context.OrderedProducts.AddAsync(orderedProduct);
                await _context.SaveChangesAsync();
            }

            var payment = new PaymentDTO
            {
                amount = totalPrice,
                orderId = order.OrderId,
                userId = userId
            };

            var bkashUrl = await _paymentService.GetBkashUrlAsync(payment);
            return bkashUrl;
        }


        public async Task<OrderReadDto?> GetOrderByIdAsync(string orderId)
        {
            var order = await _context.Orders.Include(o => o.OrderedProducts).FirstOrDefaultAsync(p => p.OrderId == orderId);
            if (order == null) throw new KeyNotFoundException("order not found");
            return _mapper.Map<OrderReadDto>(order);
        }
        public async Task<IEnumerable<OrderReadDto?>> GetUsersOrderAsync(string userId)
        {
            var order = await _context.Orders.Include(o => o.OrderedProducts).Where(p => p.UserId == userId).ToListAsync();
            if (order == null) throw new KeyNotFoundException("order not found");
            return _mapper.Map<IEnumerable<OrderReadDto>>(order);
        }
        public async Task<OrderReadDto> UpdateOrderStatusAsync(OrderStatusUpdateDto updateDto, string id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null && order!.OrderConfirmationStatus == false)
                throw new KeyNotFoundException("Order not found.");

            order.OrderStatus = updateDto.Status;

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderReadDto>(order);
        }
        public async Task<bool> UpdateOrderConfirmationStatusAsync(string id, string eventType)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                throw new KeyNotFoundException("Order not found.");

            if (eventType == "payment_success")
            {
                order.OrderConfirmationStatus = true;
                _context.Orders.Update(order);
                _messageBus.SendMessage(new { UserId = new List<string> { }, Entity = "order", EntityId = id, Title = "Orders", Message = "New Order is placed", Whom = "Admin" }, "sentNotification", "queue");

            }
            else if (eventType == "payment_failed")
            {
                _context.Orders.Remove(order);
            }
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
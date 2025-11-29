using AutoMapper;
using Contracts.Logger;
using Contracts.IRepository;
using Entities.Exceptions;
using Entities.Model;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
using Shared.DataTransferObject.Checkout;
using Shared.DataTransferObject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public OrderService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, HttpClient httpClient, IConfiguration configuration)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await repositoryManager.Order.GetOrdersByUserIdAsync(userId);
            var ordersDto = orders.Select(orders => new OrderDto
            {
                Id = orders.Id,
                UserId = orders.UserId,
                Total = orders.Total,
                DeliveryAddress = orders.DeliveryAddress,
                Status = orders.Status.ToString()
            });
            return ordersDto;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await repositoryManager.Order.GetOrderByIdAsync(id);
            if (order is null)
                throw new OrderNotFoundException($"Order with ID {id} not found.");
            var orderDto = mapper.Map<OrderDto>(order);
            return orderDto;
        }

        public async Task UpdateOrderStatusAsync(int id, OrderStatus status)
        {
            await repositoryManager.Order.UpdateOrderStatusAsync(id, status);

        }

        public async Task<(CheckoutDto checkout1, string url)> MakeOrderAsync(AddOrderDto order, string userId)
        {
            var orderWithOrderItem = await repositoryManager.Order.MakeOrderAsync(order, userId);

            var checkoutDto = new CheckoutDto
            {
                OrderId = orderWithOrderItem.order.Id,
                Price = orderWithOrderItem.order.Total,
                Delivery = (order.PaymentMethod == "cash" ? 12.00m : 0),
                Total = orderWithOrderItem.order.Total + (order.PaymentMethod == "cash" ? 12.00m : 0),
                Items = new List<OrderItemDto>
                {
                    new OrderItemDto
                    {
                        ProductId = orderWithOrderItem.orderItem.ProductId,
                        Quantity = orderWithOrderItem.orderItem.Quantity,
                        ProductName = orderWithOrderItem.orderItem.ProductName,
                        UnitPrice = orderWithOrderItem.orderItem.UnitPrice
                    }
                },
                DeliveryAddress = order.Address,
                PaymentMethod = order.PaymentMethod,
                Status = order.PaymentMethod == "cash" ? OrderStatus.Confirmed.ToString() : OrderStatus.Pending.ToString()
            };

            if (order.PaymentMethod == "card")
            {
                var paymentUrl = await new PaymentServicePayMob.PaymobService(httpClient, configuration).GeneratePaymentUrl(orderWithOrderItem.order);
                return (checkoutDto, paymentUrl);
            }
            return (checkoutDto, null);
        }
    }
}

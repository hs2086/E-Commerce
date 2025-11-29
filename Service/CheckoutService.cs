using AutoMapper;
using Contracts.IRepository;
using Contracts.Logger;
using Entities.Model;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
using Shared.DataTransferObject.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public CheckoutService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, HttpClient httpClient, IConfiguration configuration)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<(CheckoutDto checkout1, string url)> ProcessCheckoutAsync(string userId, AddCheckoutDto checkout)
        {
            var checkoutEntity = await repositoryManager.Checkout.ProcessCheckoutAsync(userId, checkout);
            var checkoutDto = new CheckoutDto
            {
                OrderId = checkoutEntity.order.Id,
                Price = checkoutEntity.order.Total,
                Delivery = (checkout.PaymentMethod == "cash" ? 12.00m : 0),
                Total = checkoutEntity.order.Total + (checkout.PaymentMethod == "cash" ? 12.00m : 0),
                Items = checkoutEntity.orderItems.Select(x => new Shared.DataTransferObject.Order.OrderItemDto
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice
                }).ToList(),
                DeliveryAddress = checkout.DeliveryAddress,
                PaymentMethod = checkout.PaymentMethod,
                Status = checkout.PaymentMethod == "cash" ? OrderStatus.Confirmed.ToString() : OrderStatus.Pending.ToString()
            };

            if (checkout.PaymentMethod == "card")
            {
                var paymentUrl = await new PaymentServicePayMob.PaymobService(httpClient, configuration).GeneratePaymentUrl(checkoutEntity.order);
                if (paymentUrl == null)
                {
                    await repositoryManager.Checkout.CancelCheckoutAsync(checkoutEntity.order.Id, checkoutEntity.cartId);
                }
                return (checkoutDto, paymentUrl);
            }
            return (checkoutDto, null);
        }
    }
}

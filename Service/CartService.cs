using AutoMapper;
using Contracts.IRepository;
using Contracts.Logger;
using Services.Contracts;
using Shared.DataTransferObject.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CartService : ICartService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public CartService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<CartDto> AddToCartAsync(AddCartItemDto addCartItem, string userId)
        {
            var cartAndCartItemsList = await repositoryManager.Cart.AddItemToCart(addCartItem.ProductId, addCartItem.Quantity, userId);
            CartDto cartDto = new CartDto()
            {
                CartId = cartAndCartItemsList.cart.Id,
                Items = cartAndCartItemsList.cartItems
                                            .Select(x => new CartItemDto
                                            {
                                                ProductId = x.ProductId,
                                                ProductName = x.ProductName,
                                                Quantity = x.Quantity,
                                                UnitPrice = x.UnitPrice,
                                                SubTotal = x.SubTotal
                                            }).ToList(),
                TotalPrice = cartAndCartItemsList.cartItems.Sum(x => x.SubTotal)
            };

            return cartDto;
        }

        public async Task<CartDto> GetCartAsync(string userId)
        {
            var cartAndCartItemList = await repositoryManager.Cart.GetCartAsync(userId);
            CartDto cartDto = new CartDto()
            {
                CartId = cartAndCartItemList.cart.Id,
                Items = cartAndCartItemList.cartItems
                                            .Select(x => new CartItemDto
                                            {
                                                ProductId = x.ProductId,
                                                ProductName = x.ProductName,
                                                Quantity = x.Quantity,
                                                UnitPrice = x.UnitPrice,
                                                SubTotal = x.SubTotal
                                            }).ToList(),
                TotalPrice = cartAndCartItemList.cartItems.Sum(x => x.SubTotal)
            };

            return cartDto;
        }

        public async Task<CartDto> UpdateCartAsync(UpdateCartItemDto updateCartItemDto, string userId)
        {
            var cartAndCartItemList = await repositoryManager.Cart.UpdateCartAsync(updateCartItemDto.ProductId, updateCartItemDto.Quantity, userId);
            CartDto cartDto = new CartDto()
            {
                CartId = cartAndCartItemList.cart.Id,
                Items = cartAndCartItemList.cartItems
                                            .Select(x => new CartItemDto
                                            {
                                                ProductId = x.ProductId,
                                                ProductName = x.ProductName,
                                                Quantity = x.Quantity,
                                                UnitPrice = x.UnitPrice,
                                                SubTotal = x.SubTotal
                                            }).ToList(),
                TotalPrice = cartAndCartItemList.cartItems.Sum(x => x.SubTotal)
            };
            return cartDto;
        }

        public async Task<CartDto> RemoveCartItemAsync(string userId, int productId)
        {
            var cartAndCartItemList = await repositoryManager.Cart.RemoveCartItem(userId, productId);
            CartDto cartDto = new CartDto()
            {
                CartId = cartAndCartItemList.cart.Id,
                Items = cartAndCartItemList.cartItems
                                            .Select(x => new CartItemDto
                                            {
                                                ProductId = x.ProductId,
                                                ProductName = x.ProductName,
                                                Quantity = x.Quantity,
                                                UnitPrice = x.UnitPrice,
                                                SubTotal = x.SubTotal
                                            }).ToList(),
                TotalPrice = cartAndCartItemList.cartItems.Sum(x => x.SubTotal)
            };
            return cartDto;
        }

        public async Task ClearCartAsync(string userId)
        {
            await repositoryManager.Cart.ClearCartAsync(userId);
        }
    }
}

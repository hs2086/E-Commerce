using Shared.DataTransferObject.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICartService
    {
        Task<CartDto> AddToCartAsync(AddCartItemDto addCartItem, string userId);
        Task<CartDto> GetCartAsync(string userId);
        Task<CartDto> UpdateCartAsync(UpdateCartItemDto updateCartItem, string userId);
        Task<CartDto> RemoveCartItemAsync(string userId, int productId);
        Task ClearCartAsync(string userId);
    }
}

using Entities.Model;
using Shared.DataTransferObject.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface ICartRepository
    {
        Task<(Cart cart, IEnumerable<CartItem> cartItems)> AddItemToCart(int productId, int quantity, string userId);
        Task<bool> CheckAvilability(AddCartItemDto addCartItem);
        Task<(Cart cart, IEnumerable<CartItem> cartItems)> GetCartAsync(string userId);
        Task<(Cart cart, IEnumerable<CartItem> cartItems)> UpdateCartAsync(int productId, int quantity, string userId);
        Task<(Cart cart, IEnumerable<CartItem> cartItems)> RemoveCartItem(string userId, int productId);
        Task ClearCartAsync(string userId);
    }
}

using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository 
{
    public interface IWishlistRepository
    {
        Task AddWishlist(Wishlist wishlist);
        Task<Wishlist> CheckIfUserHasWishlistAsync(string userId);
        Task AddWishlistItem(WishlistItem wishlistItem);
        Task<WishlistItem> GetWishlistItemAsync(int wishlistId, int productId);
        Task DeleteWishlistItem(WishlistItem wishlistItem);
        Task<IEnumerable<WishlistItem>> GetAllWishlistForUserAsync(int wishlistId);
    }
}

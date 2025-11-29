using Shared.DataTransferObject.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IWishlistService
    {
        Task AddToWishlist(string userId, int productId);
        Task DeleteFromWishList(string userId, int productId);
        Task<WishlistDto> GetAllWishlistItems(string userId);

        Task CheckIfProductIsExistsInWishlist(string userId, int productId);
    }
}

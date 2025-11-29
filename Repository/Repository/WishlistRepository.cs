using Contracts.IRepository;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly RepositoryContext repositoryContext;

        public WishlistRepository(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        public async Task AddWishlist(Wishlist wishlist)
        {
            await repositoryContext.AddAsync(wishlist);
            await repositoryContext.SaveChangesAsync();
        }
        public async Task<Wishlist> CheckIfUserHasWishlistAsync(string userId)
        {
            return await repositoryContext.Wishlists.Where(w => w.UserId == userId).FirstOrDefaultAsync();
        }
        public async Task AddWishlistItem(WishlistItem wishlistItem)
        {
            await repositoryContext.WishlistItems.AddAsync(wishlistItem);
            await repositoryContext.SaveChangesAsync();
        }
        public async Task<WishlistItem> GetWishlistItemAsync(int wishlistId, int productId)
        {
            return await repositoryContext.WishlistItems.Where(wi => wi.WishlistId == wishlistId && wi.ProductId == productId).FirstOrDefaultAsync();
        }
        public async Task DeleteWishlistItem(WishlistItem wishlistItem)
        {
            repositoryContext.Remove(wishlistItem);
            await repositoryContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<WishlistItem>> GetAllWishlistForUserAsync(int wishlistId)
        {
            return await repositoryContext.WishlistItems.Where(wi => wi.WishlistId == wishlistId).ToListAsync();
        }
    }
}

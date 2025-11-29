using Contracts.IRepository;
using Entities.Exceptions;
using Entities.Model;
using NLog.LayoutRenderers;
using Services.Contracts;
using Shared.DataTransferObject.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class WishlistService : IWishlistService
    {
        private readonly IRepositoryManager repositoryManager;

        public WishlistService(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        public async Task AddToWishlist(string userId, int productId)
        {
            var wishlist = new Wishlist()
            {
                UserId = userId
            };
            var wishlistFounded = await repositoryManager.Wishlist.CheckIfUserHasWishlistAsync(userId);

            if (wishlistFounded == null)
                await repositoryManager.Wishlist.AddWishlist(wishlist);
            var wishlistItem = new WishlistItem()
            {
                ProductId = productId,
                WishlistId = wishlist.Id,
                CreatedAt = DateTime.Now
            };

            await repositoryManager.Wishlist.AddWishlistItem(wishlistItem);
        }


        public async Task DeleteFromWishList(string userId, int productId)
        {
            var wishlist = await repositoryManager.Wishlist.CheckIfUserHasWishlistAsync(userId);

            if (wishlist == null)
                throw new WishlistNotFoundException("This user doesn't has wishlist.");

            var product = await repositoryManager.Product.GetProductAsync(productId, false);
            if (product == null)
                throw new ProductNotFoundException($"Product with id: {productId} is not found.");

            var wishlistItem = await repositoryManager.Wishlist.GetWishlistItemAsync(wishlist.Id, productId);
            if (wishlistItem == null)
                throw new WishlistItemNotFoundException("This product doesn't exist in the wishlist");

            await repositoryManager.Wishlist.DeleteWishlistItem(wishlistItem);
        }

        public async Task<WishlistDto> GetAllWishlistItems(string userId)
        {
            var wishlist = await repositoryManager.Wishlist.CheckIfUserHasWishlistAsync(userId);
            if (wishlist == null)
                throw new WishlistNotFoundException("This user doesn't has wishlist.");

            var wishlistItems = await repositoryManager.Wishlist.GetAllWishlistForUserAsync(wishlist.Id);

            var wishlistDto = new WishlistDto()
            {
                Id = wishlist.Id,
                UserId = userId,
                Items = wishlistItems.Select(x => new WishlistItemDto()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    CreatedAt = x.CreatedAt
                }).ToList()
            };
            return wishlistDto;
        }
        public async Task CheckIfProductIsExistsInWishlist(string userId, int productId)
        {
            var wishlist = await repositoryManager.Wishlist.CheckIfUserHasWishlistAsync(userId);
            if (wishlist == null)
                throw new WishlistNotFoundException("This user doesn't has wishlist.");

            var product = await repositoryManager.Product.GetProductAsync(productId, false);
            if (product == null)
                throw new ProductNotFoundException($"Product with id: {productId} is not found.");

            var wishlistItem = await repositoryManager.Wishlist.GetWishlistItemAsync(wishlist.Id, productId);
            if (wishlistItem == null)
                throw new WishlistItemNotFoundException("This product doesn't exist in the wishlist");
        }
    }
}

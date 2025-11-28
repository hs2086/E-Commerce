using Contracts.IRepository;
using Entities.Exceptions;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repos;
using Shared.DataTransferObject.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CartRepository : RepositoryBase<CartItem>, ICartRepository
    {
        public CartRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<bool> CheckAvilability(AddCartItemDto addCartItem)
        {
            var productDb = await repositoryContext.Products.FindAsync(addCartItem.ProductId);
            if (productDb == null)
                throw new ProductNotFoundException("Product not found.");
            if (productDb.StockQuantity < addCartItem.Quantity)
                return false;

            productDb.StockQuantity -= addCartItem.Quantity;
            await repositoryContext.SaveChangesAsync();
            return true;
        }

        public async Task<(Cart cart, IEnumerable<CartItem> cartItems)> AddItemToCart(int productId, int quantity, string userId)
        {
            var productDb = await repositoryContext.Products.FindAsync(productId);
            if (productDb == null)
                throw new ProductNotFoundException($"Product with id {productId} not found.");

            var cartDb = await repositoryContext.Carts.Where(c => c.UserId == userId && c.IsActive).FirstOrDefaultAsync();
            if (cartDb == null)
            {
                cartDb = new Cart()
                {
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsActive = true,
                    UserId = userId
                };
                await repositoryContext.Carts.AddAsync(cartDb);
                await repositoryContext.SaveChangesAsync();
            }

            if (await repositoryContext.CartItems.Where(ci => ci.ProductId == productId && ci.CartId == cartDb.Id).FirstOrDefaultAsync() == null)
            {
                if (quantity > productDb.StockQuantity)
                {
                    throw new QuantityNotAvailableBadRequestException("The stock less than this quantity.");
                }
                else
                {
                    productDb.StockQuantity -= quantity;
                }
                await repositoryContext.SaveChangesAsync();
                CartItem cartItem = new CartItem()
                {
                    CartId = cartDb.Id,
                    ProductId = productDb.Id,
                    ProductName = productDb.Name,
                    Quantity = quantity,
                    UnitPrice = productDb.Price,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                Create(cartItem);
                await repositoryContext.SaveChangesAsync();
            }
            IEnumerable<CartItem> cartItemsList = await repositoryContext.CartItems.Where(ci => ci.CartId == cartDb.Id).ToListAsync();

            return (cart: cartDb, cartItems: cartItemsList);
        }

        public async Task<(Cart cart, IEnumerable<CartItem> cartItems)> GetCartAsync(string userId)
        {
            var cartDb = await repositoryContext.Carts.Where(c => c.UserId == userId && c.IsActive).FirstOrDefaultAsync();
            if (cartDb == null)
                throw new CartNotFoundException("This user don't has active cart.");
            IEnumerable<CartItem> cartItemsList = await repositoryContext.CartItems.Where(ci => ci.CartId == cartDb.Id).ToListAsync();
            return (cart: cartDb, cartItems: cartItemsList);
        }

        public async Task<(Cart cart, IEnumerable<CartItem> cartItems)> UpdateCartAsync(int productId, int quantity, string userId)
        {
            var productDb = await repositoryContext.Products.FindAsync(productId);
            if (productDb == null)
                throw new ProductNotFoundException($"Product with id {productId} not found.");

            var cartDb = await repositoryContext.Carts.Where(c => c.UserId == userId && c.IsActive).FirstOrDefaultAsync();
            if (cartDb == null)
                throw new CartNotFoundException("This user don't has active cart.");

            var cartItem = await repositoryContext.CartItems.Where(c => c.CartId == cartDb.Id && c.ProductId == productId).FirstOrDefaultAsync();
            if (cartItem == null)
                throw new CartItemNotFoundException("This product doesn't exist in the cart.");

            if (quantity == 0)
            {
                Delete(cartItem);
                productDb.StockQuantity += cartItem.Quantity;
            }
            else if (quantity > productDb.StockQuantity)
            {
                throw new QuantityNotAvailableBadRequestException("The stock less than this quantity.");
            }
            else if (quantity < cartItem.Quantity)
            {
                productDb.StockQuantity += (cartItem.Quantity - quantity);
                cartItem.Quantity = quantity;
                cartItem.UpdatedAt = DateTime.Now;
            }
            else
            {
                productDb.StockQuantity -= (quantity - cartItem.Quantity);
                cartItem.Quantity = quantity;
                cartItem.UpdatedAt = DateTime.Now;
            }
            await repositoryContext.SaveChangesAsync();


            IEnumerable<CartItem> cartItemsList = await repositoryContext.CartItems.Where(ci => ci.CartId == cartDb.Id).ToListAsync();
            return (cart: cartDb, cartItems: cartItemsList);
        }

        public async Task<(Cart cart, IEnumerable<CartItem> cartItems)> RemoveCartItem(string userId, int productId)
        {
            var productDb = await repositoryContext.Products.FindAsync(productId);
            if (productDb == null)
                throw new ProductNotFoundException($"Product with id {productId} not found.");

            var cartDb = await repositoryContext.Carts.Where(c => c.UserId == userId && c.IsActive).FirstOrDefaultAsync();
            if (cartDb == null)
                throw new CartNotFoundException("This user don't has active cart.");

            var cartItem = await repositoryContext.CartItems.Where(c => c.CartId == cartDb.Id && c.ProductId == productId).FirstOrDefaultAsync();
            if (cartItem == null)
                throw new CartItemNotFoundException("This product doesn't exist in the cart.");

            productDb.StockQuantity += cartItem.Quantity;
            cartDb.UpdatedAt = DateTime.Now;
            Delete(cartItem);
            await repositoryContext.SaveChangesAsync();

            IEnumerable<CartItem> cartItemsList = await repositoryContext.CartItems.Where(ci => ci.CartId == cartDb.Id).ToListAsync();
            return (cart: cartDb, cartItems: cartItemsList);
        }

        public async Task ClearCartAsync(string userId)
        {
            var cartDb = await repositoryContext.Carts.Where(c => c.UserId == userId && c.IsActive).FirstOrDefaultAsync();
            if (cartDb == null)
                throw new CartNotFoundException("This user don't has active cart.");

            var cartItems = await repositoryContext.CartItems.Where(c => c.CartId == cartDb.Id).ToListAsync();
            if (cartItems == null)
                throw new CartItemNotFoundException("This product doesn't exist in the cart.");

            foreach (var item in cartItems)
            {
                var productDb = await repositoryContext.Products.FindAsync(item.ProductId);
                productDb.StockQuantity += item.Quantity;
                Delete(item);
            }
            await repositoryContext.SaveChangesAsync();
        }
    }

}

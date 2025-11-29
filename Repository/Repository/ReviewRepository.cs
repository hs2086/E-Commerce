using Contracts.IRepository;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Repository.Data;
using Shared.DataTransferObject.Review;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RepositoryContext repositoryContext;

        public ReviewRepository(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public async Task AddReviewAsync(Review review)
        {
            await repositoryContext.Reviews.AddAsync(review);
            await repositoryContext.SaveChangesAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await repositoryContext.Reviews.FindAsync(id);
        }

        public async Task UpdateAsync(Review review)
        {
            repositoryContext.Update(review);
            await repositoryContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Review review)
        {
            repositoryContext.Remove(review);
            await repositoryContext.SaveChangesAsync();
        }

        public async Task<PagedList<ReviewDto>> GetAllAsync(ReviewParameters parameters)
        {
            var result = await repositoryContext.Reviews.Select(r => new ReviewDto()
            {
                Id = r.Id,
                ProductId = r.ProductId,
                UserId = r.UserId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToListAsync();
            return PagedList<ReviewDto>.ToPagedList(result, parameters.PageNumber, parameters.PageSize);
        }
    }
}

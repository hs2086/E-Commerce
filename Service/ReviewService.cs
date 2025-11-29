using AutoMapper;
using Contracts.Logger;
using Contracts.IRepository;
using Entities.Exceptions;
using Entities.Model;
using Services.Contracts;
using Shared.DataTransferObject.Review;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ReviewService : IReviewService
    {
        private readonly IRepositoryManager repositoryManager;

        public ReviewService(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        public async Task<ReviewDto> AddReviewAsync(ReviewCreateDto reviewCreateDto, string userId)
        {
            var product = await repositoryManager.Product.GetProductAsync(reviewCreateDto.ProductId, false);

            if (product == null)
                throw new ProductNotFoundException($"Product with id: {reviewCreateDto.ProductId} not found.");

            var user = await repositoryManager.Auth.GetUserByIdAsync(userId);

            var review = new Review()
            {
                ProductId = product.Id,
                ReviewerName = user?.UserName ?? "",
                UserId = userId,
                Rating = reviewCreateDto.Rating,
                Comment = reviewCreateDto.Comment,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsVerifiedBuyer = await repositoryManager.Order.CheckIfUserIsBuyTheProductWithSpecifiedIdAsync(userId, product.Id)
            };

            await repositoryManager.Review.AddReviewAsync(review);

            var reviewDto = new ReviewDto()
            {
                Id = review.Id,
                UserId = userId,
                ProductId = product.Id,
                Rating = reviewCreateDto.Rating,
                Comment = reviewCreateDto.Comment,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdatedAt
            };

            return reviewDto;
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            var review = await repositoryManager.Review.GetByIdAsync(id);
            if (review == null)
                throw new ReviewNotFoundException($"Review with id: {id} is not found.");
            return new ReviewDto()
            {
                Id = review.Id,
                ProductId = review.ProductId,
                UserId = review.UserId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdatedAt
            };
        }

        public async Task UpdateReviewAsync(int id, UpdateReviewDto updateReview)
        {
            var review = await repositoryManager.Review.GetByIdAsync(id);
            if (review == null)
                throw new ReviewNotFoundException($"Review with id: {id} is not found.");
            review.Rating = updateReview.Rating;
            review.Comment = updateReview.Comment;
            review.UpdatedAt = DateTime.Now;

            await repositoryManager.Review.UpdateAsync(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await repositoryManager.Review.GetByIdAsync(id);
            await repositoryManager.Review.DeleteAsync(review);
        }

        public async Task<(IEnumerable<ReviewDto> reviews, MetaData metaData)> GetAllReviewsAsync(ReviewParameters reviewParameters)
        {
            var reviewsWithMetaData = await repositoryManager.Review.GetAllAsync(reviewParameters);

            return (reviews: reviewsWithMetaData, metaData: reviewsWithMetaData.MetaData);
        }
    } 
}

using Shared.DataTransferObject.Review;
using Shared.DataTransferObject.User;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IReviewService  
    {
        Task<ReviewDto> AddReviewAsync(ReviewCreateDto reviewCreateDto, string userId);
        Task<ReviewDto> GetReviewByIdAsync(int id);
        Task UpdateReviewAsync(int id, UpdateReviewDto updateReviewDto);
        Task DeleteReviewAsync(int id);
        Task<(IEnumerable<ReviewDto> reviews, MetaData metaData)> GetAllReviewsAsync(ReviewParameters reviewParameters);
    }
}

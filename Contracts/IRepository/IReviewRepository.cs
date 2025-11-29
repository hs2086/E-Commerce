using Entities.Model;
using Shared.DataTransferObject.Review;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository 
{
    public interface IReviewRepository
    {
        Task AddReviewAsync(Review review);
        Task<Review> GetByIdAsync(int id);
        Task UpdateAsync(Review review);
        Task DeleteAsync(Review review);
        Task<PagedList<ReviewDto>> GetAllAsync(ReviewParameters parameters);
    }
}

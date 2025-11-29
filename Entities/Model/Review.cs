using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Review
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string ReviewerName { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public int Rating { get; set; } 
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsVerifiedBuyer { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Product Product { get; set; }    
    }
}

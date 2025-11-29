using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class WishlistItem
    {
        public int Id { get; set; }
        [ForeignKey("Wishllist")]
        public int WishlistId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Wishlist Wishlist { get; set; } 
        public Product Product { get; set; }    
    }
}

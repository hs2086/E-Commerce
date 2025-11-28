using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
            new Category
            {
                Id = 1,
                Name = "Electronics",
                Description = "Devices, gadgets, and accessories such as phones, laptops, and headphones."
            },
            new Category
            {
                Id = 2,
                Name = "Fashion",
                Description = "Clothing, shoes, and accessories for men, women, and kids."
            },
            new Category
            {
                Id = 3,
                Name = "Home & Kitchen",
                Description = "Furniture, appliances, and kitchen tools for your home."
            },
            new Category
            {
                Id = 4,
                Name = "Books",
                Description = "Fiction, non-fiction, and educational books across different genres."
            },
            new Category
            {
                Id = 5,
                Name = "Sports & Outdoors",
                Description = "Equipment and apparel for fitness, sports, and outdoor activities."
            });
        }
    }
}

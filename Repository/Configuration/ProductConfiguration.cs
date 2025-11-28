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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Name = "Smartphone X12", Description = "6.5-inch OLED display, 128GB storage, 5G support.", Price = 899.99m, StockQuantity = 25, ImagePath = "/images/products/smartphone.jpg", CategoryId = 1 },
                new Product { Id = 2, Name = "Wireless Headphones", Description = "Noise-cancelling over-ear Bluetooth headphones.", Price = 199.99m, StockQuantity = 50, ImagePath = "/images/products/headphones.jpg", CategoryId = 1 },
                new Product { Id = 3, Name = "Gaming Laptop G15", Description = "Intel i7, RTX 4060, 16GB RAM, 1TB SSD.", Price = 1499.99m, StockQuantity = 12, ImagePath = "/images/products/gaming-laptop.jpg", CategoryId = 1 },
                new Product { Id = 4, Name = "Smartwatch Pro", Description = "Heart rate monitor, GPS, and water-resistant design.", Price = 249.99m, StockQuantity = 35, ImagePath = "/images/products/smartwatch.jpg", CategoryId = 1 },
                new Product { Id = 5, Name = "Men’s Denim Jacket", Description = "Classic fit, 100% cotton denim jacket.", Price = 79.99m, StockQuantity = 40, ImagePath = "/images/products/denim-jacket.jpg", CategoryId = 2 },
                new Product { Id = 6, Name = "Women’s Sneakers", Description = "Lightweight running shoes with breathable mesh.", Price = 59.99m, StockQuantity = 60, ImagePath = "/images/products/sneakers.jpg", CategoryId = 2 },
                new Product { Id = 7, Name = "Unisex Hoodie", Description = "Soft fleece hoodie, available in multiple colors.", Price = 49.99m, StockQuantity = 80, ImagePath = "/images/products/hoodie.jpg", CategoryId = 2 },
                new Product { Id = 8, Name = "Leather Wallet", Description = "Genuine leather wallet with RFID protection.", Price = 39.99m, StockQuantity = 90, ImagePath = "/images/products/wallet.jpg", CategoryId = 2 },
                new Product { Id = 9, Name = "Air Fryer XL", Description = "Healthy cooking with little to no oil, 5L capacity.", Price = 149.99m, StockQuantity = 15, ImagePath = "/images/products/airfryer.jpg", CategoryId = 3 },
                new Product { Id = 10, Name = "Ceramic Dinner Set", Description = "16-piece dinnerware set, microwave safe.", Price = 89.99m, StockQuantity = 30, ImagePath = "/images/products/dinner-set.jpg", CategoryId = 3 },
                new Product { Id = 11, Name = "Vacuum Cleaner 3000", Description = "Bagless, high-suction vacuum cleaner.", Price = 179.99m, StockQuantity = 20, ImagePath = "/images/products/vacuum.jpg", CategoryId = 3 },
                new Product { Id = 12, Name = "Electric Kettle", Description = "1.7L stainless steel kettle with auto shut-off.", Price = 39.99m, StockQuantity = 45, ImagePath = "/images/products/kettle.jpg", CategoryId = 3 },
                new Product { Id = 13, Name = "Clean Code", Description = "A Handbook of Agile Software Craftsmanship by Robert C. Martin.", Price = 49.99m, StockQuantity = 100, ImagePath = "/images/products/clean-code.jpg", CategoryId = 4 },
                new Product { Id = 14, Name = "The Pragmatic Programmer", Description = "Your Journey to Mastery by Andrew Hunt and David Thomas.", Price = 44.99m, StockQuantity = 80, ImagePath = "/images/products/pragmatic-programmer.jpg", CategoryId = 4 },
                new Product { Id = 15, Name = "Design Patterns", Description = "Elements of Reusable Object-Oriented Software.", Price = 54.99m, StockQuantity = 70, ImagePath = "/images/products/design-patterns.jpg", CategoryId = 4 },
                new Product { Id = 16, Name = "Atomic Habits", Description = "An Easy & Proven Way to Build Good Habits and Break Bad Ones by James Clear.", Price = 29.99m, StockQuantity = 90, ImagePath = "/images/products/atomic-habits.jpg", CategoryId = 4 },
                new Product { Id = 17, Name = "Mountain Bike", Description = "21-speed bike with durable aluminum frame.", Price = 499.99m, StockQuantity = 10, ImagePath = "/images/products/mountain-bike.jpg", CategoryId = 5 },
                new Product { Id = 18, Name = "Yoga Mat", Description = "Non-slip, eco-friendly yoga mat, 6mm thickness.", Price = 29.99m, StockQuantity = 70, ImagePath = "/images/products/yoga-mat.jpg", CategoryId = 5 },
                new Product { Id = 19, Name = "Football", Description = "Professional size 5 football with durable grip.", Price = 19.99m, StockQuantity = 100, ImagePath = "/images/products/football.jpg", CategoryId = 5 },
                new Product { Id = 20, Name = "Tennis Racket", Description = "Lightweight graphite racket for advanced players.", Price = 89.99m, StockQuantity = 25, ImagePath = "/images/products/tennis-racket.jpg", CategoryId = 5 }
            );
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Embedding = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Devices, gadgets, and accessories such as phones, laptops, and headphones.", "Electronics" },
                    { 2, "Clothing, shoes, and accessories for men, women, and kids.", "Fashion" },
                    { 3, "Furniture, appliances, and kitchen tools for your home.", "Home & Kitchen" },
                    { 4, "Fiction, non-fiction, and educational books across different genres.", "Books" },
                    { 5, "Equipment and apparel for fitness, sports, and outdoor activities.", "Sports & Outdoors" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Embedding", "ImagePath", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, "6.5-inch OLED display, 128GB storage, 5G support.", null, "/images/products/smartphone.jpg", "Smartphone X12", 899.99m, 25 },
                    { 2, 1, "Noise-cancelling over-ear Bluetooth headphones.", null, "/images/products/headphones.jpg", "Wireless Headphones", 199.99m, 50 },
                    { 3, 1, "Intel i7, RTX 4060, 16GB RAM, 1TB SSD.", null, "/images/products/gaming-laptop.jpg", "Gaming Laptop G15", 1499.99m, 12 },
                    { 4, 1, "Heart rate monitor, GPS, and water-resistant design.", null, "/images/products/smartwatch.jpg", "Smartwatch Pro", 249.99m, 35 },
                    { 5, 2, "Classic fit, 100% cotton denim jacket.", null, "/images/products/denim-jacket.jpg", "Men’s Denim Jacket", 79.99m, 40 },
                    { 6, 2, "Lightweight running shoes with breathable mesh.", null, "/images/products/sneakers.jpg", "Women’s Sneakers", 59.99m, 60 },
                    { 7, 2, "Soft fleece hoodie, available in multiple colors.", null, "/images/products/hoodie.jpg", "Unisex Hoodie", 49.99m, 80 },
                    { 8, 2, "Genuine leather wallet with RFID protection.", null, "/images/products/wallet.jpg", "Leather Wallet", 39.99m, 90 },
                    { 9, 3, "Healthy cooking with little to no oil, 5L capacity.", null, "/images/products/airfryer.jpg", "Air Fryer XL", 149.99m, 15 },
                    { 10, 3, "16-piece dinnerware set, microwave safe.", null, "/images/products/dinner-set.jpg", "Ceramic Dinner Set", 89.99m, 30 },
                    { 11, 3, "Bagless, high-suction vacuum cleaner.", null, "/images/products/vacuum.jpg", "Vacuum Cleaner 3000", 179.99m, 20 },
                    { 12, 3, "1.7L stainless steel kettle with auto shut-off.", null, "/images/products/kettle.jpg", "Electric Kettle", 39.99m, 45 },
                    { 13, 4, "A Handbook of Agile Software Craftsmanship by Robert C. Martin.", null, "/images/products/clean-code.jpg", "Clean Code", 49.99m, 100 },
                    { 14, 4, "Your Journey to Mastery by Andrew Hunt and David Thomas.", null, "/images/products/pragmatic-programmer.jpg", "The Pragmatic Programmer", 44.99m, 80 },
                    { 15, 4, "Elements of Reusable Object-Oriented Software.", null, "/images/products/design-patterns.jpg", "Design Patterns", 54.99m, 70 },
                    { 16, 4, "An Easy & Proven Way to Build Good Habits and Break Bad Ones by James Clear.", null, "/images/products/atomic-habits.jpg", "Atomic Habits", 29.99m, 90 },
                    { 17, 5, "21-speed bike with durable aluminum frame.", null, "/images/products/mountain-bike.jpg", "Mountain Bike", 499.99m, 10 },
                    { 18, 5, "Non-slip, eco-friendly yoga mat, 6mm thickness.", null, "/images/products/yoga-mat.jpg", "Yoga Mat", 29.99m, 70 },
                    { 19, 5, "Professional size 5 football with durable grip.", null, "/images/products/football.jpg", "Football", 19.99m, 100 },
                    { 20, 5, "Lightweight graphite racket for advanced players.", null, "/images/products/tennis-racket.jpg", "Tennis Racket", 89.99m, 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

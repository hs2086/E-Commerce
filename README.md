# 📦 E-Commerce API  
### Online Shopping Platform — ASP.NET Core | EF Core | SQL Server | Onion Architecture

A clean, modular, and scalable **E-Commerce Web API** built with **ASP.NET Core**, following **Onion Architecture**.  
The system includes product management, shopping carts, orders, secure payment via **Paymob**, email workflows via **MailKit**, and optimized object mapping using **AutoMapper Profiles**.  
It also supports **pagination**, **search**, and **advanced filtering** for better performance and user experience.

---
## 🚀 Features

### 🛒 Core E-Commerce
- Product listing with:
  - 🔍 Search  
  - 🎯 Filtering (categories, price range, etc.)  
  - 📄 Pagination  
  - 🔄 Sorting  
- Categories & subcategories
- Shopping cart (add, update, remove)
- Wishlist functionality
- Orders (create, update status)
- Stock quantity management

### 💳 Payment
- Integrated with **Paymob** payment gateway  
- Secure payment workflow  
- Payment confirmation & callback handling

### 📧 Email Service
- Email notifications using **MailKit**
- Supports HTML templates
- Emails for:
  - Order confirmation  
  - Password reset  
  - Verification email
 
### 🧱 Architecture Overview

The project is structured following a clean and scalable architecture that separates concerns across well-defined layers. The Contracts project contains only the repository interfaces, ensuring a clear abstraction between data access and business logic. The main E-Commerce project acts solely as the entry point of the application and intentionally contains no controllers to keep it lightweight and focused on configuration and startup logic. All API endpoints are placed inside E-Commerce.Presentation, providing a dedicated and organized layer for controllers. The Entities project holds all domain models, exceptions, and error models to maintain a consistent domain structure. Data access operations are implemented in the Repository layer, which fulfills the Contracts interfaces and communicates directly with the database. The Service layer handles business logic; controllers pass DTOs to the appropriate service, which applies any pre-processing or validation before interacting with the repository. To maintain clean boundaries, service.contracts contains the interfaces for the service layer. Finally, the Shared project includes reusable components such as DTOs, validations, and feature requests, ensuring consistency across all layers. This design promotes separation of concerns, maintainability, and a clear workflow from controller → service → repository.

 ## 🛠️ Tech Stack

| Technology | Purpose |
|-----------|---------|
| **ASP.NET Core Web API** | Backend Framework |
| **Entity Framework Core** | ORM |
| **SQL Server** | Database |
| **Onion Architecture** | Project Organization |
| **AutoMapper** | Mapping Profiles |
| **MailKit** | Email Service |
| **Paymob API** | Payment Integration |

## 🔌 Integration Services

### Paymob
- Authentication token flow  
- Order registration  
- Payment key generation  
- Callback URL support  

### MailKit
- SMTP configuration  
- HTML email templates  
- Fully DI-based EmailService  

---

## 🧩 AutoMapper Profiles

Used for mapping:
- Entities → DTOs  
- DTOs → Entities  

## ⚙️ How to Run the Project
1️⃣ Clone the Repository
        git clone https://github.com/hs2086/E-Commerce.git

2️⃣ Configure Environment Variables
        - Paymob
          - APIKey
          - IFrameId
          - IntegrationId
        - Email service
          - Email
          - Password
          - Host
          - Port
          


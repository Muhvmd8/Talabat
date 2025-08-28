### Talabat Web API
📌 Project Overview

The Talabat Web API is a backend service designed using .NET 8, EF Core, SQL Server, Redis, and Identity. It follows Onion Architecture with a clean separation of concerns and implements multiple design patterns for scalability and maintainability.

This project simulates a Talabat-like e-commerce food delivery system with Product, Basket, and Order modules, along with Authentication & Authorization using JWT.

---

## 🚀 Features

### ✅ Design Patterns
- **Generic Repository Pattern**
- **Unit of Work**
- **Specification Pattern**
  - Filtering with criteria
  - Pagination
  - Sorting (`OrderBy`)
  - Searching (`search` keyword)
  - Including related entities (`Include`)

### ✅ Databases & ORMs
- **Entity Framework Core**
- **SQL Server**
- **IdentityDbContext** for authentication & roles
- **Redis (In-Memory Database)** for:
  - **Caching Service**
  - **Basket Module**

### ✅ Authentication & Authorization
- **JWT Tokens** for secure authentication
- **ASP.NET Core Identity**
- **Role-based Authorization**
- Endpoints:
  - `POST /api/auth/register` → Register new user
  - `POST /api/auth/login` → Login user
  - `GET /api/auth/current-user` → Get logged-in user
  - `GET /api/auth/user-address` → Get user’s saved address
  - `POST /api/auth/user-address` → Update user’s address
  - `GET /api/auth/check-email` → Check if email exists

### ✅ Modules

#### 🛍️ Product Module
- `GET /api/products` → Get all products (with pagination, filtering, search, sort)
- `GET /api/products/{id}` → Get product by ID
- `GET /api/products/types` → Get all product types
- `GET /api/products/brands` → Get all product brands

#### 🧺 Basket Module (with Redis)
- `GET /api/basket/{id}` → Get basket
- `POST /api/basket` → Create or update basket
- `DELETE /api/basket/{id}` → Delete basket

#### 📦 Order Module
- `GET /api/orders/{id}` → Get single order
- `GET /api/orders` → Get all user orders
- `POST /api/orders` → Create new order
- `GET /api/orders/delivery-methods` → Get available delivery methods

---

## 🛠️ Technical Features
- **Onion Architecture** for clean separation of concerns
- **Custom Middleware** for global error handling
- **Custom Attribute** for caching with Redis
- **AutoMapper** for DTO ↔ Entity mapping
- **EF Core Configurations** for entities
- **Data Seeding** from JSON (brands, products, delivery methods, types)
- **Service Manager** with factory delegate
- **Payment Integration (Stripe/Mock)** support

---

## 🎛️ Admin Dashboard (MVC)
- Manage **Products, Brands, and Types**
- Manage **Users & Roles**
  - `RolesController` with Add, Update, Delete, and Assign User to Role
- CRUD operations via **MVC Views**

---

## 📌 Tech Stack
- **.NET 8**
- **Entity Framework Core**
- **ASP.NET Core Identity**
- **SQL Server**
- **Redis**
- **AutoMapper**
- **Swagger**
- **JWT Authentication**
- **MVC (for admin dashboard)**

---

## ▶️ How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/TalabatClone-ECommerce-WebAPI.git


🏗️ Architecture Pattern

- This project is structured using Onion Architecture:

🧩 Design Patterns Implemented

- Generic Repository Pattern (CRUD abstraction)
- Unit of Work (transactional consistency)
- Specification Pattern (complex queries: filtering, sorting, pagination, include, criteria)
- Service Manager (dependency orchestration)
- Factory Pattern (ApiResponseFactory)
- Custom Middleware for error handling
- Custom Attributes for caching

🔧 Technologies Used

- .NET 8 Web API
- Entity Framework Core
- SQL Server
- Redis (Caching)
- Identity (Authentication)
- JWT (Authorization)
- AutoMapper
- Onion Architecture
- MVC for Admin Dashboard

📂 Data Seeding

- brands.json
- types.json
- products.json
- delivery.json

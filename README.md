# ECommerceMiniProject

A simple ASP.NET Core MVC e-commerce web application with user authentication, product management, shopping cart, and order processing.

## Features
- User registration and login (with roles: Admin, Customer)
- Product listing, details, and admin CRUD
- Shopping cart and checkout
- Order history for users
- Admin order management
- Session-based cart
- Responsive UI with Bootstrap

## Project Structure
- **Controllers/**: MVC controllers for account, product, cart, order, etc.
- **Models/**: Entity models (Product, Order, Category, etc.)
- **ViewModels/**: Data transfer/view models for forms and views
- **Views/**: Razor views for all pages
- **Data/**: Entity Framework Core DbContext and database initializer
- **Services/**: Business logic for cart and payment
- **Helpers/**: Session extensions
- **Migrations/**: EF Core migrations
- **wwwroot/**: Static files (CSS, JS, images)

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or update connection string for your DB)

### Setup
1. Clone the repository:
   ```sh
   git clone https://github.com/mhdnifad/ECommerceMiniProject.git
   cd ECommerceMiniProject
   ```
2. Update `appsettings.json` with your SQL Server connection string.
3. Apply migrations and seed the database:
   ```sh
   dotnet ef database update
   ```
4. Run the application:
   ```sh
   dotnet run
   ```
5. Visit `https://localhost:5001` (or the URL shown in the console).

### Default Admin Credentials
- Email: `admin@demo.com`
- Password: `admin123` (change after first login)

## Deployment
- Can be deployed to Azure, Render, or any server supporting ASP.NET Core.
- For Render: Connect your GitHub repo, set build command to `dotnet build`, and start command to `dotnet ECommerceMini.dll`.

## License
MIT License

---

For questions or contributions, open an issue or pull request on GitHub.

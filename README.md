# LMSWebAppMinimal

A minimal Library Management System (LMS) built with .NET 9, designed to demonstrate clean, maintainable, and scalable software architecture for modern web APIs.

## Main Technologies Used
- .NET 9
- ASP.NET Core Minimal APIs
- Entity Framework Core
- C# 13

## Best Practices & Principles
- Object-Oriented Programming (OOP)
- SOLID Principles
- Layered/Clean Architecture
- Separation of Concerns
- Dependency Injection

## Main Endpoint Groupings
- **Books**: Manage books (add, update, view, delete)
- **Users**: Manage users (register, update, view, delete)
- **Borrowing**: Borrow and return books, view borrowed books
- **Login**: User authentication and registration

## Main Repositories Used
- `IRepository<T>`: Generic repository for CRUD operations
- `IUnitOfWork`: Transaction management
- `IBookService`, `IUserService`, `IBorrowingService`, `ILoginService`: Service abstractions for business logic

## Usage
- All endpoints require an `X-User-Id` header for authorization.
- Use the `/api/books` endpoints for book management.
- Use the `/api/users` endpoints for user management.
- Use the `/api/borrowing` endpoints for borrowing and returning books.
- Use the `/api/login` endpoints for authentication and registration.

## Prerequisites
- .NET 9 SDK
- SQL Server (or compatible database)
- Visual Studio 2022 or later

## Setup
1. Clone the repository and navigate to the project directory.
2. Restore dependencies with `dotnet restore`.
3. Apply database migrations using Entity Framework Core tools.
4. Run the API project with `dotnet run` from the API directory.
5. Access the API endpoints as needed.
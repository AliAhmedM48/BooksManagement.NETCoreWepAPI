# 📚 Books Management [.NET Core WebAPI]

A `.NET Core Web API` project that manages books and their categories using **Onion Architecture**. This project integrates several modern technologies and design patterns such as `Entity Framework Core`, `SQL Server`, `UnitOfWork`, `Repository Pattern`, `Generic Interfaces`, `Serilog` for logging, `AutoMapper` for object mapping, and `Swagger` for API documentation.

## 📑 Table of Contents

1. [Project Overview](#project-overview)
2. [Technologies Used](#technologies-used)
3. [Architecture](#architecture)
4. [Features](#features)
5. [API Endpoints](#api-endpoints)
   - [Books API](#books-api)
   - [Category API](#category-api)
6. [Setup and Installation](#setup-and-installation)
7. [Contributing](#contributing)
8. [License](#license)

## 🌟 Project Overview

BooksManagement.NETCoreWebAPI is designed to help manage a collection of books and their categories. The system provides CRUD operations for both books and categories, adhering to best practices with clean architecture and separation of concerns.

## 💻 Technologies Used

- **.NET Core Web API**
- **Entity Framework Core**: For database interactions.
- **SQL Server**: The relational database used for storage.
- **Onion Architecture**: To ensure a clean separation of concerns.
- **Serilog**: For structured logging and monitoring.
- **AutoMapper**: For object-to-object mapping (e.g., DTOs to models).
- **UnitOfWork Pattern**: For transaction management across repositories.
- **Repository Pattern**: For encapsulating data access logic.
- **Swagger**: To generate API documentation and explore endpoints.
- **Generic Interfaces**: To make repositories reusable and scalable.

## 🏗️ Architecture

The project follows **Onion Architecture** to ensure the separation of concerns, maintainability, and testability. This is achieved by dividing the project into the following layers:

- **Core Layer**: Contains the business logic, DTOs (Data Transfer Objects), and interfaces for services and repositories.
- **Service Layer**: Implements the business logic and handles the application’s workflows.
- **Repository Layer**: Manages database interactions using the `Entity Framework Core` and `Repository Pattern`.
- **API Layer**: The presentation layer that exposes the API endpoints for client interactions.

## ✨ Features

- CRUD operations for books and categories.
- Separation of concerns using `Onion Architecture`.
- `AutoMapper` for handling DTOs and entity mapping.
- Structured logging using `Serilog`.
- `Swagger` integrated for interactive API documentation.

## 🔌 API Endpoints

### 📖 Books API

- **GET /api/book**  
  Retrieves a list of all books.

- **GET /api/book/{id}**  
  Retrieves details of a specific book by its ID.

- **POST /api/book**  
  Adds a new book.

- **PUT /api/book/{id}**  
  Updates an existing book.

- **DELETE /api/book/{id}**  
  Deletes a book by its ID.

### 🗂️ Category API

- **GET /api/category**  
  Retrieves a list of all categories.

- **GET /api/category/{id}**  
  Retrieves a specific category by its ID.

- **POST /api/category**  
  Creates a new category.

- **PUT /api/category/{id}**  
  Updates an existing category.

- **DELETE /api/category/{id}**  
  Deletes a category by its ID.

## ⚙️ Setup and Installation

Follow these steps to set up the project locally:

1. **Clone the repository**:

   ```bash
   git clone https://github.com/AliAhmedM48/BooksManagement.NETCoreWebAPI.git
   ```

2. **Install dependencies**: Navigate to the project directory and restore the necessary packages:

   ```bash
   dotnet restore
   ```

3. **Update the connection string**: Configure your SQL Server connection in the appsettings.json file.

4. **Run database migrations**: Apply the migrations to create the database schema:

   ```bash
   dotnet ef database update

   ```

5. **Run the application**: Start the API server:

   ```bash
   dotnet run
   ```

6. **Access the Swagger documentation**: Open your browser and navigate to:

   ```bash
   http://localhost:{port}/swagger
   ```

## 🤝 Contributing

Contributions are welcome! Please follow these steps to contribute:

1. **Fork** the repository.
2. **Create a new branch** for your feature or bug fix.
3. **Submit a pull request**.

## 📜 License

This project is open source and available under the **MIT License**.

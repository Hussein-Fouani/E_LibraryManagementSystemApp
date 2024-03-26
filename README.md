# E-Library Management System

This project is an E-Library Management System that provides a user-friendly interface for managing books and user profiles. The system is divided into two main components: a WPF-based desktop application for the user interface and an ASP.NET Core Web API for handling backend operations.

## Features

- **View Books**: Browse and search through the available book collection.
- **View Book Details**: Get detailed information about a specific book.
- **Add New Books**: Admins can easily add new books to the system.
- **Update Book Information**: Modify details of existing books.
- **Delete Books**: Admins can remove books from the library.
- **User Profile**: Users can view their profile information.
- **Borrow Books**: Users can borrow books from the library.

## Technologies Used

- **WPF (Windows Presentation Foundation)**: Used for creating the desktop application with a rich user interface.
- **ASP.NET Core Web API**: Powers the backend, providing RESTful services for CRUD operations.
- **Entity Framework Core**: ORM (Object-Relational Mapper) for database interactions.
- **HttpClient**: Used to communicate between the WPF application and the ASP.NET Core API.
- **Crystal Reports**: Utilized for generating reports within the system.

## Setup and Installation

1. **WPF Application Setup**:
   - Clone the repository.
   - Open the WPF project in Visual Studio.
   - Build and run the application.

2. **ASP.NET Core API Setup**:
   - Open the API project in Visual Studio.
   - Configure the database connection in the `appsettings.json` file.
   - Run migrations to create the database schema.
   - Start the API.

3. **Database Migration**:
   - Ensure that the database is migrated. Use the following commands in the Package Manager Console:
     ```
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

## API Endpoints

- **GET /api/Book**: Retrieve all books.
- **GET /api/Book/{id}**: Retrieve details of a specific book.
- **POST /api/Book**: Add a new book (Admin only).
- **PUT /api/Book/{id}**: Update information for a specific book (Admin only).
- **DELETE /api/Book/{id}**: Delete a book (Admin only).
- **GET /api/User/{id}**: Retrieve details of a specific user.
- **GET /api/User/{username}/profile**: Retrieve profile information for a specific user.
- **POST /api/Borrow/{userId}/{bookId}**: Borrow a book for a user.

## Contributing

Feel free to contribute to the development of this E-Library Management System by opening issues or creating pull requests. Follow the [contribution guidelines](CONTRIBUTING.md) for more details.

## License

This project is licensed under the [MIT License](LICENSE).

---

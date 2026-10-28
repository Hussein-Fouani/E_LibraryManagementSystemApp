# E-Library Management System

A library management system made of a WPF desktop client and an ASP.NET Core Web API. Users can sign up, sign in, browse and search books, borrow books and view the books they have borrowed; admins can add, update and delete books.

## Tech Stack

- .NET 8 (`net8.0` for the API and domain library, `net8.0-windows` for the WPF client)
- ASP.NET Core Web API with Swagger (Swashbuckle)
- Entity Framework Core 8 with SQL Server and lazy-loading proxies
- AutoMapper 13
- JWT token generation (`System.IdentityModel.Tokens.Jwt`)
- Serilog (file sink)
- WPF with `WpfAnimatedGif`, `HttpClient` and `Microsoft.AspNet.WebApi.Client`
- SAP Crystal Reports (.NET Framework 4.8 projects) for the book report

## Project Structure

| Project | Description |
| --- | --- |
| `E_LibraryApi` | ASP.NET Core Web API: controllers, `E_LibDb` DbContext, repositories, AutoMapper profile and EF Core migrations. |
| `ELibrary.Domain` | Class library with the entity models (`Book`, `BorrowedBooks`, `SignUp`, `UserRL`, `UsersRole`, `User`) and DTOs. |
| `E_LibraryManagementSystem` | WPF desktop client (sign in, sign up, dashboard, add/view/search books, borrow books, user profile). Calls the API at `http://localhost:5179/api/`. |
| `CrystalReportWpfApplication` | .NET Framework 4.8 WPF window hosting a Crystal Reports viewer, opened from the dashboard's report menu. |
| `CrystalReportsApplication` | .NET Framework 4.8 Windows Forms Crystal Reports sample. |

## Features

- Sign up (username, email, password, confirmation). New accounts get the `User` role.
- Sign in with username and password; the returned role controls what the dashboard enables.
- View all books and search by name, author, genre, ISBN, language or publication.
- Admin-only actions in the WPF client: add, update and delete books. Adding a book whose name already exists increases its number of copies.
- Borrow a book by username and book name: the number of available copies is decreased, the book is marked unavailable when no copies are left, and the return date is set to 14 days later. A user cannot borrow the same book twice.
- User profile window listing the books a user has borrowed.
- Separate `api/User` endpoints for registration and JWT login.

Note: the role checks are done in the WPF client; the API endpoints themselves are not protected by authentication.

## Getting Started

### Prerequisites

- Windows (the client is a WPF application)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB, Express or full edition)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`
- Visual Studio with SAP Crystal Reports for Visual Studio, only if you want to build the two Crystal Reports projects

### Configuration

The connection string is in `E_LibraryApi/appsettings.json`:

```json
"ConnectionStrings": {
  "Default": "Server=.;Database=ElibraryDb;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

Change `Server=` to your SQL Server instance if needed.

The JWT signing key is read from `ApiSettings:Secret`. The value in `appsettings.json` is only a placeholder; set your own key (at least 32 characters) with user secrets or an environment variable:

```bash
cd E_LibraryApi
dotnet user-secrets init
dotnet user-secrets set "ApiSettings:Secret" "<a random string of at least 32 characters>"
```

or

```powershell
$env:ApiSettings__Secret="<a random string of at least 32 characters>"
```

### Create the database

```bash
cd E_LibraryApi
dotnet ef database update
```

To give a user admin rights in the desktop client, set their `Role` to `admin` in the `UsersRole` table.

### Run

1. Start the API on `http://localhost:5179` (Swagger UI at `/swagger`):

   ```bash
   cd E_LibraryApi
   dotnet run --launch-profile http
   ```

2. Start the WPF client (from Visual Studio, or):

   ```bash
   cd E_LibraryManagementSystem
   dotnet run
   ```

## API Endpoints

Base URL: `http://localhost:5179`

### Books

| Method | Route | Description |
| --- | --- | --- |
| GET | `/api/Book/all` | Get all books. |
| GET | `/api/Book?query={text}` | Search books by name, author, genre, ISBN, language or publication. |
| POST | `/api/Book` | Add a book (or increase the copies of an existing book with the same name). |
| PUT | `/api/Book/{id}` | Update a book. |
| DELETE | `/api/Book/{id}` | Delete a book. |
| POST | `/api/Book/addborrowedbook?username={username}&BookName={bookName}` | Borrow a book for a user. |
| GET | `/api/Book/{username}` | Get the books borrowed by a user. |

### Accounts

| Method | Route | Description |
| --- | --- | --- |
| POST | `/api/SignUp` | Create an account (`username`, `password`, `confirmPassword`, `email`). |
| POST | `/api/SignIn?username={username}&password={password}` | Sign in; returns the user with its role. |
| POST | `/api/User/Register` | Register a user in the `User` table (`userName`, `name`, `password`, `role`). |
| POST | `/api/User/login` | Log in a `User` and receive a JWT. |

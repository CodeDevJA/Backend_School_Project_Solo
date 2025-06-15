# Backend School Project Solo

A robust file and folder management system built with ASP.NET Core Web API, featuring user authentication and hierarchical file organization.

## 🚀 Features

- **User Authentication & Authorization**: Secure user registration and login using ASP.NET Core Identity
- **File Management**: Upload, organize, and manage files within a folder structure
- **Folder Hierarchy**: Create nested folders with parent-child relationships
- **Database Integration**: PostgreSQL database with Entity Framework Core
- **Scalable Architecture**: Controller-Service-Repository pattern for maintainable code

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core Web API (.NET 9.0)
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core 9.0
- **Authentication**: ASP.NET Core Identity
- **API Documentation**: OpenAPI/Swagger with Scalar
- **Development Environment**: Visual Studio Code
- **Version Control**: Git & GitHub

## 📋 Prerequisites

Before running this application, ensure you have the following installed:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download) or later
- [PostgreSQL](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/)

## 🔧 Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/CodeDevJA/Backend_School_Project_Solo.git
   cd Backend_School_Project_Solo
   ```

2. **Configure the database connection**
   
   Update the connection string in `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=your_database_name;Username=your_username;Password=your_password"
     }
   }
   ```

3. **Install dependencies**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:5001` or `http://localhost:5000` (only an example, use your own).

## 📚 API Documentation

When running in development mode, you can access the interactive API documentation at:
- **Scalar UI**: `http://localhost:5000/scalar/v1`
- **OpenAPI Spec**: `http://localhost:5000/openapi/v1.json`

## 🏗️ Project Architecture

This project follows a **Controller-Service-Repository** architecture pattern:

```
├── Controllers/       # API endpoints and request handling
├── Services/          # Business logic layer
├── Repositories/      # Data access layer
├── Entities/          # Database models
├── DTOs/              # Data transfer objects
├── DbContext/         # Entity Framework context
└── CustomExceptions/  # Custom exception classes
```

### Key Components

- **Controllers**: Handle HTTP requests and responses
- **Services**: Implement business logic and orchestrate operations
- **Repository**: Manage data access and database operations
- **Entities**: Define database models and relationships
- **DTOs**: Structure request and response data

## 📊 Database Schema

### Core Entities

- **UserEntity**: Extends IdentityUser for user management
- **FolderEntity**: Represents folders with hierarchical relationships
- **FileEntity**: Represents files associated with folders

### Relationships

- Users can have multiple folders (1:N)
- Folders can contain subfolders (1:N, self-referencing)
- Folders can contain multiple files (1:N)

## 🔐 Authentication

The API uses ASP.NET Core Identity with bearer token authentication:

- User registration and login endpoints
- Protected endpoints require valid bearer tokens

## 📁 Project Structure

```
Backend_School_Project_Solo/
├── CustomExceptionClasses/
│   └── CustomExceptionClasses.cs
├── DbContext/
│   └── AppDbContext.cs
├── Entities/
│   ├── DtoModels/
│   │   ├── AuthControllerDTOs/
│   │   └── FnFMSControllerDTOs/
│   ├── FileEntity.cs
│   ├── FolderEntity.cs
│   └── UserEntity.cs
├── Services/
│   └── Controller/
├── Migrations/
├── Properties/
├── appsettings.Development.json
├── Program.cs
└── README.md
```

## 🚦 API Endpoints

### Authentication
- `POST /api/Auth/register-user`             - Register a new user
- `POST /login`                              - User login

### Folders
- `POST /api/Folder/create/root-folder`      - Create a new root folder
- `POST /api/Folder/create/folder-in-folder` - Create a new sub folder
- `PUT /api/Folder/update/name`              - Update folder
- `DELETE /api/Folder/delete`                - Delete folder

### Files
- `POST /api/File/upload`                    - Upload a file
- `GET /api/File/download/{fileId}`          - Download file
- `PUT /api/File/update-name`                - Update file name
- `DELETE /api/File/delete`                  - Delete file

*Note: Detailed API documentation is available through the Scalar interface when running the application.*

## 🔄 Development Workflow

1. **Feature Development**: Create feature branches from main
2. **Code Changes**: Follow the established architecture patterns
3. **Database Changes**: Create and apply EF Core migrations
4. **Testing**: *(Testing framework to be implemented)*
5. **Documentation**: Update API documentation as needed

## 🚧 Roadmap

- [ ] Implement comprehensive unit, integration and e2e tests
- [ ] Add frontend application
- [ ] Implement file versioning
- [ ] Add additional capabilities
- [ ] Enhance error handling and logging
- [ ] Performance optimization and caching

## 📝 License

This project is licensed under the [MIT License](LICENSE) - see the LICENSE file for details.

## 👨‍💻 Author

**Your Name**
- GitHub: [@CodeDevJA](https://github.com/CodeDevJA)

## 🙏 Acknowledgments

- ASP.NET Core documentation and community
- Entity Framework Core team
- PostgreSQL community
- Open source contributors

---

*This project is part of a school assignment focusing on backend development (ASP.NET Web Api) with modern .NET technologies.*

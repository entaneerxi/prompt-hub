# Prompt Hub

**Web Application for collecting and categorizing AI Prompts from around the world**

## Overview

Prompt Hub is a web application built with ASP.NET Core 8.0 that allows users to collect, organize, and manage AI prompts. It features a comprehensive categorization system with tags and provides search and filtering capabilities.

## Tech Stack

- **Backend**: .NET Core 8.0
- **Frontend**: Razor Pages + MVC
- **Database**: Microsoft SQL Server (MSSQL)
- **ORM**: Entity Framework Core
- **UI Framework**: Bootstrap 5

## Features

### 1. CRUD Operations for Prompts
- Create, Read, Update, and Delete AI prompts
- Each prompt includes:
  - Title
  - Content (the actual prompt text)
  - Description
  - Source URL
  - Category
  - Tags (many-to-many relationship)
  - Created Date
  - Updated Date

### 2. Category Management
- Full CRUD operations for categories
- Pre-seeded categories:
  - Finance (การเงิน)
  - IT/Technology (เทคโนโลยีสารสนเทศ)
  - Marketing (การตลาด)
  - Education (การศึกษา)
  - Creative Writing (การเขียนเขียงสร้างสรรค์)
  - Business (ธุรกิจ)
  - Health (สุขภาพ)
  - General (ทั่วไป)

### 3. Tag System
- Full CRUD operations for tags
- Many-to-many relationship between prompts and tags
- Multiple tags can be assigned to each prompt

### 4. Search & Filter
- Search prompts by title or content
- Filter by category
- Filter by tags
- Combined filtering capabilities

### 5. Responsive UI
- Card-based layout for browsing prompts
- Detailed prompt view pages
- Management interfaces for categories and tags
- Fully responsive design with Bootstrap 5

## Database Schema

### Tables

1. **Prompts**
   - Id (int, Primary Key)
   - Title (nvarchar(255), Required)
   - Content (nvarchar(max), Required)
   - Description (nvarchar(500))
   - SourceUrl (nvarchar(500))
   - CategoryId (int, Foreign Key)
   - CreatedAt (datetime)
   - UpdatedAt (datetime)

2. **Categories**
   - Id (int, Primary Key)
   - Name (nvarchar(100), Required)
   - Description (nvarchar(255))
   - CreatedAt (datetime)

3. **Tags**
   - Id (int, Primary Key)
   - Name (nvarchar(50), Required)
   - CreatedAt (datetime)

4. **PromptTags** (Junction table for many-to-many)
   - PromptId (int, Foreign Key)
   - TagId (int, Foreign Key)
   - Composite Primary Key (PromptId, TagId)

## Installation & Setup

### Prerequisites

- .NET 8.0 SDK or later
- Microsoft SQL Server (LocalDB, Express, or Full)
- Visual Studio 2022 or Visual Studio Code (optional)

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/entaneerxi/prompt-hub.git
   cd prompt-hub
   ```

2. **Update Database Connection String**
   
   Open `appsettings.json` and update the connection string to match your SQL Server instance:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PromptHubDb;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Install Dependencies**
   ```bash
   dotnet restore
   ```

4. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```
   
   This will create the database and seed it with the default categories.

5. **Run the Application**
   ```bash
   dotnet run
   ```

6. **Access the Application**
   
   Open your browser and navigate to:
   - `https://localhost:5001` (HTTPS)
   - `http://localhost:5000` (HTTP)

## Project Structure

```
PromptHub/
├── Controllers/
│   ├── HomeController.cs          # Home page controller
│   ├── PromptsController.cs       # Prompts CRUD operations
│   ├── CategoriesController.cs    # Categories management
│   └── TagsController.cs          # Tags management
├── Models/
│   ├── Prompt.cs                  # Prompt model
│   ├── Category.cs                # Category model
│   ├── Tag.cs                     # Tag model
│   └── PromptTag.cs              # Junction model for many-to-many
├── Data/
│   └── ApplicationDbContext.cs    # EF Core DbContext
├── Views/
│   ├── Home/                      # Home views
│   ├── Prompts/                   # Prompts CRUD views
│   ├── Categories/                # Categories CRUD views
│   ├── Tags/                      # Tags CRUD views
│   └── Shared/                    # Shared layout and partials
├── wwwroot/                       # Static files (CSS, JS, libraries)
├── Migrations/                    # EF Core migrations
├── appsettings.json              # Configuration
├── Program.cs                     # Application entry point
└── PromptHub.csproj              # Project file
```

## Usage

### Managing Prompts

1. **Create a New Prompt**
   - Navigate to "Prompts" > "Create New Prompt"
   - Fill in the title, content, description, and source URL
   - Select a category
   - Choose relevant tags
   - Click "Create"

2. **Search and Filter**
   - Use the search box to find prompts by title or content
   - Select a category from the dropdown to filter by category
   - Select a tag to filter by specific tag
   - Combine filters for more precise results

3. **Edit or Delete**
   - Click "Edit" on any prompt card to modify it
   - Click "Delete" to remove a prompt (with confirmation)

### Managing Categories

- Navigate to "Categories" to view all categories
- Create new categories as needed
- Edit existing categories
- Delete unused categories

### Managing Tags

- Navigate to "Tags" to view all tags
- Create new tags for better organization
- Edit existing tags
- Delete unused tags

## Development

### Adding Migrations

When you make changes to the models:

```bash
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

### Building the Project

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is open source and available under the MIT License.

## Support

For issues, questions, or contributions, please open an issue on GitHub.

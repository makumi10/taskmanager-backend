# Task Manager — Backend

The backend for the Task Management System. Built with .NET 9 Web API, following a clean layered architecture with a service layer, DTOs, and Entity Framework Core for database access.

---

## Live Demo

**API:** http://taskmanagerapp26.runasp.net/api/tasks  
**Swagger UI:** http://taskmanagerapp26.runasp.net/swagger/index.html  
**Frontend Repository:** https://github.com/makumi10/taskmanager-frontend.git

---

## Tech Stack

| Layer       | Technology                                         |
|-------------|----------------------------------------------------|
| Framework   | .NET 9 Web API                                     |
| ORM         | Entity Framework Core                              |
| Database    | SQL Server                                         |
| Docs        | Swagger / OpenAPI                                  |
| Hosting     | MonsterASP.NET (IIS)                               |

---

## Project Structure

```
TaskManager/
├── Controllers/
│   └── TasksController.cs        
├── Data/
│   └── AppDbContext.cs           
├── DTOs/
│   └── TaskDto.cs                
├── Migrations/                   
├── Models/
│   └── TaskItem.cs               
├── Services/
│   ├── ITaskService.cs           
│   └── TaskService.cs            
├── Validators/
│   └── FutureDateAttribute.cs    
├── appsettings.json              
├── Program.cs                    
└── README.md
```

### File Descriptions

- **`TasksController.cs`** — Handles all incoming HTTP requests and delegates logic to the service layer.
- **`AppDbContext.cs`** — EF Core DbContext responsible for database communication.
- **`TaskDto.cs`** — Defines the data shapes for creating, updating and returning tasks.
- **`TaskItem.cs`** — The task database entity model.
- **`ITaskService.cs`** — Service interface defining the task operations contract.
- **`TaskService.cs`** — Service implementation containing all business logic.
- **`FutureDateAttribute.cs`** — Custom validation attribute that rejects past due dates.
- **`appsettings.json`** — Base app configuration with placeholder connection string.
- **`Program.cs`** — App entry point, middleware pipeline and service registration.

---

## Prerequisites

> These are only required for **Option 2** and **Option 3**. Skip this section if you are using the live hosted API.

Before running this project locally, make sure you have the following installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (free edition is sufficient)
- [EF Core CLI Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

To install EF Core CLI tools globally, run:

```bash
dotnet tool install --global dotnet-ef
```

---

## Running the Project

There are three ways to run this project depending on your available resources:

> **No setup required?** Jump straight to Option 1 — the API is already live and ready to use.

---

### Option 1 — Use the Live Hosted API

If you don't want to set up a local environment, the API is already live and fully functional. You can interact with it directly via Swagger UI or any API client such as Postman:

- **Swagger UI:** http://taskmanagerapp26.runasp.net/swagger/index.html
- **API Base URL:** http://taskmanagerapp26.runasp.net/api/tasks

This is the recommended option if you just want to explore or test the API without any local setup.

---

### Option 2 — Run Locally with `dotnet run`

1. Clone the repository:

```bash
git clone https://github.com/makumi10/taskmanager-backend.git
cd taskmanager-backend
```

2. Create a SQL Server database:

```sql
CREATE DATABASE taskmanager;
```

3. Add your local credentials in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=taskmanager;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

4. Install dependencies:

```bash
dotnet restore
```

5. Apply migrations:

```bash
dotnet ef migrations add Init
dotnet ef database update
```

6. Run the API:

```bash
dotnet run
```

The API will be available at `https://localhost:{port}`. You can test all endpoints via Swagger UI at:

```
https://localhost:{port}/swagger
```

---

## API Endpoints

| Method | Endpoint           | Description       | Status Code    |
|--------|--------------------|-------------------|----------------|
| GET    | /api/tasks         | List all tasks    | 200 OK         |
| GET    | /api/tasks/{id}    | Get single task   | 200 OK         |
| POST   | /api/tasks         | Create a task     | 201 Created    |
| PUT    | /api/tasks/{id}    | Update a task     | 200 OK         |
| DELETE | /api/tasks/{id}    | Delete a task     | 204 No Content |

> Note: Task IDs are GUIDs (e.g. `3fa85f64-5717-4562-b3fc-2c963f66afa6`)

### Sample Request Body (POST / PUT)

```json
{
  "title": "Design system architecture",
  "description": "Define the microservices breakdown and communication patterns.",
  "status": "in_progress",
  "dueDate": "2026-03-05T23:59:59.000Z"
}
```

### Status Values

| Value        | Description             |
|--------------|-------------------------|
| pending      | Task not yet started    |
| in_progress  | Task currently underway |
| completed    | Task finished           |

---

### Option 3 — Host on Local IIS

If you prefer to host the API on your local IIS server instead of using `dotnet run`, follow these steps:

### Prerequisites

Make sure the following are enabled on your Windows machine:

- **IIS** — Enable via *Control Panel → Programs → Turn Windows features on or off → Internet Information Services*
- **ASP.NET Core Hosting Bundle** — Download and install from [dotnet.microsoft.com](https://dotnet.microsoft.com/en-us/download/dotnet/9.0). This installs the ASP.NET Core Module required for IIS to host .NET apps.

### 1. Publish the Application

In Visual Studio, right-click the project → **Publish** → **Folder** and publish to a local folder, for example:

```
C:\inetpub\taskmanager
```

Or via CLI:

```bash
dotnet publish -c Release -o C:\inetpub\taskmanager
```

### 2. Create a New Site in IIS

1. Open **IIS Manager** (search for it in the Start menu)
2. In the left panel, right-click **Sites** → **Add Website**
3. Fill in the details:
   - **Site name:** TaskManager
   - **Physical path:** `C:\inetpub\taskmanager`
   - **Port:** `8080` (or any available port)
4. Click **OK**

### 3. Set the Application Pool

1. In IIS Manager, click **Application Pools** in the left panel
2. Find the pool named **TaskManager** (created automatically)
3. Right-click it → **Basic Settings**
4. Set **.NET CLR Version** to **No Managed Code**
5. Click **OK**

### 4. Configure the Connection String

In the published folder (`C:\inetpub\taskmanager`), open `appsettings.json` and update the connection string with your local PostgreSQL credentials:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=taskmanager;Username=postgres;Password=your_password"
}
```

### 5. Set Folder Permissions

IIS needs read access to the published folder:

1. Right-click `C:\inetpub\taskmanager` → **Properties → Security**
2. Click **Edit → Add**
3. Type `IIS AppPool\TaskManager` and click **OK**
4. Grant **Read & Execute** permissions
5. Click **Apply**

### 6. Access the API

Start the site in IIS Manager and open your browser:

```
http://localhost:8080/swagger      ← Swagger UI
http://localhost:8080/api/tasks    ← API endpoint
```

> **Note:** If port 8080 is already in use, change it to any available port in the IIS site bindings.

The app uses SQL Server for both local development and production. Update the connection string in `appsettings.json` to point to your local SQL Server instance when running locally, and to your MonsterASP SQL Server instance when deploying to production.

---

## Validation Rules

- **Title** — required, maximum 255 characters
- **Status** — must be one of `pending`, `in_progress`, or `completed`
- **Due Date** — optional, but cannot be a date in the past

---

## Screenshots

### Swagger UI
![Swagger UI Screenshot](screenshots/SwaggerUI.png)

### Create Task
![Create Task Screenshot](screenshots/CreateTask.png)

### Get All Tasks
![Get All Tasks Screenshot](screenshots/GetAllTasks.png)

---

## Author

**Brian Makumi**

Practical Assignment — Health Tech Solutions  
February 2026

https://www.brianmakumi.com

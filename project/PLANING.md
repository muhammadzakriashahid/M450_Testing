# Planing
## Project Selection
- Create a Task Management App to manage tasks with states like pending, in progress, and completed
- Users can add, edit, delete tasks and view task lists. (Profile based List/Login are optional so ignore for now)
- Backend API in C# (ASP.NET Core Web API)
- Database should be SQL-Lite
- Frontend in Angular 20
## Project Structure
```
flowchart TD
    classDef frontend fill:#42b883,color:#000,stroke:#42b883
    classDef backend fill:#512bd4,color:#fff,stroke:#512bd4
    classDef database fill:#007396,color:#fff,stroke:#007396
    classDef component fill:#f4f4f4,color:#000,stroke:#666

    subgraph Frontend["Frontend Layer (Angular 20)"]
        UI[User Interface]:::frontend
        Components["Components
        • TaskList"]:::component
        Services["Services
        • TaskService"]:::frontend
    end

    subgraph Backend["Backend Layer (ASP.NET Core)"]
        API[API Endpoints]:::backend
        Controllers["Controllers
        • TaskController"]:::component
        BusinessLogic["Services
        • TaskService"]:::backend
        Repositories["Repositories
        • TaskRepository"]:::backend
    end

    subgraph Data["Data Layer"]
        DB[(SQLite Database)]:::database
    end

    UI --> Components
    Components --> Services
    Services <--> API
    API --> Controllers
    Controllers --> BusinessLogic
    BusinessLogic --> Repositories
    Repositories <--> DB
```
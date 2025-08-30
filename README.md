# CSV Data Management Application

An ASP.NET MVC application that allows users to upload CSV files, store the data in a Microsoft SQL Server database, and interact with the stored data directly from the UI. The app supports filtering, sorting, inline editing, and record deletion.

## Features

* Upload CSV files with fields: **Name, Date of Birth, Married, Phone, Salary**.
* Storage of uploaded data in **MS SQL Server**.
* Client-side filtering and sorting by any column using JavaScript.
* Inline editing of table rows with real-time updates.
* Ability to delete records from the database.
* Basic error handling and validation for input data.

## Technology Stack

* **Frontend:** JavaScript, HTML, CSS
* **Backend:** ASP.NET Core MVC, C#, Entity Framework Core
* **Database:** Microsoft SQL Server (MSSQL)
* **Architecture:** Clean Architecture with layered structure (API, Domain, Application, Persistence, Infrastructure, UI)
* **CSV Processing:** CsvHelper library for parsing

## Architecture Overview

* **API Layer:** Handles HTTP endpoints and SignalR hubs for real-time communication.
* **Domain Layer:** Core entities.
* **Application Layer:** Implements CQRS with MediatR handlers.
* **Persistence Layer:** Manages data storage and access using Entity Framework Core.
* **Infrastructure Layer:** Integrations CsvHelper
* **UI Layer:** Frontend components with dynamic updates for gameplay.

## How to Run the Project Locally

### Getting Started

1. Clone the repository to your local machine.

```bash
git clone https://github.com/Mkrager/Contact-Manager.git
```

2. Set up your development environment. Make sure you have the necessary tools and packages installed.

3. Configure the project settings and dependencies. You may need to create configuration files for sensitive information like API keys and database connection strings.

4. Install the required packages using your package manager of choice (e.g., npm, yarn, NuGet).

5. Run the application locally for development and testing.

```bash
dotnet run
```

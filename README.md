<p align="center">
 <img width="100" height="100" alt="Image" src="https://github.com/user-attachments/assets/9060d032-7794-445e-83a9-2c9dca26fc23" />
</p>


<h1 align="center">XBLog - Blog Management System</h1>


<p align="center">
  A modern blog management system built with <b>ASP.NET Core MVC</b> following the <b>Onion Architecture</b> principles.
</p>

<p align="center">

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?style=for-the-badge&logo=.net)
![C#](https://img.shields.io/badge/C%23-Language-239120?style=for-the-badge&logo=c-sharp)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-ORM-6DB33F?style=for-the-badge)
![FluentValidation](https://img.shields.io/badge/FluentValidation-Validation-orange?style=for-the-badge)

</p>

---

# 📖 Overview

Blog Management System is a portfolio project developed with **ASP.NET Core MVC** using **Onion Architecture**.

The application allows administrators to create and manage blog articles while authenticated users can interact with published content by posting comments.

This project was developed as a portfolio application to demonstrate practical experience in ASP.NET Core MVC, Onion Architecture, Entity Framework Core, Cookie Authentication, and modern software development practices.

---

# ✨ Features

- 🔐 Cookie-Based Authentication
- 👤 User Registration & Login
- 🛡️ Role-Based Authorization
- 📝 Create, Edit, Delete & Publish Articles
- 💬 Comment System (Authenticated Users Only)
- 📂 Category Management
- 📄 Server-Side Pagination
- ✅ FluentValidation
- 🏗️ Onion Architecture
- 💾 SQL Server Database
- ⚡ Entity Framework Core
- 🎨 Responsive User Interface

---

# 🏛️ Architecture

The project follows the **Onion Architecture** pattern to keep the application maintainable and scalable.

<p align="center">
 <img width="900" height="400" alt="Image" src="https://github.com/user-attachments/assets/7e0c0aa1-2a75-4eef-ac6b-e79e5d9edbed" />
</p>

### Project Layers

- **Presentation**
  - Controllers
  - Views
  - ViewModels

- **Application**
  - Services
  - Contracts
  - DTOs
  - Business Logic

- **Domain**
  - Entities
  - Interfaces
  - Domain Rules

- **Infrastructure**
  - Entity Framework Core
  - SQL Server
  - Repositories
  - Authentication

---

# 📸 Application Screenshots

## 🏠 Home Page

<p align="center">
 <img width="900" height="400" alt="Image" src="https://github.com/user-attachments/assets/6c5bc89d-2771-4b20-8b8d-015b6810fd80" />
</p>

---

## 📄 Blog Details

<p align="center">
  <img width="900" height="400" alt="Image" src="https://github.com/user-attachments/assets/72d81ce7-ed29-4826-89ec-b9719e498762" />
</p>

---

## 🔑 Login

<p align="center">
 <img width="900" height="400" alt="Image" src="https://github.com/user-attachments/assets/6c7672c2-95ee-4a40-b772-aaa4064faeb2" />
</p>

---

## 📝 Register

<p align="center">
 <img width="900" height="400" alt="Image" src="https://github.com/user-attachments/assets/3cd98442-a563-4c05-8a64-bbab2ae191e2" />
</p>

---

## 💬 Comments

<p align="center">
  <img width="900" height="400" alt="Image" src="https://github.com/user-attachments/assets/884cd8e8-2b46-4cc1-a113-e6984b31800d" />
</p>

---

## ⚙️ Admin Dashboard

<p align="center">
  <img width="900" height="400" alt="Image" src="https://github.com/user-attachments/assets/cdc97124-1131-4c9a-b55f-843e4c41de74" />
</p>

---

## ✍️ Create Article

<p align="center">
  <img width="900" height="400" alt="Image" src="https://github.com/user-attachments/assets/53b18509-b2fb-40c2-98cd-4ca83bfb734f" />

</p>

---

## 📂 Category Management

<p align="center">
  <img width="900" height="400" alt="Image" src="https://github.com/user-attachments/assets/59bfbd14-16bf-46ad-ac3e-fb50e9ae16fd" />
</p>

---

# 🛠️ Technologies

- ASP.NET Core MVC
- C#
- Entity Framework Core
- Microsoft SQL Server
- Cookie Authentication
- Onion Architecture
- FluentValidation
- Bootstrap 5
- HTML5
- CSS3
- JavaScript

---

# 📂 Project Structure

```text
BlogManagementSystem
│
├── XBLog.Web
├── XBLog.Infrastructure
├── XBLog.Application
└── XBLog.Domain

```

---

# 🚀 Getting Started

## Clone Repository

```bash
git clone https://github.com/YourUsername/BlogManagementSystem.git
```

## Navigate to Project

```bash
cd BlogManagementSystem
```

## Update Connection String

Open **appsettings.json** and configure your SQL Server connection string.

## Apply Migrations

```bash
dotnet ef database update
```

## Run Application

```bash
dotnet run
```

---

# 💡 Skills Demonstrated

- ASP.NET Core MVC
- Onion Architecture
- Cookie Authentication
- Entity Framework Core
- FluentValidation
- Repository Pattern
- Dependency Injection
- SQL Server
- Pagination
- Clean Code Principles

---

# 📌 Future Improvements

- Image Upload
- Search Functionality
- Tags
- Rich Text Editor
- User Profile
- REST API
- Docker Support
- Unit Testing
- Logging (Serilog)

---

# 📄 License

This project is available under the MIT License.

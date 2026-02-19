# Property Management System (PMS)

A comprehensive multi-role Property Management System built with ASP.NET Core Razor Pages that enables Property Owners (Admins), Agents, and Tenants to efficiently manage properties, rooms, payments, and maintenance issues.

## 🚀 Features

### Role-Based Access Control
- **Admin (Property Owner)**: Full system access with cross-agent reporting and audit capabilities
- **Agent**: Manage assigned properties, rooms, tenants, and payments
- **Tenant**: View rental details, make payments, report issues, and track payment history

### Core Functionality
- ✅ User authentication and authorization with ASP.NET Core Identity
- ✅ Property and room management
- ✅ Tenant management with lease tracking
- ✅ Payment processing and tracking
- ✅ Issue reporting with image attachments
- ✅ Role-based dashboards with KPIs
- ✅ Audit logging for critical operations
- ✅ Property-Agent assignments with date tracking

## 📸 Screenshots

### Login Page
![Login Page](https://github.com/user-attachments/assets/87233435-bc51-4405-8f44-cf3d2d7ead73)

### Admin Dashboard
![Admin Dashboard](https://github.com/user-attachments/assets/91e3bba4-df72-4898-b81b-7f6b1f160310)

## 🏗️ Architecture

The solution follows a clean architecture pattern with the following projects:

```
PropertyManagementSystem/
├── PMS.Web/              # ASP.NET Core Razor Pages application
├── PMS.Models/           # Domain models, DTOs, and ViewModels
├── PMS.Data/             # Data access layer with Entity Framework Core
└── PMS.Utilities/        # Shared utilities and constants
```

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8.0 (Razor Pages)
- **Authentication**: ASP.NET Core Identity
- **Database**: MySQL/MariaDB with Entity Framework Core (Code-First)
- **Alternative**: In-Memory Database for development/testing
- **ORM**: Entity Framework Core 8.0
- **UI**: Bootstrap 5
- **Database Provider**: Pomelo.EntityFrameworkCore.MySql

## 📦 Prerequisites

- .NET 8.0 SDK or later
- MySQL/MariaDB (optional - can use in-memory database)
- Visual Studio 2022 / VS Code / Rider

## 🚦 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Destiny4R/Property_Management_Systems.git
cd Property_Management_Systems
```

### 2. Configuration

Update `appsettings.json` in the `PMS.Web` project:

```json
{
  "UseInMemoryDatabase": true,  // Set to false for MySQL
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PropertyManagement;User=root;Password=yourpassword;"
  }
}
```

### 3. Build the Solution

```bash
dotnet build
```

### 4. Run the Application

```bash
cd PMS.Web
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`

### 5. Default Login Credentials

```
Email: admin@pms.com
Password: Admin@123
```

## 📊 Database Schema

### Core Entities

- **ApplicationUser**: Extended Identity user with custom properties
- **Property**: Properties managed by the system
- **PropertyAssignment**: Links agents to properties with date tracking
- **Room**: Individual rooms within properties
- **Tenant**: Tenant information linked to users and rooms
- **Payment**: Payment records with full audit trail
- **IssueTicket**: Maintenance and issue tracking
- **Attachment**: File attachments for issue tickets
- **AuditLog**: System-wide audit trail

## 🔐 Security Features

- Password hashing with ASP.NET Core Identity
- Role-based authorization (Admin, Agent, Tenant)
- Secure session management
- PCI-compliant payment handling (tokens only, no raw card data)
- Audit logging for critical operations

## 🎯 Roadmap

### MVP (Current Implementation)
- ✅ Core authentication and authorization
- ✅ Basic dashboards for all roles
- ✅ Data models and database schema
- ⏳ Complete CRUD operations for all entities
- ⏳ Payment gateway integration (Paystack)
- ⏳ Email notifications
- ⏳ File upload for issue attachments
- ⏳ Advanced reporting and analytics

### Future Enhancements
- Automatic rent reminders (email/SMS)
- Lease management with escalation schedules
- Bank reconciliation
- Export to accounting systems
- Multi-currency support
- Advanced analytics and KPI dashboards
- Mobile application

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📝 License

This project is licensed under the MIT License.

## 👥 Authors

- **Destiny4R** - Initial work

## 🙏 Acknowledgments

- ASP.NET Core team for the excellent framework
- Bootstrap for the UI components
- Entity Framework Core team for the ORM

## 📞 Support

For support, please open an issue in the GitHub repository.

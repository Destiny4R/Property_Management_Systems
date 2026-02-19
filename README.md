# Property Management System (PMS)

A comprehensive web-based Property Management System built with ASP.NET Core 8.0 Razor Pages, designed for property owners, agents, and tenants to manage properties, rooms, payments, and maintenance efficiently.

## 🎯 Features

### Multi-Role Support
- **Admin** - Full system access with property, agent, and tenant management
- **Agent** - Manage assigned properties, rooms, tenants, and payments
- **Tenant** - View room details, make payments, and report maintenance issues

### Core Functionality
- ✅ Role-based access control (RBAC) with ASP.NET Core Identity
- ✅ Property and room management
- ✅ Agent assignment to properties
- ✅ Tenant management with lease tracking
- ✅ Payment processing and history
- ✅ Issue/maintenance ticket system with attachment support
- ✅ Comprehensive audit logging
- ✅ Real-time dashboards with KPIs

## 🏗️ Architecture

The project follows a clean architecture pattern with separation of concerns:

```
├── PMS.Web/                    # ASP.NET Core Razor Pages application
│   ├── Pages/                  # Razor Pages (UI)
│   │   ├── Account/           # Authentication pages
│   │   ├── Admin/             # Admin dashboard and features
│   │   ├── Agent/             # Agent dashboard and features
│   │   └── Tenant/            # Tenant dashboard and features
│   └── wwwroot/               # Static files (CSS, JS, images)
├── PMS.Models/                 # Domain models, ViewModels, DTOs
│   ├── Models/                # Entity models
│   ├── ViewModels/            # View models for UI
│   └── DTOs/                  # Data transfer objects
├── PMS.DataAccess/            # Data access layer
│   ├── Data/                  # DbContext and database seeding
│   └── Migrations/            # EF Core migrations
└── PMS.Utilities/             # Utility functions and helpers
```

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- SQLite (for development) or SQL Server (for production)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/Destiny4R/Property_Management_Systems.git
cd Property_Management_Systems
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build the solution:
```bash
dotnet build
```

4. Run database migrations:
```bash
cd PMS.Web
dotnet ef database update --project ../PMS.DataAccess/PMS.DataAccess.csproj
```

5. Run the application:
```bash
cd PMS.Web
dotnet run
```

The application will start at `https://localhost:5001` (or `http://localhost:5000`)

### Default Credentials

The system comes pre-seeded with test accounts:

**Admin Account**
- Email: `admin@pms.com`
- Password: `Admin@123`

**Agent Account**
- Email: `agent@pms.com`
- Password: `Agent@123`

**Tenant Account**
- Must be created by Admin or Agent

## 📊 Database Schema

The system uses the following core entities:

- **ApplicationUser** - Extended Identity user with role and status
- **Property** - Properties managed in the system
- **PropertyAssignment** - Agent-to-property assignments
- **Room** - Individual rooms within properties
- **Tenant** - Tenant information and lease details
- **Payment** - Payment records with full audit trail
- **IssueTicket** - Maintenance/issue reports
- **Attachment** - File attachments for issues
- **AuditLog** - System-wide audit logging

## 🔐 Security Features

- Password complexity requirements
- Role-based authorization
- Session management
- SQL injection prevention (EF Core parameterized queries)
- XSS protection (Razor Pages encoding)
- CSRF protection (built-in anti-forgery tokens)

## 🎨 Technology Stack

- **Framework**: ASP.NET Core 8.0
- **UI**: Razor Pages with Bootstrap 5
- **ORM**: Entity Framework Core 8.0
- **Database**: SQLite (Development) / SQL Server (Production)
- **Authentication**: ASP.NET Core Identity
- **Frontend**: jQuery, Bootstrap 5

## 📝 Planned Features

- [ ] Admin CRUD operations for Properties, Agents, and Tenants
- [ ] Agent room and tenant management interfaces
- [ ] Payment gateway integration (Paystack)
- [ ] Email notifications with HTML templates
- [ ] PDF receipt generation
- [ ] Advanced reporting and analytics
- [ ] Multi-property tenant support
- [ ] Lease renewal workflows
- [ ] SMS notifications
- [ ] Export to CSV/Excel
- [ ] Multi-tenant (SaaS) support

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👥 Authors

- **Destiny4R** - Initial work

## 🙏 Acknowledgments

- Built following ASP.NET Core best practices
- Inspired by real-world property management needs
- Designed with scalability and maintainability in mind

## 📞 Support

For support, email support@pms.com or open an issue in the repository.

---

**Note**: This is an MVP (Minimum Viable Product) implementation. Additional features and enhancements are planned for future releases.

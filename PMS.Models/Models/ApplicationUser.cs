using Microsoft.AspNetCore.Identity;

namespace PMS.Models.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public new string? PhoneNumber { get; set; }
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum UserRole
{
    Admin = 1,
    Agent = 2,
    Tenant = 3
}

public enum UserStatus
{
    Active = 1,
    Suspended = 2,
    Disabled = 3
}

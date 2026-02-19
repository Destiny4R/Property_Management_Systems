using Microsoft.AspNetCore.Identity;

namespace PMS.Models.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName => $"{FirstName} {LastName}".Trim();
    public string? Phone { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum UserStatus
{
    Active,
    Suspended,
    Disabled
}

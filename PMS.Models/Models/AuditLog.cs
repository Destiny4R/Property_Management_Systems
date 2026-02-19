namespace PMS.Models.Models;

public class AuditLog
{
    public int Id { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string PerformedByUserId { get; set; } = string.Empty;
    public string? DetailsJson { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Navigation property
    public ApplicationUser PerformedByUser { get; set; } = null!;
}

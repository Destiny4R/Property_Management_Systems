using System.ComponentModel.DataAnnotations;

namespace PMS.Models.Models;

public class AuditLog
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string EntityType { get; set; } = string.Empty;
    
    [Required]
    public string EntityId { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string Action { get; set; } = string.Empty;
    
    [Required]
    public string PerformedByUserId { get; set; } = string.Empty;
    
    [StringLength(2000)]
    public string? DetailsJson { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

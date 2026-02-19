using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Models.Models;

public class Tenant
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int RoomId { get; set; }
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    public DateTime LeaseStartDate { get; set; }
    
    public DateTime LeaseEndDate { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? RentAmountOverride { get; set; }
    
    [StringLength(100)]
    public string? NextOfKinName { get; set; }
    
    [StringLength(20)]
    public string? NextOfKinPhone { get; set; }
    
    [StringLength(100)]
    public string? EmergencyContactName { get; set; }
    
    [StringLength(20)]
    public string? EmergencyContactPhone { get; set; }
    
    public TenantStatus Status { get; set; } = TenantStatus.Active;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; } = null!;
    
    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; } = null!;
    
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<IssueTicket> IssueTickets { get; set; } = new List<IssueTicket>();
}

public enum TenantStatus
{
    Active = 1,
    Suspended = 2,
    Disabled = 3
}

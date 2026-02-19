using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Models.Models;

public class Payment
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int TenantId { get; set; }
    
    [Required]
    public int RoomId { get; set; }
    
    [Required]
    public int PropertyId { get; set; }
    
    [Required]
    public string AgentId { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    
    [StringLength(10)]
    public string Currency { get; set; } = "NGN";
    
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    
    [StringLength(100)]
    public string? GatewayTxnId { get; set; }
    
    [StringLength(50)]
    public string? Gateway { get; set; }
    
    [StringLength(4)]
    public string? Last4 { get; set; }
    
    [StringLength(100)]
    public string? TokenId { get; set; }
    
    [StringLength(50)]
    public string PaymentMethod { get; set; } = "Cash";
    
    [StringLength(100)]
    public string? Reference { get; set; }
    
    public string? RecordedByUserId { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    [ForeignKey("TenantId")]
    public virtual Tenant Tenant { get; set; } = null!;
    
    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; } = null!;
    
    [ForeignKey("PropertyId")]
    public virtual Property Property { get; set; } = null!;
    
    [ForeignKey("AgentId")]
    public virtual ApplicationUser Agent { get; set; } = null!;
    
    [ForeignKey("RecordedByUserId")]
    public virtual ApplicationUser? RecordedBy { get; set; }
}

public enum PaymentStatus
{
    Pending = 1,
    Completed = 2,
    Failed = 3,
    Refunded = 4,
    Adjusted = 5
}

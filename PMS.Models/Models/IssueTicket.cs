using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Models.Models;

public class IssueTicket
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
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;
    
    public IssueStatus Status { get; set; } = IssueStatus.Open;
    
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
    
    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}

public enum IssueStatus
{
    Open = 1,
    InProgress = 2,
    Closed = 3
}

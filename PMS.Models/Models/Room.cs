using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Models.Models;

public class Room
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int PropertyId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string RoomNumber { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string? Floor { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal RentAmount { get; set; }
    
    public RoomStatus Status { get; set; } = RoomStatus.Available;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    [ForeignKey("PropertyId")]
    public virtual Property Property { get; set; } = null!;
    
    public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<IssueTicket> IssueTickets { get; set; } = new List<IssueTicket>();
}

public enum RoomStatus
{
    Available = 1,
    Occupied = 2,
    Suspended = 3,
    Disabled = 4
}

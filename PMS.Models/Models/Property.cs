using System.ComponentModel.DataAnnotations;

namespace PMS.Models.Models;

public class Property
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    public string Address { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    public virtual ICollection<PropertyAssignment> PropertyAssignments { get; set; } = new List<PropertyAssignment>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<IssueTicket> IssueTickets { get; set; } = new List<IssueTicket>();
}

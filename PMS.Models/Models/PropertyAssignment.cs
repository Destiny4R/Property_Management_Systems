using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Models.Models;

public class PropertyAssignment
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int PropertyId { get; set; }
    
    [Required]
    public string AgentId { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? EndDate { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public string AssignedByAdminId { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    [ForeignKey("PropertyId")]
    public virtual Property Property { get; set; } = null!;
    
    [ForeignKey("AgentId")]
    public virtual ApplicationUser Agent { get; set; } = null!;
    
    [ForeignKey("AssignedByAdminId")]
    public virtual ApplicationUser AssignedBy { get; set; } = null!;
}

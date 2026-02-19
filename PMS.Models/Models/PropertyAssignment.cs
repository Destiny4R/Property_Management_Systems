namespace PMS.Models.Models;

public class PropertyAssignment
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string AgentId { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string AssignedByAdminId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Property Property { get; set; } = null!;
    public ApplicationUser Agent { get; set; } = null!;
    public ApplicationUser AssignedByAdmin { get; set; } = null!;
}

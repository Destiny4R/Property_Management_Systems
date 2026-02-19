namespace PMS.Models.Models;

public class Property
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<PropertyAssignment> PropertyAssignments { get; set; } = new List<PropertyAssignment>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<IssueTicket> IssueTickets { get; set; } = new List<IssueTicket>();
}

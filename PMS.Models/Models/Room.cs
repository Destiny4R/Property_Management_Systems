namespace PMS.Models.Models;

public class Room
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string? Floor { get; set; }
    public decimal RentAmount { get; set; }
    public RoomStatus Status { get; set; } = RoomStatus.Available;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public Property Property { get; set; } = null!;
    public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<IssueTicket> IssueTickets { get; set; } = new List<IssueTicket>();
}

public enum RoomStatus
{
    Available,
    Occupied,
    Suspended,
    Disabled
}

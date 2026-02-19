namespace PMS.Models.Models;

public class IssueTicket
{
    public int Id { get; set; }
    public int TenantId { get; set; }
    public int RoomId { get; set; }
    public int PropertyId { get; set; }
    public string AgentId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public Tenant Tenant { get; set; } = null!;
    public Room Room { get; set; } = null!;
    public Property Property { get; set; } = null!;
    public ApplicationUser Agent { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}

public enum TicketStatus
{
    Open,
    InProgress,
    Closed
}

namespace PMS.Models.Models;

public class Attachment
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public IssueTicket Ticket { get; set; } = null!;
}

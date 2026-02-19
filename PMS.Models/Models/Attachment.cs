using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Models.Models;

public class Attachment
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int TicketId { get; set; }
    
    [Required]
    [StringLength(500)]
    public string FilePath { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string? FileType { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    [ForeignKey("TicketId")]
    public virtual IssueTicket IssueTicket { get; set; } = null!;
}

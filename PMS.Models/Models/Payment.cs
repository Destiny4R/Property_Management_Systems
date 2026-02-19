namespace PMS.Models.Models;

public class Payment
{
    public int Id { get; set; }
    public int TenantId { get; set; }
    public int RoomId { get; set; }
    public int PropertyId { get; set; }
    public string AgentId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "NGN";
    public DateTime PaymentDate { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? GatewayTxnId { get; set; }
    public string? PaymentMethod { get; set; }
    public string? PaymentReference { get; set; }
    public string? Last4 { get; set; }
    public string? TokenId { get; set; }
    public string RecordedByUserId { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public Tenant Tenant { get; set; } = null!;
    public Room Room { get; set; } = null!;
    public Property Property { get; set; } = null!;
    public ApplicationUser Agent { get; set; } = null!;
    public ApplicationUser RecordedByUser { get; set; } = null!;
}

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed,
    Refunded,
    Adjusted
}

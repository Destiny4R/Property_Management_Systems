using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMS.Models.Models;

namespace PMS.Data.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyAssignment> PropertyAssignments { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<IssueTicket> IssueTickets { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Property configuration
        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(1000);
        });

        // PropertyAssignment configuration
        modelBuilder.Entity<PropertyAssignment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Property)
                .WithMany(p => p.PropertyAssignments)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Agent)
                .WithMany()
                .HasForeignKey(e => e.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.AssignedByAdmin)
                .WithMany()
                .HasForeignKey(e => e.AssignedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => new { e.PropertyId, e.AgentId });
        });

        // Room configuration
        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RoomNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Floor).HasMaxLength(50);
            entity.Property(e => e.RentAmount).HasColumnType("decimal(18,2)");

            entity.HasOne(e => e.Property)
                .WithMany(p => p.Rooms)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.PropertyId, e.RoomNumber }).IsUnique();
        });

        // Tenant configuration
        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NextOfKinName).HasMaxLength(200);
            entity.Property(e => e.NextOfKinPhone).HasMaxLength(50);
            entity.Property(e => e.EmergencyContactName).HasMaxLength(200);
            entity.Property(e => e.EmergencyContactPhone).HasMaxLength(50);
            entity.Property(e => e.RentAmountOverride).HasColumnType("decimal(18,2)");

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Room)
                .WithMany(r => r.Tenants)
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.RoomId);
        });

        // Payment configuration
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Currency).IsRequired().HasMaxLength(10);
            entity.Property(e => e.GatewayTxnId).HasMaxLength(200);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentReference).HasMaxLength(200);
            entity.Property(e => e.Last4).HasMaxLength(4);
            entity.Property(e => e.TokenId).HasMaxLength(200);
            entity.Property(e => e.Notes).HasMaxLength(1000);

            entity.HasOne(e => e.Tenant)
                .WithMany(t => t.Payments)
                .HasForeignKey(e => e.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Room)
                .WithMany(r => r.Payments)
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Property)
                .WithMany(p => p.Payments)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Agent)
                .WithMany()
                .HasForeignKey(e => e.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.RecordedByUser)
                .WithMany()
                .HasForeignKey(e => e.RecordedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.TenantId);
            entity.HasIndex(e => e.RoomId);
            entity.HasIndex(e => e.PropertyId);
            entity.HasIndex(e => e.AgentId);
            entity.HasIndex(e => e.PaymentDate);
            entity.HasIndex(e => e.Status);
        });

        // IssueTicket configuration
        modelBuilder.Entity<IssueTicket>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(2000);

            entity.HasOne(e => e.Tenant)
                .WithMany(t => t.IssueTickets)
                .HasForeignKey(e => e.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Room)
                .WithMany(r => r.IssueTickets)
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Property)
                .WithMany(p => p.IssueTickets)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Agent)
                .WithMany()
                .HasForeignKey(e => e.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.TenantId);
            entity.HasIndex(e => e.PropertyId);
            entity.HasIndex(e => e.Status);
        });

        // Attachment configuration
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FilePath).IsRequired().HasMaxLength(500);
            entity.Property(e => e.FileType).IsRequired().HasMaxLength(50);

            entity.HasOne(e => e.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(e => e.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // AuditLog configuration
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EntityType).IsRequired().HasMaxLength(100);
            entity.Property(e => e.EntityId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Action).IsRequired().HasMaxLength(100);

            entity.HasOne(e => e.PerformedByUser)
                .WithMany()
                .HasForeignKey(e => e.PerformedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.EntityType);
            entity.HasIndex(e => e.EntityId);
            entity.HasIndex(e => e.Timestamp);
        });
    }
}

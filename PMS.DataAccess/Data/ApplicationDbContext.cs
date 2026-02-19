using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMS.Models.Models;

namespace PMS.DataAccess.Data;

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

        // Configure relationships and constraints
        modelBuilder.Entity<PropertyAssignment>()
            .HasOne(pa => pa.Property)
            .WithMany(p => p.PropertyAssignments)
            .HasForeignKey(pa => pa.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PropertyAssignment>()
            .HasOne(pa => pa.Agent)
            .WithMany()
            .HasForeignKey(pa => pa.AgentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Room>()
            .HasOne(r => r.Property)
            .WithMany(p => p.Rooms)
            .HasForeignKey(r => r.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Tenant>()
            .HasOne(t => t.Room)
            .WithMany(r => r.Tenants)
            .HasForeignKey(t => t.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Tenant>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Tenant)
            .WithMany(t => t.Payments)
            .HasForeignKey(p => p.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Room)
            .WithMany(r => r.Payments)
            .HasForeignKey(p => p.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Property)
            .WithMany(pr => pr.Payments)
            .HasForeignKey(p => p.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Agent)
            .WithMany()
            .HasForeignKey(p => p.AgentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<IssueTicket>()
            .HasOne(i => i.Tenant)
            .WithMany(t => t.IssueTickets)
            .HasForeignKey(i => i.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<IssueTicket>()
            .HasOne(i => i.Room)
            .WithMany(r => r.IssueTickets)
            .HasForeignKey(i => i.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<IssueTicket>()
            .HasOne(i => i.Property)
            .WithMany(p => p.IssueTickets)
            .HasForeignKey(i => i.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<IssueTicket>()
            .HasOne(i => i.Agent)
            .WithMany()
            .HasForeignKey(i => i.AgentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.IssueTicket)
            .WithMany(i => i.Attachments)
            .HasForeignKey(a => a.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        // Create indexes for better query performance
        modelBuilder.Entity<Payment>()
            .HasIndex(p => new { p.PropertyId, p.AgentId, p.RoomId, p.TenantId });

        modelBuilder.Entity<Payment>()
            .HasIndex(p => p.PaymentDate);

        modelBuilder.Entity<PropertyAssignment>()
            .HasIndex(pa => new { pa.PropertyId, pa.AgentId, pa.IsActive });

        modelBuilder.Entity<Tenant>()
            .HasIndex(t => new { t.RoomId, t.Status });

        modelBuilder.Entity<AuditLog>()
            .HasIndex(al => new { al.EntityType, al.EntityId, al.Timestamp });
    }
}

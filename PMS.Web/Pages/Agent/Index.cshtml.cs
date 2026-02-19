using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Data;
using PMS.Models.Models;
using PMS.Utilities;

namespace PMS.Web.Pages.Agent;

[Authorize(Roles = Roles.Agent)]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public int PropertiesAssigned { get; set; }
    public int TotalRooms { get; set; }
    public int OccupiedRooms { get; set; }
    public int UnresolvedIssues { get; set; }
    public List<Payment> RecentPayments { get; set; } = new();

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return;

        var propertyIds = await _context.PropertyAssignments
            .Where(pa => pa.AgentId == user.Id && pa.IsActive)
            .Select(pa => pa.PropertyId)
            .ToListAsync();

        PropertiesAssigned = propertyIds.Count;

        TotalRooms = await _context.Rooms
            .Where(r => propertyIds.Contains(r.PropertyId))
            .CountAsync();

        OccupiedRooms = await _context.Rooms
            .Where(r => propertyIds.Contains(r.PropertyId) && r.Status == RoomStatus.Occupied)
            .CountAsync();

        UnresolvedIssues = await _context.IssueTickets
            .Where(t => propertyIds.Contains(t.PropertyId) && t.Status != TicketStatus.Closed)
            .CountAsync();

        RecentPayments = await _context.Payments
            .Include(p => p.Tenant)
                .ThenInclude(t => t.User)
            .Include(p => p.Room)
            .Where(p => propertyIds.Contains(p.PropertyId))
            .OrderByDescending(p => p.PaymentDate)
            .Take(10)
            .ToListAsync();
    }
}

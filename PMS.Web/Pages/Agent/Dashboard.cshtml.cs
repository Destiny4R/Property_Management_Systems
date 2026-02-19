using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.DataAccess.Data;
using PMS.Models.Models;

namespace PMS.Web.Pages.Agent;

[Authorize(Roles = "Agent")]
public class DashboardModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public int AssignedProperties { get; set; }
    public int TotalRooms { get; set; }
    public int ActiveTenants { get; set; }
    public int OpenIssues { get; set; }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return;

        // Get properties assigned to this agent
        var assignedPropertyIds = await _context.PropertyAssignments
            .Where(pa => pa.AgentId == user.Id && pa.IsActive)
            .Select(pa => pa.PropertyId)
            .ToListAsync();

        AssignedProperties = assignedPropertyIds.Count;

        // Get total rooms in assigned properties
        TotalRooms = await _context.Rooms
            .Where(r => assignedPropertyIds.Contains(r.PropertyId))
            .CountAsync();

        // Get active tenants in assigned properties
        ActiveTenants = await _context.Tenants
            .Where(t => assignedPropertyIds.Contains(t.Room.PropertyId) && t.Status == TenantStatus.Active)
            .CountAsync();

        // Get open issues for assigned properties
        OpenIssues = await _context.IssueTickets
            .Where(i => assignedPropertyIds.Contains(i.PropertyId) && i.Status == IssueStatus.Open)
            .CountAsync();
    }
}

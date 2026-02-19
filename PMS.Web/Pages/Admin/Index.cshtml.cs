using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Data;
using PMS.Models.Models;
using PMS.Utilities;

namespace PMS.Web.Pages.Admin;

[Authorize(Roles = Roles.Admin)]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public int TotalProperties { get; set; }
    public int TotalAgents { get; set; }
    public int TotalTenants { get; set; }
    public decimal TodayPayments { get; set; }
    public List<Payment> RecentPayments { get; set; } = new();

    public async Task OnGetAsync()
    {
        TotalProperties = await _context.Properties.CountAsync();
        
        var agents = await _userManager.GetUsersInRoleAsync(Roles.Agent);
        TotalAgents = agents.Count;

        TotalTenants = await _context.Tenants.CountAsync();

        var today = DateTime.Today;
        TodayPayments = await _context.Payments
            .Where(p => p.PaymentDate.Date == today && p.Status == PaymentStatus.Completed)
            .SumAsync(p => (decimal?)p.Amount) ?? 0;

        RecentPayments = await _context.Payments
            .Include(p => p.Tenant)
                .ThenInclude(t => t.User)
            .Include(p => p.Property)
            .OrderByDescending(p => p.PaymentDate)
            .Take(10)
            .ToListAsync();
    }
}

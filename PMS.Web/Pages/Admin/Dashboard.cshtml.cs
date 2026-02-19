using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.DataAccess.Data;
using PMS.Models.Models;

namespace PMS.Web.Pages.Admin;

[Authorize(Roles = "Admin")]
public class DashboardModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DashboardModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public int TotalProperties { get; set; }
    public int TotalAgents { get; set; }
    public int TotalTenants { get; set; }
    public decimal TodayPayments { get; set; }

    public async Task OnGetAsync()
    {
        TotalProperties = await _context.Properties.CountAsync();
        TotalAgents = await _context.Users.CountAsync(u => u.Role == UserRole.Agent);
        TotalTenants = await _context.Tenants.CountAsync();
        
        var today = DateTime.UtcNow.Date;
        TodayPayments = await _context.Payments
            .Where(p => p.PaymentDate.Date == today && p.Status == PaymentStatus.Completed)
            .SumAsync(p => (decimal?)p.Amount) ?? 0;
    }
}

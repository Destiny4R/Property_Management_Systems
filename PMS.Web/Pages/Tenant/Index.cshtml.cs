using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Data;
using PMS.Models.Models;
using PMS.Utilities;
using TenantModel = PMS.Models.Models.Tenant;

namespace PMS.Web.Pages.Tenant;

[Authorize(Roles = Roles.Tenant)]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<TenantModel> MyRentals { get; set; } = new();
    public List<Payment> RecentPayments { get; set; } = new();
    public decimal TotalPaid { get; set; }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return;

        MyRentals = await _context.Tenants
            .Include(t => t.Room)
                .ThenInclude(r => r.Property)
            .Where(t => t.UserId == user.Id && t.Status == TenantStatus.Active)
            .ToListAsync();

        var tenantIds = MyRentals.Select(t => t.Id).ToList();

        RecentPayments = await _context.Payments
            .Include(p => p.Room)
                .ThenInclude(r => r.Property)
            .Where(p => tenantIds.Contains(p.TenantId))
            .OrderByDescending(p => p.PaymentDate)
            .Take(10)
            .ToListAsync();

        TotalPaid = await _context.Payments
            .Where(p => tenantIds.Contains(p.TenantId) && p.Status == PaymentStatus.Completed)
            .SumAsync(p => (decimal?)p.Amount) ?? 0;
    }
}

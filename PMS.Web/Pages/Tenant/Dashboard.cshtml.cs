using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.DataAccess.Data;
using PMS.Models.Models;

namespace PMS.Web.Pages.Tenant;

[Authorize(Roles = "Tenant")]
public class DashboardModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public Models.Models.Tenant? CurrentTenant { get; set; }
    public string PropertyName { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public decimal RentAmount { get; set; }
    public DateTime NextPaymentDue { get; set; }
    public int TotalPayments { get; set; }
    public DateTime? LastPaymentDate { get; set; }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return;

        // Get current tenant record
        CurrentTenant = await _context.Tenants
            .Include(t => t.Room)
                .ThenInclude(r => r.Property)
            .Where(t => t.UserId == user.Id && t.Status == TenantStatus.Active)
            .FirstOrDefaultAsync();

        if (CurrentTenant != null)
        {
            PropertyName = CurrentTenant.Room.Property.Name;
            RoomNumber = CurrentTenant.Room.RoomNumber;
            RentAmount = CurrentTenant.RentAmountOverride ?? CurrentTenant.Room.RentAmount;
            
            // Calculate next payment due (simple logic: 30 days from lease start or last payment)
            var lastPayment = await _context.Payments
                .Where(p => p.TenantId == CurrentTenant.Id && p.Status == PaymentStatus.Completed)
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefaultAsync();

            if (lastPayment != null)
            {
                NextPaymentDue = lastPayment.PaymentDate.AddDays(30);
                LastPaymentDate = lastPayment.PaymentDate;
            }
            else
            {
                NextPaymentDue = CurrentTenant.LeaseStartDate.AddDays(30);
            }

            TotalPayments = await _context.Payments
                .Where(p => p.TenantId == CurrentTenant.Id && p.Status == PaymentStatus.Completed)
                .CountAsync();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Models.Models;

namespace PMS.DataAccess.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Seed Roles
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
        if (!await roleManager.RoleExistsAsync("Agent"))
        {
            await roleManager.CreateAsync(new IdentityRole("Agent"));
        }
        if (!await roleManager.RoleExistsAsync("Tenant"))
        {
            await roleManager.CreateAsync(new IdentityRole("Tenant"));
        }

        // Seed Admin User
        var adminEmail = "admin@pms.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FullName = "System Administrator",
                PhoneNumber = "+2341234567890",
                Role = UserRole.Admin,
                Status = UserStatus.Active,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Seed a sample Agent
        var agentEmail = "agent@pms.com";
        var agentUser = await userManager.FindByEmailAsync(agentEmail);
        
        if (agentUser == null)
        {
            agentUser = new ApplicationUser
            {
                UserName = agentEmail,
                Email = agentEmail,
                EmailConfirmed = true,
                FullName = "Sample Agent",
                PhoneNumber = "+2349876543210",
                Role = UserRole.Agent,
                Status = UserStatus.Active,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(agentUser, "Agent@123");
            
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(agentUser, "Agent");
            }
        }

        await context.SaveChangesAsync();
    }
}

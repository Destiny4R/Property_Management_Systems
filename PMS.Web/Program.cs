using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Data;
using PMS.Models.Models;
using PMS.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase", true);

if (useInMemoryDatabase)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("PropertyManagement"));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Server=localhost;Database=PropertyManagement;User=root;Password=password;";

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await SeedData(context, userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();

static async Task SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    // Create database if not exists
    await context.Database.EnsureCreatedAsync();

    // Seed roles
    foreach (var role in Roles.AllRoles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Seed admin user
    var adminEmail = "admin@pms.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "System",
            LastName = "Administrator",
            Phone = "+2348000000000",
            EmailConfirmed = true,
            Status = UserStatus.Active
        };

        var result = await userManager.CreateAsync(adminUser, "Admin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, Roles.Admin);
        }
    }
}

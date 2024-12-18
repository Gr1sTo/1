using _1.Data;
using _1.Middleware;
using _1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IMedicineService, MedicineService>();

builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    //var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
    //if (!adminRoleExists)
    //{
    //    await roleManager.CreateAsync(new IdentityRole("Admin"));
    //}

    //var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    //if (adminUser != null)
    //{
    //    var isInRole = await userManager.IsInRoleAsync(adminUser, "Admin");
    //    if (!isInRole)
    //    {
    //        await userManager.AddToRoleAsync(adminUser, "Admin");
    //    }
    //}
}

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

app.UseMiddleware<LoggingMiddleware>();

app.MapGet("/", async context =>
{
    if (!context.User.Identity?.IsAuthenticated ?? true)
    {
        context.Response.Redirect("/Identity/Account/Login");
    }
    else
    {
        context.Response.Redirect("/DatabaseView");
    }
});

app.MapRazorPages();

app.Run();

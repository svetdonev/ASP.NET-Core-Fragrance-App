using Fragrance_Web_App.Data;
using Fragrance_Web_App.Infrastructure;
using Fragrance_Web_App.Mapping;
using Fragrance_Web_App.Repositories;
using Fragrance_Web_App.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddAutoMapper(typeof(FragranceMappingProfile));
builder.Services.AddScoped<IFragranceService, FragranceService>();
builder.Services.AddScoped<IFragranceRepository, FragranceSqlRepository>();
builder.Services.AddDbContext<FragranceAppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FragranceAppDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.PrepareDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
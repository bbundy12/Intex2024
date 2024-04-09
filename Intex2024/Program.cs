using Intex2024.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// For Google signin 
/*services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});*/

// Add services to the container.
//For the identity database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// For the identity database-- This part needs to be kept
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// For the non identity database
builder.Services.AddDbContext<IntexDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:IntexConnection"]);
});
builder.Services.AddScoped<IIntexRepository, EFIntexRepository>();

/*builder.Services.AddRazorPages();*/

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>() ;allow you to access the current session of the session cart


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
/*app.MapControllerRoute("pagenumandtype", "{projectType}/{pageNum}", new { Controller = "Home", action = "Index" });
app.MapControllerRoute("pagination", "{pageNum}", new { Controller = "Home", action = "Index", pageNum = 1 });
app.MapControllerRoute("projectType", "{projectType}", new { Controller = "Home", action = "Index", pageNum = 1 });*/
app.MapRazorPages();

app.Run();

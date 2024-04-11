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
var connectionString = builder.Configuration.GetConnectionString("IntexConnection") ?? throw new InvalidOperationException("Connection string 'IntexConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Configuration Identity Services
builder.Services.AddIdentity<Customer, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IIntexRepository, EFIntexRepository>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


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
    name: "cart",
    pattern: "/Cart",
    defaults: new { controller = "Home", action = "Cart" }
);
app.MapControllerRoute("pagenumandtype", "Products/{productCategory}/{pageNum}", new { Controller = "Home", action = "Products" });
app.MapControllerRoute("pagination", "Products/{pageNum}", new { Controller = "Home", action = "Products", pageNum = 1});
app.MapControllerRoute("productCategory", "Products/{productCategory}", new { Controller = "Home", action = "Products", pageNum = 1 });

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();

using Intex2024.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Onnx;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("Intex2024"));
// builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
var services = builder.Services;
var configuration = builder.Configuration;

var clientId = builder.Configuration["Authentication:Google:ClientId"];
var clientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 10;
    options.Password.RequiredUniqueChars = 1;
});

//For Google signin 
/*services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = clientId;
    googleOptions.ClientSecret = clientSecret;
});*/


// Add services to the container.
//For the identity database
var connectionString = builder.Configuration.GetConnectionString("IntexConnection") ?? throw new InvalidOperationException("Connection string 'IntexConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Configuration Identity Services
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

/*builder.Services.AddDbContext<IntexDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:IntexConnection"]);
});*/
builder.Services.AddScoped<IIntexRepository, EFIntexRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();


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
    defaults: new { controller = "Home", action = "Cart" });

app.MapControllerRoute("pagenumandtype", "Products/{productCategory}/{pageNum}", new { Controller = "Home", action = "Products" });
app.MapControllerRoute("pagination", "Products/{pageNum}", new { Controller = "Home", action = "Products", pageNum = 1});
app.MapControllerRoute("productCategory", "Products/{productCategory}", new { Controller = "Home", action = "Products", pageNum = 1 });

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();

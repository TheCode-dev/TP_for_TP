using Microsoft.AspNetCore.Identity;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;
using TP_KP_Belyshev.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>()
    .AddDefaultTokenProviders();

builder.Services
    .AddTransient<IUserStore<ApplicationUser>, CustomUserStore>()
    .AddTransient<IRoleStore<ApplicationRole>, CustomRoleStore>()
    .AddSingleton<IApplicationUserRepository, ApplicationUserRepository>()
    .AddSingleton<IApplicationRoleRepository, ApplicationRoleRepository>()
    .AddSingleton<ICatProductRepository, CatProductRepository>()
    .AddSingleton<IProductsRepository, ProductsRepository>()
    .AddSingleton<IUsersRepository, UsersRepository>()
    .AddSingleton<IOrdersRepository, OrdersRepository>()

    ;

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

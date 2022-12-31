using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Infrastructure.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web_ECommerce.Data;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("ContextBaseConnection") ?? throw new InvalidOperationException("Connection string 'ContextBaseConnection' not found.");

// Add services to the container.
var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Teste;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
builder.Services.AddDbContext<ContextBase>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ContextBase>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IGeneric<>), typeof(RepositoryGenerics<>));
builder.Services.AddScoped<IProduct, RepositoryProduct>();
builder.Services.AddScoped<InterfaceProductApp, AppProduct>();

builder.Services.AddScoped<IServiceProduct, ServiceProduto>();
builder.Services.AddScoped<ICompraUsuario,RepositoryCompraUsuario>();
builder.Services.AddScoped<InterfaceCompraUsuarioApp, AppCompraUsuario>();
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


using Proxy_shopping.service;
using Microsoft.EntityFrameworkCore;
using Proxy_shopping.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("linkToProxy");
builder.Services.AddDbContext<ProxyContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=homepage}/{id?}");
//請用這個規則，去找對應的 Controller 和 Action -->設定首頁  
app.Run();

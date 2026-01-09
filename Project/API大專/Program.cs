using Microsoft.EntityFrameworkCore;
using API大專.Models;
using API大專.service;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("ProxyContext");
builder.Services.AddDbContext<ProxyContext>(x => x.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CommissionService>(); //注入service

//設定COR 跨網域
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMvc",
        policy =>
        {
            policy.WithOrigins("http://localhost:5032")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowMvc");

app.UseAuthorization();

app.MapControllers();

app.Run();

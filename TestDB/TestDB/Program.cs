using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using TestDB.Data;
using TestDB.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<TextContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TextContext") ?? throw new InvalidOperationException("Connection string 'TextContext' not found.")));

// Database Setup
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TestContext>(options =>  
    options.UseMySql(connectionString, new MySqlServerVersion("11.4.2 - MariaDB - ubu2404"))  
);

// Verify connection
using (var connection = new MySqlConnection(connectionString))
{
    try
    {
        connection.Open();
        System.Diagnostics.Debug.WriteLine("Connection successful!");
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Connection failed: {ex.Message}");
    }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
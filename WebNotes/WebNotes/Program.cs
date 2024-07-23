using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using WebNotes.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebNotesDBContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion("11.4.2 - MariaDB - ubu2404"))
);

var app = builder.Build();

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

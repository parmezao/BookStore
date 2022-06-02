using Book_Store.Data;
using Book_Store.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var server = builder.Configuration["DbServer"] ?? "localhost";
var port = builder.Configuration["DbPort"] ?? "1433"; // Default SQL Server port
var user = builder.Configuration["DbUser"] ?? "sa"; 
var password = builder.Configuration["Password"] ?? "Unknown#2022";
var database = builder.Configuration["Database"] ?? "BooksDB";
var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}";

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(connectionString));

// Force the application to use Port 80
builder.WebHost.UseUrls("http://*:80"); 

builder.Services.AddControllersWithViews();

var app = builder.Build();
builder.Services.AddMigrationInitialization(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    pattern: "{controller=BookStore}/{action=Index}/{id?}");

app.Run();

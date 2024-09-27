using Banco.Models;
using Banco.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var dbHost = builder.Configuration["DB_HOST"] ?? "localhost";
var dbPort = builder.Configuration["DB_PORT"] ?? "1433";
var dbName = builder.Configuration["DB_NAME"] ?? "BancoDB";
var dbUser = builder.Configuration["DB_USER"] ?? "sa";
var dbPass = builder.Configuration["DB_PASS"] ?? "TesteMT.2024";

var conString = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPass};Trusted_Connection=false;MultipleActiveResultSets=true;TrustServerCertificate=true;";

//add conexao com o banco
builder.Services.AddDbContext<BancoDbContext>(options => options.UseSqlServer(conString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Inicializando migrations
DbService.InitialMigration(app);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

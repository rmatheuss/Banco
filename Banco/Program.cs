using Banco.Models;
using Banco.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var dbHost = builder.Configuration["DB_HOST"] ?? "localhost";
var dbPort = builder.Configuration["DB_PORT"] ?? "1433";
var dbName = builder.Configuration["DB_NAME"] ?? "BancoDB";
var dbUser = builder.Configuration["DB_USER"] ?? "sa";
var dbPass = builder.Configuration["DB_PASS"] ?? "TesteMT.2024";

var conString = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPass};Trusted_Connection=false;MultipleActiveResultSets=true;TrustServerCertificate=true;";

//add conexao com o banco
builder.Services.AddDbContext<BancoDbContext>(options => options.UseSqlServer(conString));

//add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Banco API",
        Description = "API de Banco para gerenciar transações"
    });
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//Inicializando migrations
DbService.InitialMigration(app);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banco API v1");
    c.RoutePrefix = string.Empty; // Deixa o Swagger na rota raiz ("/")
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

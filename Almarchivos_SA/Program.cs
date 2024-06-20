using Almarchivos_SA.Data;
using Almarchivos_SA.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;

// Conexión con la base de datos, definiendo MiConexionBD
var connectionString = configuration.GetConnectionString("MiConexionBD");

var dbContextOptions = new DbContextOptionsBuilder<Connection>()
    .UseSqlServer(connectionString)
    .Options;

builder.Services.AddDbContext<Connection>(options =>
    options.UseSqlServer(connectionString));

//Definiendo los servicios que se usaran dentro del proyecto.
builder.Services.AddScoped<IConsulta, ConsultaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

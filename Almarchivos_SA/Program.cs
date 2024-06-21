using Almarchivos_SA.Data;
using Almarchivos_SA.Services;
using Microsoft.EntityFrameworkCore;
// Librería para realizar la autentificación
using Microsoft.AspNetCore.Authentication.Cookies; 

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

// Configuración de la autenticación por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Almarchivo_SA.Cookie"; // Nombre de la cookie de sesión
        options.LoginPath = "/Login/Login"; // Ruta de la página de inicio de sesión
        options.AccessDeniedPath = "/Home/AccessDenied"; // Ruta de acceso denegado
    });

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

// Middleware de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();

using Almarchivos_SA.Data;
using Almarchivos_SA.Services;
using Microsoft.EntityFrameworkCore;
// Librer�a para realizar la autentificaci�n
using Microsoft.AspNetCore.Authentication.Cookies; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;

// Conexi�n con la base de datos, definiendo MiConexionBD
var connectionString = configuration.GetConnectionString("MiConexionBD");

var dbContextOptions = new DbContextOptionsBuilder<Connection>()
    .UseSqlServer(connectionString)
    .Options;

builder.Services.AddDbContext<Connection>(options =>
    options.UseSqlServer(connectionString));

//Definiendo los servicios que se usaran dentro del proyecto.
builder.Services.AddScoped<IConsulta, ConsultaService>();

// Configuraci�n de la autenticaci�n por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Almarchivo_SA.Cookie"; // Nombre de la cookie de sesi�n
        options.LoginPath = "/Login/Login"; // Ruta de la p�gina de inicio de sesi�n
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

// Middleware de autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();

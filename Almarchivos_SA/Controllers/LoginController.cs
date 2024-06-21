using Almarchivos_SA.Data;
using Almarchivos_SA.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Almarchivos_SA.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConsulta _consulta;
        private readonly Connection _miConexion;

        public LoginController(IConsulta consulta, Connection miConexion)
        {
            _consulta = consulta;
            _miConexion = miConexion;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string nombreUsuario, string contraseña)
        {
            try
            {
                var usuario = _miConexion.Usuarios.FirstOrDefault(u => u.Nombre_Usuario == nombreUsuario);

                if (usuario != null)
                {
                    // Decodificar la contraseña almacenada y comparar con la contraseña ingresada
                    byte[] data = Convert.FromBase64String(usuario.Contraseña);
                    string contraseñaAlmacenada = Encoding.UTF8.GetString(data);

                    if (contraseña == contraseñaAlmacenada)
                    {
                        // Crear claims del usuario para la cookie de autenticación
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre_Usuario),
                    // Añadir más claims según sea necesario para la aplicación
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        // Crear principal
                        var principal = new ClaimsPrincipal(claimsIdentity);

                        // Iniciar sesión con la cookie de autenticación
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Index", "Home"); // Redirigir a la página principal del sistema
                    }
                }

                ViewBag.ErrorMensaje = "Credenciales incorrectas. Inténtelo de nuevo.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMensaje = $"Error al iniciar sesión: {ex.Message}";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            // Salir de la sesión
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Login"); // Redirige al método Login del controlador Login
        }


    }
}

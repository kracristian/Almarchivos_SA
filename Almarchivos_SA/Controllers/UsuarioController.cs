using Almarchivos_SA.Models;
using Almarchivos_SA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almarchivos_SA.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConsulta _consulta;

        public UsuarioController(IConsulta consulta)
        {
            _consulta = consulta;
        }
        [Authorize]
        public IActionResult Index(int page = 1, int pageSize = 10, string filtro = "")
        {
            ResultadoUsuariosYPaginacion usuarios = _consulta.GetUsuariosCompleta(page, pageSize, filtro);
            Paginacion(usuarios.Paginacion);

            return View(usuarios);
        }
        [Authorize]
        public IActionResult AgregarUsuario(int Id_Usuario)
        {
            Usuario usuario = _consulta.CargarUsuario(Id_Usuario);
            return View(usuario);
        }
        [HttpPost]
        public IActionResult ActualizarUsuario(Usuario usuario)
        {
            _consulta.ActualizarUsuario(usuario);
            Index();
            return View("Index");
        }

        public IActionResult EliminarUsuario(int Id_Usuario)
        {
            _consulta.EliminarUsuario(Id_Usuario);
            Index();
            return View("Index");
        }

        [HttpPost]
        public IActionResult GuardarUsuario(Usuario usuario)
        {
            _consulta.GuardarUsuario(usuario);
            Index();
            return View("Index");
        }
        public IActionResult Buscador(string filtro)
        {
            return RedirectToAction("Index", new { filtro = filtro });
        }
        public void Paginacion(Paginacion paginacion)
        {
            ViewBag.CurrentPage = paginacion.CurrentPage;
            ViewBag.PageSize = paginacion.PageSize;
            ViewBag.TotalPages = paginacion.TotalPages;
            ViewBag.TotalRegistros = paginacion.TotalRegistros;
        }
    }
}

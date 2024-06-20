using Almarchivos_SA.Models;
using Almarchivos_SA.Services;
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

        public IActionResult Index(int page = 1, int pageSize = 10, string filtro = "")
        {
            ResultadoUsuariosYPaginacion personas = _consulta.GetUsuariosCompleta(page, pageSize, filtro);
            Paginacion(personas.Paginacion);

            return View(personas);
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

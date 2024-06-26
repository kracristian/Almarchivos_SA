using Almarchivos_SA.Models;
using Almarchivos_SA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace Almarchivos_SA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConsulta _consulta;

        public HomeController( IConsulta consulta)
        {
            _consulta = consulta;
        }

        [Authorize]
        public IActionResult Index(int page = 1, int pageSize = 10, string filtro = "")
        {
            ResultadoPersonasYPaginacion personas = _consulta.GetPersonasCompleta(page, pageSize, filtro);
            Paginacion(personas.Paginacion);

            return View(personas);
        }
        [Authorize]
        public IActionResult AgregarPersona(int Id_Persona)
        {
            Persona persona = _consulta.CargarPersona(Id_Persona);
            return View(persona);
        }
        [HttpPost]
        public IActionResult ActualizarPersona(Persona persona)
        {
            _consulta.ActualizarPersona(persona);
            Index();
            return View("Index");
        }
        [HttpPost]
        public IActionResult GuardarPersona(Persona persona)
        {
            _consulta.GuardarPersona(persona);
            Index();
            return View("Index");
        }
        public IActionResult EliminarPersona(int Id_Persona)
        {
            _consulta.EliminarPersona(Id_Persona);
            Index();
            return View("Index");
        }


        public IActionResult Buscador(string filtrarPor)
        {
            return RedirectToAction("Index", new { filtro = filtrarPor });
        }
        public void Paginacion(Paginacion paginacion)
        {
            ViewBag.CurrentPage = paginacion.CurrentPage;
            ViewBag.PageSize = paginacion.PageSize;
            ViewBag.TotalPages = paginacion.TotalPages;
            ViewBag.TotalRegistros = paginacion.TotalRegistros;
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

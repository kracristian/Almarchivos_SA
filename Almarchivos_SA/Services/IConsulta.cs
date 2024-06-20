using Almarchivos_SA.Models;
using Microsoft.AspNetCore.Mvc;

namespace Almarchivos_SA.Services
{
    public interface IConsulta
    {
        List<Usuario> GetUsuarios();
        List<Persona> GetPersonas();
        ResultadoPersonasYPaginacion GetPersonasCompleta(int page = 1, int pageSize = 10, string filtro = "");
        ResultadoUsuariosYPaginacion GetUsuariosCompleta(int page = 1, int pageSize = 10, string filtro = "");
        Persona CargarPersona(int idPersona);
        void GuardarPersona(Persona persona);
        void ActualizarPersona(Persona persona);
        Usuario CargarUsuario(int idUsuario);
        void GuardarUsuario(Usuario usuario);
        void ActualizarUsuario(Usuario usuario);
    }
}

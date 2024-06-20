using Almarchivos_SA.Data;
using Almarchivos_SA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Almarchivos_SA.Services
{
    public class ConsultaService : IConsulta
    {
        private readonly Connection _miConexion;

        public ConsultaService(Connection miConexion)
        {
            _miConexion = miConexion;
        }

        [HttpGet(Name = "Traer todos los usuarios")]
        public List<Usuario> GetUsuarios()
        {
            return _miConexion.Usuarios.ToList();
        }

        [HttpGet(Name = "Traer todos los personas")]
        public List<Persona> GetPersonas()
        {
            return _miConexion.Personas.ToList();
        }

        public ResultadoPersonasYPaginacion GetPersonasCompleta(int page = 1, int pageSize = 10, string filtro = "")
        {
            try
            {
                int skip = (page - 1) * pageSize;

                // Consulta base para obtener las personas y aplicar el filtro
                var consulta = _miConexion.Personas
                                .Where(p =>
                                    string.IsNullOrEmpty(filtro) ||
                                    p.Nombres.Contains(filtro) ||
                                    p.Apellidos.Contains(filtro) ||
                                    p.Email.Contains(filtro) ||
                                    p.Tipo_Identificacion.Contains(filtro) ||
                                    p.Numero_Identificacion.Contains(filtro))
                                .Select(p => new Persona
                                {
                                    Id_Persona = p.Id_Persona,
                                    Nombres = p.Nombres,
                                    Apellidos = p.Apellidos,
                                    Numero_Identificacion = p.Numero_Identificacion,
                                    Email = p.Email,
                                    Tipo_Identificacion = p.Tipo_Identificacion,
                                    Fecha_Creacion = p.Fecha_Creacion
                                });

                // Aplicar paginación
                Paginacion paginacion = Paginacion(pageSize, consulta.Count(), page);

                // Obtener los datos paginados
                var personasPaginadas = consulta
                                        .Skip(skip)
                                        .Take(pageSize)
                                        .ToList();

                // Crear el resultado final con personas y la información de paginación
                ResultadoPersonasYPaginacion resultadoPersonaYPaginacion = new ResultadoPersonasYPaginacion
                {
                    Personas = personasPaginadas,
                    Paginacion = paginacion
                };

                return resultadoPersonaYPaginacion;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al obtener personas: {ex.Message}");
                return null;
            }
        }
        public ResultadoUsuariosYPaginacion GetUsuariosCompleta(int page = 1, int pageSize = 10, string filtro = "")
        {
            try
            {
                int skip = (page - 1) * pageSize;

                // Consulta base para obtener los usuarios y aplicar el filtro
                var consulta = _miConexion.Usuarios
                                .Where(u =>
                                    string.IsNullOrEmpty(filtro) ||
                                    u.Nombre_Usuario.Contains(filtro))
                                .Select(u => new Usuario
                                {
                                    Id_Usuario = u.Id_Usuario,
                                    Nombre_Usuario = u.Nombre_Usuario,
                                    Contraseña = u.Contraseña,
                                    Fecha_Creacion = u.Fecha_Creacion
                                });

                // Aplicar paginación
                Paginacion paginacion = Paginacion(pageSize, consulta.Count(), page);

                // Obtener los datos paginados
                var usuariosPaginados = consulta
                                        .Skip(skip)
                                        .Take(pageSize)
                                        .ToList();

                // Crear el resultado final con usuarios y la información de paginación
                ResultadoUsuariosYPaginacion resultadoUsuariosYPaginacion = new ResultadoUsuariosYPaginacion
                {
                    Usuarios = usuariosPaginados,
                    Paginacion = paginacion
                };

                return resultadoUsuariosYPaginacion;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al obtener usuarios: {ex.Message}");
                return null;
            }
        }
        public Persona CargarPersona(int idPersona)
        {
            try
            {
                var persona = _miConexion.Personas.FirstOrDefault(p => p.Id_Persona == idPersona);

                return persona;
            }
            catch (Exception ex)
            {
                Persona persona = null;
                return persona; // Manejo de errores
            }
        }
        public void GuardarPersona(Persona persona)
        {
            try
            {
                persona.Fecha_Creacion = DateTime.Now; 
                _miConexion.Personas.Add(persona);
                _miConexion.SaveChanges();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, logueo, etc.
                throw new ApplicationException("Error al guardar la persona.", ex);
            }
        }
        public void ActualizarPersona(Persona persona)
        {
            try
            {
                var personaExistente = _miConexion.Personas.Find(persona.Id_Persona);

                if (personaExistente != null)
                {
                    personaExistente.Nombres = persona.Nombres;
                    personaExistente.Apellidos = persona.Apellidos;
                    personaExistente.Numero_Identificacion = persona.Numero_Identificacion;
                    personaExistente.Email = persona.Email;
                    personaExistente.Tipo_Identificacion = persona.Tipo_Identificacion;

                    _miConexion.SaveChanges();
                }
                else
                {
                    throw new ApplicationException("La persona no fue encontrada.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, logueo, etc.
                throw new ApplicationException("Error al actualizar la persona.", ex);
            }
        }
        public Paginacion Paginacion(int pageSize = 10, int totalRecords = 0, int page = 0)
        {
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            if (totalPages > 10 && page <= totalPages - 9)
            {
                totalPages = page + 9;
            }
            Paginacion paginacion = new Paginacion
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRegistros = totalRecords
            };

            return paginacion;
        }
    }
}

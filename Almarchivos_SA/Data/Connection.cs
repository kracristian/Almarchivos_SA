using Almarchivos_SA.Models;
using Microsoft.EntityFrameworkCore;

namespace Almarchivos_SA.Data
{
    public class Connection : DbContext
    {
        public Connection(DbContextOptions<Connection> options)
       : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}

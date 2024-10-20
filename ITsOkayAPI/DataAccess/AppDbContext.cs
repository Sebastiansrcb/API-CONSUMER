using ITsOkayAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ITsOkayAPI.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sintoma> Sintomas { get; set; }
        public DbSet<RegistroPsicologico> RegistrosPsicologicos { get; set; }
        public DbSet<RegistroSintomas> RegistrosSintomas { get; set; }
        public DbSet<Pacientes> Pacientes { get; set;}
        public DbSet<Contactos> Contactos { get; set; }
    }
}

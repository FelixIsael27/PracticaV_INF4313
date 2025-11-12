using Microsoft.EntityFrameworkCore;
using Actividad5LengProg3.Models;

namespace Actividad5LengProg3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<EstudianteViewModel> Estudiantes { get; set; }
        public DbSet<RecintoViewModel> Recintos { get; set; }
        public DbSet<CarreraViewModel> Carreras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecintoViewModel>()
                .HasKey(r => r.Codigo);

            modelBuilder.Entity<CarreraViewModel>()
                .HasKey(c => c.Codigo);

            modelBuilder.Entity<EstudianteViewModel>()
                .HasKey(e => e.Matricula);

            modelBuilder.Entity<EstudianteViewModel>()
                .HasOne(e => e.RecintoCod)
                .WithMany(r => r.Estudiantes)
                .HasForeignKey(e => e.Recinto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EstudianteViewModel>()
                .HasOne(e => e.CarreraCod)
                .WithMany(c => c.Estudiantes)
                .HasForeignKey(e => e.Carrera)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Actividad5LengProg3.Models.Actividad5LengProg3.Migrations;

public partial class Actividad5LengProg3Context : DbContext
{
    public Actividad5LengProg3Context()
    {
    }

    public Actividad5LengProg3Context(DbContextOptions<Actividad5LengProg3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Recinto> Recintos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Actividad5LengProg3;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Carreras__06370DADCCDED611");

            entity.Property(e => e.Codigo).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Carrera).HasMaxLength(100);
            entity.Property(e => e.Celular).HasMaxLength(20);
            entity.Property(e => e.CorreoInstitucional).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(150);
            entity.Property(e => e.Genero).HasMaxLength(30);
            entity.Property(e => e.Matricula).HasMaxLength(20);
            entity.Property(e => e.NombreCompleto).HasMaxLength(100);
            entity.Property(e => e.Recinto).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.Turno).HasMaxLength(30);

            entity.HasOne(d => d.CarreraNavigation).WithMany()
                .HasForeignKey(d => d.Carrera)
                .HasConstraintName("FK_Estudiantes_Carreras");

            entity.HasOne(d => d.RecintoNavigation).WithMany()
                .HasForeignKey(d => d.Recinto)
                .HasConstraintName("FK_Estudiantes_Recintos");
        });

        modelBuilder.Entity<Recinto>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Recintos__06370DAD90B9F357");

            entity.Property(e => e.Codigo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

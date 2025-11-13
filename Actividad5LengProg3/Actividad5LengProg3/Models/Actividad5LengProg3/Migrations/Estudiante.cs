using System;
using System.Collections.Generic;

namespace Actividad5LengProg3.Models.Actividad5LengProg3.Migrations;

public partial class Estudiante
{
    public string Matricula { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string Carrera { get; set; } = null!;

    public string Recinto { get; set; } = null!;

    public string CorreoInstitucional { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public string Genero { get; set; } = null!;

    public string Turno { get; set; } = null!;

    public bool EstaBecado { get; set; }

    public int? PorcentajeBeca { get; set; }

    public virtual Carrera CarreraNavigation { get; set; } = null!;

    public virtual Recinto RecintoNavigation { get; set; } = null!;
}

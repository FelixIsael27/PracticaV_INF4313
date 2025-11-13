using System;
using System.Collections.Generic;

namespace Actividad5LengProg3.Models.Actividad5LengProg3.Migrations;

public partial class Carrera
{
    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int CantidadCreditos { get; set; }

    public int CantidadMaterias { get; set; }
}

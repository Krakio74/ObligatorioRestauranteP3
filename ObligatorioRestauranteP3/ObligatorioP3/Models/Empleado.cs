using System;
using System.Collections.Generic;

namespace ObligatorioP3.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Rango { get; set; } = null!;

    public string Estado { get; set; } = null!;
    //public string Activo { get; set; } = null!;

    public virtual Usuario? IdNavigation { get; set; } = null!;
    public Empleado(int id, string rango, string estado)
    {
        Id = id;
        Rango = rango;
        Estado = estado;
    }
}

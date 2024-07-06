using System;
using System.Collections.Generic;

namespace ObligatorioRestauranteP3.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Telefono { get; set; }

    public string Contraseña { get; set; } = null!;

    public DateOnly FechaCreacion { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Empleado? Empleado { get; set; }

    public virtual ICollection<PermisosUsuario> PermisosUsuarios { get; set; } = new List<PermisosUsuario>();
}

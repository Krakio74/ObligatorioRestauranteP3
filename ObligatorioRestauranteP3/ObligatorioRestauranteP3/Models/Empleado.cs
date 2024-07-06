using System;
using System.Collections.Generic;

namespace ObligatorioRestauranteP3.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public bool? Anular { get; set; }

    public bool? Modificar { get; set; }

    public int ResId { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Usuario IdNavigation { get; set; } = null!;
}

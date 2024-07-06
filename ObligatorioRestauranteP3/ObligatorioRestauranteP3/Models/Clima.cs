using System;
using System.Collections.Generic;

namespace ObligatorioRestauranteP3.Models;

public partial class Clima
{
    public int Id { get; set; }

    public TimeOnly? Fecha { get; set; }

    public string? Temperatura { get; set; }

    public string? DescripcionClima { get; set; }
}

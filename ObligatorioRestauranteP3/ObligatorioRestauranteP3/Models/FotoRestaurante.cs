using System;
using System.Collections.Generic;

namespace ObligatorioRestauranteP3.Models;

public partial class FotoRestaurante
{
    public int RestauranteId { get; set; }

    public string? Foto { get; set; }

    public virtual Restaurante Restaurante { get; set; } = null!;
}

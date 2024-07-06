using System;
using System.Collections.Generic;

namespace ObligatorioRestauranteP3.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string NombrePlato { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public double? Precio { get; set; }

    public string Categoria { get; set; } = null!;

    public virtual ICollection<OrdenDetalle> OrdenDetalles { get; set; } = new List<OrdenDetalle>();

    public virtual ICollection<Restaurante> IdRestaurantes { get; set; } = new List<Restaurante>();
}

using System;
using System.Collections.Generic;

namespace ObligatorioRestauranteP3.Models;

public partial class Orden
{
    public int Id { get; set; }

    public int? ReservaId { get; set; }

    public decimal Total { get; set; }

    public int? RestauranteId { get; set; }

    public decimal? DescCliente { get; set; }

    public decimal? DescTemperatura { get; set; }

    public decimal? DescClima { get; set; }

    public virtual ICollection<OrdenDetalle> OrdenDetalles { get; set; } = new List<OrdenDetalle>();

    public virtual Reserva? Reserva { get; set; }

    public virtual Restaurante? Restaurante { get; set; }
}

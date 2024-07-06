using System;
using System.Collections.Generic;

namespace ObligatorioP3.Models;

public partial class Orden
{
    public int Id { get; set; }

    public int? ReservaId { get; set; }

    public decimal Total { get; set; }

    public decimal? Descuento { get; set; }

    public virtual ICollection<OrdenDetalle> OrdenDetalles { get; set; } = new List<OrdenDetalle>();

    public virtual Reserva? Reserva { get; set; }
}

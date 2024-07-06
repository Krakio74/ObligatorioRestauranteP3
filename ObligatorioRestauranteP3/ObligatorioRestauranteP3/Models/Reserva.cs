using System;
using System.Collections.Generic;

namespace ObligatorioRestauranteP3.Models;

public partial class Reserva
{
    public int Id { get; set; }

    public int? Clienteid { get; set; }

    public int? RestauranteId { get; set; }

    public int? MesaId { get; set; }

    public DateTime Fecha { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Cliente? Cliente { get; set; }

    public virtual Mesa? Mesa { get; set; }

    public virtual ICollection<Orden> Ordens { get; set; } = new List<Orden>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();

    public virtual Restaurante? Restaurante { get; set; }
}

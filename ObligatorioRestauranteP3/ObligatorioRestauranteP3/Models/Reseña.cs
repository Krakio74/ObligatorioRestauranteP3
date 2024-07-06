using System;
using System.Collections.Generic;

namespace ObligatorioRestauranteP3.Models;

public partial class Reseña
{
    public int Id { get; set; }

    public int? ReservaId { get; set; }

    public DateOnly Fecha { get; set; }

    public string Comentario { get; set; } = null!;

    public int Puntaje { get; set; }

    public int? ClienteId { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Reserva? Reserva { get; set; }
}

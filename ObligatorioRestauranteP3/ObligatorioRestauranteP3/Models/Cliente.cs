using System;
using System.Collections.Generic;

namespace ObligatorioRestauranteP3.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string TipoCliente { get; set; } = null!;

    public int? Puntaje { get; set; }

    public string? Email { get; set; }

    public virtual Usuario IdNavigation { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();
}

using System;
using System.Collections.Generic;

namespace ObligatorioP3.Models;

public partial class Cliente
{
    public int Id { get; set; }
    public string TipoCliente { get; set; } = null!;
    public virtual Usuario? Usuario { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();
}

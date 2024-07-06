using ObligatorioRestauranteP3.Models;
using System;
using System.Collections.Generic;

namespace ObligatorioRestauranteP3.Models;

public partial class Restaurante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public TimeOnly HoraApertura { get; set; }

    public TimeOnly HoraCierre { get; set; }

    public virtual ICollection<Mesa> Mesas { get; set; } = new List<Mesa>();

    public virtual ICollection<OrdenDetalle> OrdenDetalles { get; set; } = new List<OrdenDetalle>();
    public virtual ICollection<Orden>? Ordens { get; set; } = new List<Orden>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Menu> IdMenus { get; set; } = new List<Menu>();
    public virtual FotoRestaurante? FotoRestaurante { get; set; }
}

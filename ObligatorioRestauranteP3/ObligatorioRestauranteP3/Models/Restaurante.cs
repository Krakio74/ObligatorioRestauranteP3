using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ObligatorioP3.Controllers;
using ObligatorioP3.Models;

namespace ObligatorioP3.Models;

public partial class Restaurante
{

    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public TimeOnly HoraApertura { get; set; }

    public TimeOnly HoraCierre { get; set; }


    public virtual FotoRestaurante? FotoRestaurante { get; set; }


    public virtual ICollection<Mesa> Mesas { get; set; } = new List<Mesa>();

    public virtual ICollection<OrdenDetalle> OrdenDetalles { get; set; } = new List<OrdenDetalle>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();


}

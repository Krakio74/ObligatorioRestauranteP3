using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioP3.Models;

public partial class FotoRestaurante
{
    [Key]
    public int RestauranteId { get; set; }

    public string? Foto { get; set; }

    [ForeignKey("RestauranteId")]
    public virtual Restaurante Restaurante { get; set; }
}

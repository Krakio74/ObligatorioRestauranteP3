using System;
using System.Collections.Generic;

namespace ObligatorioP3.Models;

public partial class FotoMenu
{
    public int MenuId { get; set; }

    public int? Foto { get; set; }

    public virtual Menu Menu { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace ObligatorioP3.Models;

public partial class FotoUsuario
{
    public int UserId { get; set; }

    public string FotoName { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}

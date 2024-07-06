using System;
using System.Collections.Generic;

namespace ObligatorioRestauranteP3.Models;

public partial class PermisosUsuario
{
    public int UserId { get; set; }

    public string PageAccess { get; set; } = null!;

    public bool? AcessTable { get; set; }

    public bool? SeeList { get; set; }

    public bool? InsertData { get; set; }

    public bool? EditData { get; set; }

    public bool? DeleteData { get; set; }

    public bool? EditOwnData { get; set; }

    public virtual Usuario User { get; set; } = null!;
}

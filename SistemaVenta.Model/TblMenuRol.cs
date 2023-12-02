using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class TblMenuRol
{
    public int IdMenuRol { get; set; }

    public int? IdMenu { get; set; }

    public int? IdRol { get; set; }

    public virtual TblMenu? IdMenuNavigation { get; set; }

    public virtual TblRol? IdRolNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class TblMenu
{
    public int IdMenu { get; set; }

    public string? NombreMenu { get; set; }

    public string? Icono { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<TblMenuRol> TblMenuRols { get; set; } = new List<TblMenuRol>();
}

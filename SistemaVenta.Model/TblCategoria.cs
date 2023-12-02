using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class TblCategoria
{
    public int IdCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<TblProducto> TblProductos { get; set; } = new List<TblProducto>();
}

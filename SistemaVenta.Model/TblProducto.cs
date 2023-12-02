using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class TblProducto
{
    public int IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public int? IdCategoria { get; set; }

    public int? Stock { get; set; }

    public decimal? Precio { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? Nota { get; set; }

    public virtual TblCategoria? IdCategoriaNavigation { get; set; }

    public virtual ICollection<TblDetalleVenta> TblDetalleVenta { get; set; } = new List<TblDetalleVenta>();
}

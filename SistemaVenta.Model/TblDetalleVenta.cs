using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class TblDetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public virtual TblProducto? IdProductoNavigation { get; set; }

    public virtual TblVenta? IdVentaNavigation { get; set; }
}

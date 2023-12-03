using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    //min 14.53 part 4
    public class Venta_DTO
    {
        public int IdVenta { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? TipoPago { get; set; }
        //De decimal a string
        public string? TotalTexto { get; set; }
        //De DateTime a string
        public string? FechaRegistro { get; set; }

        //Se modifico el método virtual se paso DetalleVenta_DTO en vez de Tbl_DetalleVenta
        public virtual ICollection<DetalleVenta_DTO>? TblDetalleVenta { get; set; }
    }
}

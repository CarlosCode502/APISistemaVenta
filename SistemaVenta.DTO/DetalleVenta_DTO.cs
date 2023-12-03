using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    // min 13.20 part 4
    public class DetalleVenta_DTO
    {
        public int? IdProducto { get; set; }

        //Se agregó
        public string? DescripcionProducto { get; set; }

        public int? Cantidad { get; set; }

        //Modifico de decimal a string
        public string? PrecioTexto { get; set; }

        //Modifico de decimal a string
        public string? TotalTexto { get; set; }
    }
}

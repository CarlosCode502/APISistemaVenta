using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    //Prod min 12.00 part 4
    public class Producto_DTO
    {
        public int IdProducto { get; set; }

        public string? NombreProducto { get; set; }

        public int? IdCategoria { get; set; }

        //Se agrgó un nuevo campo
        public string? DescripcionCategoria { get; set; }

        public int? Stock { get; set; }

        //Se modifico de decimal a string
        public string? Precio { get; set; }

        //Se modifico de bool a int
        public int? EsActivo { get; set; }
    }
}
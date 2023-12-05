using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    //min 19.44
    public class DashBoard_DTO
    {
        public int TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }

        //Se agrego la propiedad total productos min 01.16.35 parte 5
        public int TotalProductos { get; set; }

        public List<VentasSemana_DTO>? VentasUltimaSemana { get; set; }
    }
}

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
        public List<VentasSemana_DTO>? VentasUltimaSemana { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    //min 18.43
    public class VentasSemana_DTO
    {
        public string? Fecha { get; set; }

        public int Total { get; set; }

        //Quería mostrar el total de ingreso en esta semana pero como que no (min 11.51 parte 15 )
        //public double? TotalIngreso { get; set; }
    }
}

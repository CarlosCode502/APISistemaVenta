﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    //min 17.23 part 2
    public class Reporte_DTO
    {
        public string? FechaRegistro { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? Producto { get; set; }

        public int? Cantidad { get; set; }

        public string? Precio { get; set; }

        public string? Total { get; set; }

        public string? TipoPago { get; set; }        

        public string? TotalVenta { get; set; }

    }
}

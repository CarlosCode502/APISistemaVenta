using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    /// <summary>
    /// Clase que contiene las propiedades del modelo que se van a mostrar únicamente luego del método y array. min 06.22
    /// part 4
    /// </summary>
    public class Menu_DTO
    {
        public int IdMenu { get; set; }

        public string? NombreMenu { get; set; }

        public string? Icono { get; set; }

        public string? Url { get; set; }
    }
}

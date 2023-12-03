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
    public class Rol_DTO
    {
        //Solo vamos a mostrar estos campos 06.22

        public int IdRol { get; set; }

        public string? Nombre { get; set; }
    }
}

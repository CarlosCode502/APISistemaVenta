using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    /// <summary>
    /// Clase que contiene las propiedades del modelo que se van a mostrar luego del método y array. min 06.22 part 4
    /// </summary>
    public class Usuario_DTO
    {
        public int IdUsuario { get; set; }

        public string? NombreCompleto { get; set; }

        public string? Correo { get; set; }        

        public int? IdRol { get; set; }

        //Se agrego para agregar el rol de la descripción
        public string? RolDescripcion { get; set; }

        public string? Clave { get; set; }

        //Se cambió el tipo de dato de bool a int
        public int? EsActivo { get; set; }

    }
}

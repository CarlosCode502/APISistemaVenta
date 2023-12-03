using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    /// <summary>
    /// Va a poder guardar la sesión del usuario que se ha logeado min 10.00 part4
    /// </summary>
    public class Sesion_DTO
    {
        public int IdUsuario { get; set; }

        public string? NombreCompleto { get; set; }

        public string? Correo { get; set; }

        //Se agrego para agregar el rol de la descripción
        public string? RolDescripcion { get; set; }
    }
}

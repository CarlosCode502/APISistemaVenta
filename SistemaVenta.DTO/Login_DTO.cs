using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    /// <summary>
    /// Clase que permite recibir las credenciales del usuario min 10.29 part 4
    /// y validar si corresponden.
    /// </summary>
    public class Login_DTO
    {
        public string? Correo { get; set; }

        public string? Clave { get; set; }
    }
}

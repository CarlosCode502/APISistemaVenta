using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class TblUsuario
{

    //Es una mala practica mostrar todas las propiedades asi como enviar el modelo completo min 02.48

    public int IdUsuario { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Correo { get; set; }

    public int? IdRol { get; set; }

    public string? Clave { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual TblRol? IdRolNavigation { get; set; }
}

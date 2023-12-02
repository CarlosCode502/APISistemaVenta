using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class TblRol
{
    public int IdRol { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<TblMenuRol> TblMenuRols { get; set; } = new List<TblMenuRol>();

    public virtual ICollection<TblUsuario> TblUsuarios { get; set; } = new List<TblUsuario>();
}

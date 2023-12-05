using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregar referencia min 24.49 parte 5
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface ICategoriaService
    {
        //Creando el único método que va a contener min 25.05 parte 5
        Task<List<Categoria_DTO>> Lista();
    }
}

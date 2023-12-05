using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregando referencia min 57.50 parte 5
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IDashBoardService
    {
        //Método para otener un resumen min 58.18 parte 5
        Task<DashBoard_DTO> Resumen();
    }
}

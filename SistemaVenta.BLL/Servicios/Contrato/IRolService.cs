using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregando referencias min 01.10 parte 5
using SistemaVenta.DTO;
using SistemaVenta.DAL;
using SistemaVenta.Model;
using SistemaVenta.Utility;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IRolService
    {
        //Método que se va a implementar dentro del servicio de rol
        Task<List<Rol_DTO>> ListaRol();
    }
}

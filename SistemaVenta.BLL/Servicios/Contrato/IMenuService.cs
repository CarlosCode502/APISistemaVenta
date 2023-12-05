using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregamos las referencias min 01.20.00 parte 5
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IMenuService
    {
        //Permite listar los menús dependiendo de los privilegios del usuario min 01.20.10 parte 5
        //Recibe el id del usuario
        Task<List<Menu_DTO>> ListaMenu(int idUsuario);
    }
}

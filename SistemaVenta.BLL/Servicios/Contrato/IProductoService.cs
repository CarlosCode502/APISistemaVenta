using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregando referencias min 28.09 parte 5
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IProductoService
    {
        //Método listar usuarios min 28.39 parte 5
        Task<List<Producto_DTO>> ListaProductos();

        //Método para crear productos min 28.42 parte 5
        Task<Producto_DTO> Crear(Producto_DTO modeloC);

        //Método para poder editar recibe un producto DTO con campo modelo min 28.48 parte 5
        Task<bool> Editar(Producto_DTO modeloE);

        //Método para poder editar recibe el id del producto a eliminar min 28.52 parte 5
        Task<bool> Eliminar(int id);
    }
}

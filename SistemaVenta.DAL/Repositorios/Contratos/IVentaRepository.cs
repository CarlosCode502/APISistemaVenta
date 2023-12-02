using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Model; //Agregar referencia

namespace SistemaVenta.DAL.Repositorios.Contratos
{
    //Herencia de IGenericRepository min 07.21
    //Accedemos a la interfaz en donde podremos acceder al modelo que necesitemos en este caso venta
    public interface IVentaRepository : IGenericRepository<TblVenta>
    {
        //Devuelve una venta
        Task<TblVenta> Registrar(TblVenta modeloVenta);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregando referencia min 40.50 parte 5
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    /// <summary>
    /// Contiene todos los métodos de esta interfaz. min 40.58 parte 5.
    /// </summary>
    public interface IVentaService
    {
        //Método que permite registrar la venta (recibe venta dto) min 41.25 parte 5.
        Task<Venta_DTO> Registrar(Venta_DTO modeloReg);

        //Devuelve una lista de tipo de venta_DTO, recine parametros(filtro) min 42.02 parte 5
        //Muestra los registros que coincidan por filtro
        Task<List<Venta_DTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);

        //Devuelve un reporte de ventas, recibe 2 parametros (filtros) parte min 43.05 parte 5
        Task<List<Reporte_DTO>> Reporte(string fechaInicio, string fechaFin);
    }
}

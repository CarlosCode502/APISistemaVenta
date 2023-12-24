using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Agregando las referencias del controlador en blanco min 18.30 parte 6
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad; //Aqui se encuentra response (comunicación)
using SistemaVenta.BLL.Servicios;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        //Var de solo lectura que contiene una def para la interfaz min 18.50 parte 6
        private readonly IVentaService _ventaService;

        //Inyectar la interfaz a la clase 
        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        /// <summary>
        /// Contiene la lógica para poder registrar una Venta min 19.08 parte 6
        /// </summary>
        /// <param name="ventaR">Recibe el modelo venta.</param>
        /// <returns>Respuesta exitosa/noexitosa.</returns>
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] Venta_DTO ventaR)
        {
            //Va a ser una nueva instancia a la clase response min 19.08 parte 6
            var rsp = new Response<Venta_DTO>(); //Error aqui no debi devolver un listado

            try
            {
                //Var rsp va retornar(Status es de response) un true
                rsp.Status = true;

                //Obtiene el valor de la ejecución del método al acceder al servicio min 19.08 parte 6
                rsp.Value = await _ventaService.Registrar(ventaR);

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error min
                rsp.Status = false;
                rsp.Msg = ex.Message;
            }

            //Retornar la respuesta de forma positiva min 19.08 parte 6
            return Ok(rsp);
        }

        /// <summary>
        /// Permite obtener un historial de venta según una busqueda o filtro. min. 20.20 parte 6
        /// </summary>
        /// <param name="buscarPor"></param>
        /// <param name="numeroVenta"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns>Respuesta exitosa / no exitosa.</returns>
        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor, string? numeroVenta, string? fechaInicio, string? fechaFin)
        {
            //Va a validar si se recibe una lista de dto min 20.20 parte 6
            var rsp = new Response<List<Venta_DTO>>();

            //Agregamos nueva lógica min 21.19v parte 6
            //Valida si numeroVenta es nulo que sea nulo sino que sea el numeroVenta
            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            //Misma logica si es nulo y si no (para fechas)
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            try
            {
                //Var rsp va retornar(Status es de response) un true min 21.58 parte 6
                rsp.Status = true;

                //Obtiene el valor del método accediendo al servicio min 22.30 parte 6
                rsp.Value = await _ventaService.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin);

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error
                //min 22.30 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 22.30 parte 6
            return Ok(rsp);
        }

        /// <summary>
        /// Permite obtener un reporte según 2 parametros o filtros. min. 23.10 parte 6
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns>Respuesta exitosa / no exitosa.</returns>
        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin)
        {
            //Va a validar si se recibe una lista de dto min 23.15 parte 6
            var rsp = new Response<List<Reporte_DTO>>();

            try
            {
                //Var rsp va retornar(Status es de response) un true min 23.15 parte 6
                rsp.Status = true;

                //Obtiene el valor del método accediendo al servicio min 23.28 parte 6
                rsp.Value = await _ventaService.Reporte(fechaInicio, fechaFin);

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error min 23.28 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 23.28 parte 6
            return Ok(rsp);
        }
    }
}

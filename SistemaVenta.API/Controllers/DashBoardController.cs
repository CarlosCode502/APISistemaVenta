using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Agregando las referencias del controlador en blanco min 24.00 parte 6
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad; //Aqui se encuentra response (comunicación)

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        //Var de solo lectura que contiene una ref para la interfaz min 24.10 parte 6
        private readonly IDashBoardService _dashboardservice;

        //Agregamos la interfaz y le pasamos la definicion 
        public DashBoardController(IDashBoardService dashboardservice)
        {
            _dashboardservice = dashboardservice;
        }

        /// <summary>
        /// Permite obtener un resumen con el total de ventas, etc. min 24.35 parte 6
        /// </summary>
        /// <returns>Respuesta exitosa/noexitosa.</returns>
        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            //Va a validar si se recibe una lista de dto min 24.49 parte 6
            var rsp = new Response<DashBoard_DTO>(); //No pasar una lista

            try
            {
                //Var rsp va retornar(Status es de response) un true min 24.49 parte 6
                rsp.Status = true;

                //Obtiene el valor del método accediendo al servicio min 25.15 parte 6
                rsp.Value = await _dashboardservice.Resumen();

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error min 25.15 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 25.15 parte 6
            return Ok(rsp);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Agregando las referencias del controlador en blanco min 02.22 parte 6
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad; //Aqui se encuentra response (comunicación)

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        //Crear el servicio que se va a implementar min 03.02 parte 6
        private readonly IRolService _rolService;

        //Constructor de esta clase
        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        //Único método que va a tener este controlador min 03.45 parte 6
        [HttpGet]
        [Route("Lista")] //Mismo nombre que el action
        public async Task<IActionResult> Lista()
        {
            //Va a ser una nueva instancia a la clase response min 04.38 parte 6
            //Que apunta al Rol_DTO
            var rsp = new Response<List<Rol_DTO>>();

            try
            {
                //Var rsp va retornar(Status es de response) un true min 05.09 parte 6
                rsp.Status = true;

                //Usar rolservice ya que este obtiene todos los roles
                rsp.Value = await _rolService.ListaRol();

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error
                //min 05.30 parte 6
                rsp.Status = false; 
                rsp.Msg = ex.Message;
               
            }

            //Retornar la respuesta de forma positiva min 05.53 parte 6
            return Ok(rsp);
        }
    }
}
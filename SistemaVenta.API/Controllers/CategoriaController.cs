using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Agregando las referencias del controlador en blanco min 13.50 parte 6
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad; //Aqui se encuentra response (comunicación)
using SistemaVenta.BLL.Servicios;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        //Variable privada de solo lectura que contiene una def para esta interfaz min 13.55 parte 6
        private readonly ICategoriaService _categoriaService;

        //Inyecta el servicio a la clase (para evitar asi tener que crear una nueva instancia) cada vez que 
        //se dese utilizar o sea declarada
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        //Único método que va a tener este controlador min 14.29 parte 6
        [HttpGet]
        [Route("Lista")] //Mismo nombre que el action
        public async Task<IActionResult> Lista()
        {
            //Va a validar si se recibe una lista de dto min 14.29 parte 6
            var rsp = new Response<List<Categoria_DTO>>();

            try
            {
                //Var rsp va retornar(Status es de response) un true min 14.29 parte 6
                rsp.Status = true;

                //Obtiene el valor del método accediendo al servicio min 14.42 parte 6
                rsp.Value = await _categoriaService.Lista();

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error
                //min 05.30 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 14.29 parte 6
            return Ok(rsp);
        }
    }
}

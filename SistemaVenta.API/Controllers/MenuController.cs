using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Agregando las referencias del controlador en blanco min 25.35 parte 6
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad; //Aqui se encuentra response (comunicación)

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        //Var de solo lectura que contiene una definicion para la interfaz min 25.46 parte 6
        private readonly IMenuService _menuService;

        //Agregando la Interfaz y relacionando la def
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// Permite obtener el listado de menus que le correspondan según su id usuario. min 25.46 parte 6
        /// </summary>
        /// <param name="Usuario">Id del usuario.</param>
        /// <returns>Respuesta exitosa/noexitosa.</returns>
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista(int idUsuario)
        {
            //Va a validar si se recibe una lista de dto min 26.30 parte 6
            var rsp = new Response<List<Menu_DTO>>();

            try
            {
                //Var rsp va retornar(Status es de response) un true min 26.30 parte 6
                rsp.Status = true;

                //Obtiene el valor del método accediendo al servicio min 26.51 parte 6
                rsp.Value = await _menuService.ListaMenu(idUsuario);

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error min 26.51 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 26.51 parte 6
            return Ok(rsp);
        }
    }
}

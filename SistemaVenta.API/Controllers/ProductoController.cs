using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Agregando las referencias del controlador en blanco min 15.16 parte 6
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad; //Aqui se encuentra response (comunicación)

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        //Se agrego iLoger para msj de log (extra ya teninedo parte 11 terminado bug de precio string)
        private readonly ILogger logger;

        //Variable privada de solo lectura que contiene una def para esta interfaz min 15.25 parte 6
        private readonly IProductoService _productoService;

        //Inyecta el servicio a la clase (para evitar asi tener que crear una nueva instancia) cada vez que 
        //se dese utilizar o sea declarada
        //Se agrego iLoger para msj de log (extra ya teninedo parte 11 terminado bug de precio string)
        public ProductoController(ILogger<ProductoController> logger,IProductoService productoService)
        {
            this.logger = logger;
            _productoService = productoService;
        }

        /// <summary>
        /// Obtiene un listado de productos. min 16.21 parte 6
        /// </summary>
        /// <returns>Respuesta exitosa/noexitosa.</returns>
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            //Va a validar si se recibe una lista de dto min 16.21 parte 6
            var rsp = new Response<List<Producto_DTO>>();

            try
            {
                //Var rsp va retornar(Status es de response) un true min 16.21 parte 6
                rsp.Status = true;

                //Obtiene el valor del método accediendo al servicio min 16.21 parte 6
                rsp.Value = await _productoService.ListaProductos();

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error
                //min 05.30 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 16.21 parte 6
            return Ok(rsp);
        }


        /// <summary>
        /// Permite validar si el producto fue guardado exitosamente. min 16.50 parte 6
        /// </summary>
        /// <param name="productoC">Recibe el modelo producto.</param>
        /// <returns>Respuesta exitosa/noexitosa.</returns>
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Producto_DTO? productoC)
        {
            //Va a ser una nueva instancia a la clase response min 16.50 parte 6
            var rsp = new Response<Producto_DTO>(); //Error aqui no debi devolver un listado

            try
            {
                //Var rsp va retornar(Status es de response) un true
                rsp.Status = true;

                //Obtiene el valor de la ejecución del método al acceder al servicio min 16.50 parte 6
                rsp.Value = await _productoService.Crear(productoC!);

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error min
                rsp.Status = false;
                rsp.Msg = ex.Message;
                //Se agrego iLoger para msj de log (extra ya teninedo parte 11 terminado bug de precio string)
                string message = $"Error de envio, los detalles son los siguientes:{ex.Message}";
                logger.LogError(message);
            }

            //Retornar la respuesta de forma positiva min 16.50 parte 6
            return Ok(rsp);
        }

        /// <summary>
        /// Permite validar si el producto se pudo editar correctamente. min 17.28 parte 6
        /// </summary>
        /// <param name="usuarioE">Recibe el modelo productoDTO.</param>
        /// <returns>Respuesta exitosa/noexitosa.</returns>
        [HttpPut]//Se cambio
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Producto_DTO productoE)
        {
            //Va a ser una nueva instancia a la clase response min 17.28 parte 6
            var rsp = new Response<bool>(); //Error aqui no debi devolver un listado

            try
            {
                //Var rsp va retornar(Status es de response) un true min 17.28 parte 6
                rsp.Status = true;

                //Obtiene el valor de la ejecución del método al acceder al servicio min 17.28 parte 6
                rsp.Value = await _productoService.Editar(productoE);

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error min 17.28 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 17.28 parte 6
            return Ok(rsp);
        }

        /// <summary>
        /// Permite validar si el producto se pudo eliminar correctamente. min 17.50 parte 6
        /// </summary>
        /// <param name="id">El id del producto.</param>
        /// <returns>Respuesta exitosa/noexitosa.</returns>
        [HttpDelete] //Se cambio
        [Route("Eliminar/{id:int}")] //Especificamos la url(y id) durante la ejecución 
        public async Task<IActionResult> Eliminar(int id)
        {
            //Va a ser una nueva instancia a la clase response min 17.50 parte 6
            var rsp = new Response<bool>(); //Error aqui no debi devolver un listado

            try
            {
                //Var rsp va retornar(Status es de response) un true min 17.50 parte 6
                rsp.Status = true;

                //Obtiene el valor de la ejecución del método al acceder al servicio
                rsp.Value = await _productoService.Eliminar(id);

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 17.50 parte 6
            return Ok(rsp);
        }
    }
}

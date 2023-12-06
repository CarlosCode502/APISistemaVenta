using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Agregando las referencias del controlador en blanco min 06.55 parte 6
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad; //Aqui se encuentra response (comunicación)
using SistemaVenta.BLL.Servicios;

namespace SistemaVenta.API.Controllers
{
    /// <summary>
    /// Contiene la lógica para ejecutar los servicios parte 6
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //Para acceder a la IUsuarioServicio 
        private readonly IUsuarioService _usuarioServicio;

        //Constructor para inyectar la interfaz a esta clase min 07.22 parte 6
        public UsuarioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            //Va a ser una nueva instancia a la clase response min 07.52 parte 6
            //Que apunta al Usuario_DTO
            var rsp = new Response<List<Usuario_DTO>>();

            try
            {
                //Var rsp va retornar(Status es de response) un true min 07.52 parte 6
                rsp.Status = true;

                //Usar rolservice ya que este obtiene todos los roles
                rsp.Value = await _usuarioServicio.ListaUsuarios();

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error
                //min 05.30 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 07.52 parte 6
            return Ok(rsp);
        }


        //Action / API para validar las credenciales del usuario min 08.50 parte 6
        //Devuelve desde el cuerpo un login que contiene las credenciales del usuario
        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] Login_DTO login)
        {
            //Va a ser una nueva instancia a la clase response min 09.29 parte 6
            var rsp = new Response<Sesion_DTO>(); //Error aqui no debi devolver un listado

            try
            {
                //Var rsp va retornar(Status es de response) un true min 09.29 parte 6
                rsp.Status = true;

                //Usar Validar credenciales min 09.42 parte 6
                rsp.Value = await _usuarioServicio.ValidarCredenciales(login.Correo, login.Clave);

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error min 05.30 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 09.42 parte 6
            return Ok(rsp);
        }

        //Action / API para validar las credenciales del usuario min 11.05 parte 6
        //Devuelve desde el cuerpo un GuardarUsuario
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Usuario_DTO usuario)
        {
            //Va a ser una nueva instancia a la clase response min 11.05 parte 6
            var rsp = new Response<Usuario_DTO>(); //Error aqui no debi devolver un listado

            try
            {
                //Var rsp va retornar(Status es de response) un true min 11.05 parte 6
                rsp.Status = true;

                //Obtiene el value de uservice metodo crear y le pasa el usuario min 11.05 parte 6
                rsp.Value = await _usuarioServicio.Crear(usuario);

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error min 11.05 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 11.05 parte 6
            return Ok(rsp);
        }

        //Action / API para validar las credenciales del usuario min 11.56 parte 6
        //Devuelve desde el cuerpo un EditarUsuario
        [HttpPut]//Se cambio
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Usuario_DTO usuarioE)
        {
            //Va a ser una nueva instancia a la clase response min 11.56 parte 6
            var rsp = new Response<bool>(); //Error aqui no debi devolver un listado

            try
            {
                //Var rsp va retornar(Status es de response) un true min 11.56 parte 6
                rsp.Status = true;

                //Obtiene el usuarioservice y da acceso a los métodos min 11.56 parte 6
                rsp.Value = await _usuarioServicio.Editar(usuarioE);

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error min 11.56 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 11.56 parte 6
            return Ok(rsp);
        }

        //Action / API para validar las credenciales del usuario min 12.38 parte 6
        //Devuelve desde el cuerpo un EliminarUsuario
        [HttpDelete] //Se cambio
        [Route("Eliminar/{id:int}")] //Especificamos la url(y id) durante la ejecucion 
        public async Task<IActionResult> Eliminar(int id)
        {
            //Va a ser una nueva instancia a la clase response min 12.38 parte 6
            var rsp = new Response<bool>(); //Error aqui no debi devolver un listado

            try
            {
                //Var rsp va retornar(Status es de response) un true min 12.38 parte 6
                rsp.Status = true;

                //Obtiene el usuarioservice y da acceso a los métodos min 12.38 parte 6
                rsp.Value = await _usuarioServicio.Eliminar(id);

            }
            catch (Exception ex) //Si se ocupara el exception
            {
                //Devuelve el error min 12.38 parte 6
                rsp.Status = false;
                rsp.Msg = ex.Message;

            }

            //Retornar la respuesta de forma positiva min 12.38 parte 6
            return Ok(rsp);
        }
    }
}
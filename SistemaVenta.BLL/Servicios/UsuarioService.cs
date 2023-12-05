using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregar referencias min 10.00 part5
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class UsuarioService : IUsuarioService //Luego de importar se trae todos los métodos min 11.00 parte 5
    {
        //Creando un clase génerica que contiene una definicion min 10.39 part5
        private readonly IGenericRepository<TblUsuario> _usuarioRepositorio;

        //Variable para trabajar con IMapper
        private readonly IMapper _mapper;

        //Se genero en acciones rápidas (Teniendo seleccionado lo de arriba) min 11.05 part 5
        public UsuarioService(IGenericRepository<TblUsuario> usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        //Métodos de la interfaz

        public async Task<List<Usuario_DTO>> ListaUsuarios()
        {
            try
            {
                //Devuelve un query de un usuario
                var queryUsuario = await _usuarioRepositorio.Consultar();

                //Obtiene todos los roles que le pertenecen a los usuarios min 12.25 part 5
                //IdRolNavigation contiene la definición para todos los usuarios
                var listaUsuarios = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();

                //Devolver la respuesta del método
                //Donde especificamos a que se desea convertir (luego el origen en parentesis) min 12.58 part 5
                return _mapper.Map<List<Usuario_DTO>>(listaUsuarios);
            }
            catch
            {
                //Que arroje el error
                throw;
            }
        }

        public async Task<Sesion_DTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                //Una consulta de un usuario que cumpla con los parametros solicitados min 13.57 part 5
                //(Se especifica con un filtro) y se obtiene el que cumpla
                var queryUsuario = await _usuarioRepositorio.Consultar(u => u.Correo == correo && u.Clave == clave);

                //Se valida si exista o un valor por defecto y si es nulo has esta validación min 14.46 part 5
                if(queryUsuario.FirstOrDefault() == null)
                {
                    //SI no existe se interrupe el proceso
                    throw new TaskCanceledException("El usuario no existe");
                }

                //Variable que se va dovelver la respuesta ya que si existe el usuario se toma el rol es decir
                //El primer elemento que se encontro (ya que si contiene uno) min 14.37 part 5 
                TblUsuario devolver = queryUsuario.Include(rol => rol.IdRolNavigation).First();

                //Necesitamos retornar el modelo usuario pero a través de sesion_DTO min 15.50 part 5
                //Esto se logra a través de un (detino, origen)
                return _mapper.Map<Sesion_DTO>(devolver);                
            }
            catch
            {
                //Que arroje el error
                throw;
            }
        }

        public async Task<Usuario_DTO> Crear(Usuario_DTO modeloC)
        {
            try
            {
                //Recibe un usuario no un modelo en si para eso se debe de mapear como el modelo min 17.05 part 5
                //para pasarlos como usuario se debe especificar el modelo y usuario
                var usuarioCreado = await _usuarioRepositorio.Crear(_mapper.Map<TblUsuario>(modeloC));

                //Valdia si el usuario es = a 0 quiere decir que no se creó correctamente min 17.34 part 5
                if(usuarioCreado.IdUsuario == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el usuario");
                }

                //Si se pudo crear va a continuar 
                //Obtenemos al usuario creado a partir del id min 18.02 part 5
                var query = await _usuarioRepositorio.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);

                //Actualizamos el usuario creado (el campo rol recordando que irolnav tiene acceso) min 18.35 part 5
                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();

                //Necesitamos retornar el usuario creado especificando (el destino y el origen) min 19.00 part 5
                return _mapper.Map<Usuario_DTO>(usuarioCreado);

            }
            catch (Exception)
            {
                //Devuelve el error
                throw;
            }
        }

        public async Task<bool> Editar(Usuario_DTO modeloE)
        {
            try
            {
                //Necesitamos a maper ya que necesitamos acceder al modelo y convertirlo en un usuario pero que
                //pertenezca a la clase de nuestros modelos min 19.18 parte 5
                var usuarioModelo = _mapper.Map<TblUsuario>(modeloE);

                //Obtiene una busqueda para saber si ese usuario existe o no en la bd (recibe un filtro) min 20.09 parte 5
                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);

                //SI usuario encontrado es = a nulo entonces no existe el usuario min 20.37 parte 5
                if(usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("El usuario no existe.");
                }

                //SI el usuario si existe se editaran sus propiedades 21.00 p5
                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                //Obtenemos una respuesta si el usuario fue editado exitosamente (obtiene un true) ? false 21.59 p5
                bool respuesta = await _usuarioRepositorio.Editar(usuarioEncontrado);

                //Si respuesta es falso va mostrar un error min 22.24 parte 5
                if(!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar."); 
                }

                //Si es true se retorna la respuesta min 22.38 parte 4
                return respuesta;
            }
            catch
            {
                //Devuelve el error
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                //Obtener el usuario pasando un filtro con el id min 22.54 parte 5
                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == id);

                //Valida si el usuario existe si no muestra un msj de error min 23.12 parte 5
                if (usuarioEncontrado == null) { throw new TaskCanceledException("Usuario no encontrado."); }

                //Si existe el usuario va a devolver un true caso contrario fañse min 23.32 parte 5
                bool respuesta = await _usuarioRepositorio.Eliminar(usuarioEncontrado);

                //Válida si respuesta es false muestra msj de error min 23.49 parte 5
                if(!respuesta) { throw new TaskCanceledException("No se pudo eliminar el usuario"); }

                //Se retorna la respuesta si es true min 24.05 parte 5
                return respuesta;
            }
            catch
            {
                //Devuelve el error
                throw;
            }
        }
    }
}
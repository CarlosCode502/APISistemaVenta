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

                //Actualizamos el usuario creado 

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> Editar(Usuario_DTO modeloE)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}

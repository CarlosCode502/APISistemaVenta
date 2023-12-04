using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregar referencias min 02.20 part5
using AutoMapper;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class RolService : IRolService //Implementar la interfaz min 04.28 part5
    {
        //Creando un clase génerica que contiene una definicion min 03.19 part5
        private readonly IGenericRepository<TblRol> _rolRepositorio;

        //Variable para trabajar con IMapper
        private readonly IMapper _mapper;

        //Se genero en acciones rápidas (Teniendo seleccionado lo de arriba) min 04.00 part 5
        public RolService(IGenericRepository<TblRol> rolRepositorio, IMapper mapper)
        {
            _rolRepositorio = rolRepositorio;
            _mapper = mapper;
        }

        //Obtiene el método declarado en rolService
        public async Task<List<Rol_DTO>> ListaRol()
        {
            //Desarrollar toda la lógica
            try
            {
                //Obtener el listado de roles a través del método consultar min 05.04 part5
                var listaRoles = await _rolRepositorio.Consultar();
                //Va a retornar un mapeo (donde se obtiene un Iqueryable y se desea convertir en un rol_dto)
                //(Origen = listadoRoles) => (Destino = Rol_DTO)
                //Convertir un Rol_DTO en lista min 6.39 part5
                return _mapper.Map<List<Rol_DTO>>(listaRoles).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}

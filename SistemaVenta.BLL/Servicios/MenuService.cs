using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregar referencias min 01.20.36 parte 5
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.Model;


namespace SistemaVenta.BLL.Servicios
{
    public class MenuService : IMenuService //Agregar la interfaz min 01.20.44 parte 5
    {
        //Contiene un def para la clase génerica de la tbl producto min 01.21.10 parte 5
        private readonly IGenericRepository<TblUsuario> _usuarioRepositorio;

        //Contiene un def para la clase génerica de la tbl producto
        private readonly IGenericRepository<TblMenuRol> _menuRolRepositorio;

        //Contiene un def para la clase génerica de la tbl producto
        private readonly IGenericRepository<TblMenu> _menuRepositorio;

        //Contiene una def para Mapper
        private readonly IMapper _mapper;

        //Creado automáticamente (tener seleccionado las def anteriores) min 01.21.45 parte 5
        public MenuService(IGenericRepository<TblUsuario> usuarioRepositorio, IGenericRepository<TblMenuRol> menuRolRepositorio, IGenericRepository<TblMenu> menuRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _menuRolRepositorio = menuRolRepositorio;
            _menuRepositorio = menuRepositorio;
            _mapper = mapper;
        }

        //Único método que contiene la interfaz min 01.22.01 parte 5
        public async Task<List<Menu_DTO>> ListaMenu(int idUsuario)
        {
                //Creamos una consulta que obtiene los usuarios según el usuarioId
                IQueryable<TblUsuario> tblUsuarios = await _usuarioRepositorio.Consultar(u => u.IdUsuario == idUsuario);

                //Una consulta para obtener los menu rol
                IQueryable<TblMenuRol> tablaMenuRol = await _menuRolRepositorio.Consultar();

                //Obtiene los registros del modelo tbl menu
                IQueryable<TblMenu> tablaMenu = await _menuRepositorio.Consultar();

            try
            {
                //Consulta haciedo referencia a tblmenu (inner join de tblUsuario con tbmMenuRol) min 01.24.11 parte 5
                //De la tablaUsuarios union con tablaMenuRol (especificar la union) de u.IdRol a mr.IdRol
                //Que va a especifar la conexion de la tablaMenu de mr.IdMenu a m.IdMenu
                //Finalmente seleccionamos m que retorna un listado
                //(u = tblUsuario, mr = tablaMenuRol, m = tablaMenu) son alias 
                IQueryable<TblMenu> tblResultado = (from u in tblUsuarios
                                                    join mr in tablaMenuRol on u.IdRol equals mr.IdRol
                                                    join m in tablaMenu on mr.IdMenu equals m.IdMenu
                                                    select m).AsQueryable();

                //Ontener el resultado en una lista
                var listaMenus = tblResultado.ToList();

                //Mapear el listado de origen(listaMenus) a destino(Menu_DTO) 
                return _mapper.Map<List<Menu_DTO>>(listaMenus);
            }
            catch
            {
                //Devuelve el error
                throw;
            }
        }
    }
}

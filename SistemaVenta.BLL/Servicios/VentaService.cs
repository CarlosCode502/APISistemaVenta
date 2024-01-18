using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregar referencias min 43.20 part5
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.Model;


namespace SistemaVenta.BLL.Servicios
{
    public class VentaService : IVentaService //Agregar e implementar interfaces min 45.01 parte 5
    {
        //Contiene una definicion para la iventarepo (SV.DAL.Repositorios) min 44.32 parte 5
        private readonly IVentaRepository _ventaRepositorio;

        //Clase génerica que contiene una definicion para la tabla a la que se hace referencia min 44.12 parte 5
        private readonly IGenericRepository<TblDetalleVenta> _detalleVentaRepositorio;

        //Contiene una definicion para mapper 44.20 parte 5
        private readonly IMapper _mapper;

        static int[] myArray = { 13, 2, 4, 35, 1 };

        //Se genero automaticamente en acciones rápidas (teniendo seleccionado lo de arriba) min 44.51 parte 5
        public VentaService(IVentaRepository ventaRepositorio, IGenericRepository<TblDetalleVenta> detalleVentaRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }

        //Se obtienen los métodos de la interfaz para poder desarrollar la lógica min 45.18 parte 5

        public async Task<Venta_DTO> Registrar(Venta_DTO modeloReg)
        {
            try
            {
                //Obtien la venta generada a través de método generar (espec modelo y definicion) min 45.45 parte 5
                var ventaGenerada = await _ventaRepositorio.Registrar(_mapper.Map<TblVenta>(modeloReg));

                //Valida si la venta se pudo generar (si no esta vacio o nulo según su id) min 46.22 parte 5
                if (ventaGenerada.IdVenta == 0) { throw new TaskCanceledException("No se pudo crear la venta."); }

                //Si es diferente de 0 se retorna la venta mapeada min 47.09 parte 5
                //Retorna el modelo tblventa en un ventadto (contiene numdoc y idventa)
                return _mapper.Map<Venta_DTO>(ventaGenerada);
            }
            catch
            {
                //Devuelve el error
                throw;
            }
        }

        public async Task<List<Venta_DTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            //Query que genera una consulta de la tabla venta min 47.55 parte r
            IQueryable<TblVenta> query = await _ventaRepositorio.Consultar();

            //Contiene la lista de la tabla venta min 48.30 parte 5
            var listaResultado = new List<TblVenta>();

            try
            {
                //Valida la busqueda (if anidado) min 48.47 parte 5
                if (buscarPor == "fecha")
                {
                    //En caso de que el parametro búscar por sea = "fecha"

                    //parsear a un formato especifico 
                    DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("en-GT"));

                    //Para fecha de fin
                    DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("en-GT"));

                    // Va obtener un resultado de la query(ejecuta una consulta a la tabla tblventa)min 50.30 parte 5
                    //(filtros o rango) Cuando la fecha registro sea >= a la fecha de inicio y <= a la fecha fin
                    listaResultado = await query.Where(v =>
                    v.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                    v.FechaRegistro.Value.Date <= fech_Fin.Date
                    ).Include(dv => dv.TblDetalleVenta) //(Include)Luego se incluyen la propiedades de TblDetalleVenta
                    .ThenInclude(p => p.IdProductoNavigation) //ThenInclude(añadir los productos por cada detalleventa)
                    .ToListAsync(); //Obtenemos un listado asyncrono min 52.41 parte 5
                }
                //else /*if (buscarPor == "numero")*/
                //{
                //    //if (buscarPor == "" || buscarPor.IsNullOrEmpty())
                //    //{
                //    //    buscarPor = "0";
                //    //}

                //    string numeroObtenido = numeroVenta;
                //    //Va a ser búsqueda por NumeroDeDocumento min 53.27 parte 4
                //    //Entonces listaResultado espera una consulta cuando el NumeroDoc sea igual al numeroVenta
                //    listaResultado = await query.Where(v => v.NumeroDocumento == numeroObtenido)
                //        .Include(dv => dv.TblDetalleVenta)
                //        .ThenInclude(p => p.IdProductoNavigation)
                //        .ToListAsync();
                //}

                //SIN MODIFICAR
                else
                {
                    //Va a ser búsqueda por NumeroDeDocumento min 53.27 parte 4
                    //Entonces listaResultado espera una consulta cuando el NumeroDoc sea igual al numeroVenta
                    //Por cada detalle de venta tambien su producto
                    listaResultado = await query.Where(v => v.NumeroDocumento == numeroVenta)
                        .Include(dv => dv.TblDetalleVenta)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .ToListAsync();
                }
            }
            catch
            {
                //Devuelve el error
                throw;
            }

            //Al final retornaremos el listaResultado (pero es necesario mapear) min 53.52 parte 5
            //Ya que recibimos un TblVenta y necesitamos un Venta_DTO
            return _mapper.Map<List<Venta_DTO>>(listaResultado);
        }

        public async Task<List<Reporte_DTO>> Reporte(string fechaInicio, string fechaFin)
        {
            //Crear un iquerable que obtiene una consulta min 54.36 parte 5
            IQueryable<TblDetalleVenta> query = await _detalleVentaRepositorio.Consultar();

            //obtenemos un listado de tipo TblDetalleVenta
            var listaResultado = new List<TblDetalleVenta>();

            try
            {
                //Creamos 2 variables de tipo DT que recibe un parseo con la cultura y formato min 55.10 parte 5
                DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-GT"));
                DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-GT"));

                //Obtener todos los listados que cumplan con un rango parte 5
                listaResultado = await query
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv => 
                        dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_Inicio.Date && 
                        dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_Fin.Date)
                    .ToListAsync(); //min 57.00 parte 5
            }
            catch
            {
                //Devuelve el error
                throw;
            }

            //retornamos la var lista resultado pero antes se debe hacer un mapeo (TblDetalleVenta a ReporteDto) min 57.23 p5
            return _mapper.Map<List<Reporte_DTO>>(listaResultado);
        }
    }
}
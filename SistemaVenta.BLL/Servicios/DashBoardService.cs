using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregar referencias min 58.46 part5
using AutoMapper;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class DashBoardService : IDashBoardService //Add ref a la interfaz min 59.00 parte 5 (imp luego del constr)
    {
        //Contiene una definición para la interfaz IVentaRepositori min 59.30 parte 5
        private readonly IVentaRepository _ventaRepositorio;
        //Contiene un def para la clase génerica de la tbl producto
        private readonly IGenericRepository<TblProducto> _productoRepositorio;
        //Contiene una def para Mapper
        private readonly IMapper _mapper;

        //#-- Agregado para poder obtener el total de productos vendidos 16/01/2024 13.21pm
        private readonly IGenericRepository<TblDetalleVenta> _detalleVentaRepositorio;

        //Generado automáticamente (teniendo seleccionado las definiciones anteriores) min 59.46 parte 5
        public DashBoardService(IVentaRepository ventaRepositorio, IGenericRepository<TblProducto> productoRepositorio, IMapper mapper, IGenericRepository<TblDetalleVenta> detalleVentaRepositorio)
        {
            _ventaRepositorio = ventaRepositorio;
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
            //#-- Agregado para poder obtener el total de productos vendidos 16/01/2024 13.21pm
            _detalleVentaRepositorio = detalleVentaRepositorio;
        }

        //Serie de métodos que nos permitirán obtener el resumen min 01.00.00 parte 5

        //#-- Método para obtener el día actual de la semana 16/01/2024 18.03
        private int obtenerNumeroDeSemana()
        {
            
            int diaSemana = (int)DateTime.Now.DayOfWeek;

            //#-- Para obtener el dia de la semana
            //foreach(var diaDeSemana in Enum.GetValues(typeof(DayOfWeek))){
            //    obtenerDiaSemana = (int)diaDeSemana * -1;
            //}

            int obtenerDiaSemana = diaSemana*-1;

            return obtenerDiaSemana;
        }


        //private ya que solo debe tener acceso esta clase (Retorna una tabla de ventas)
        //restarCantidadDias (Para devolver todo un rango de ventas según una fecha eje fechahoy - 9dias ) min 01.01.30
        //Los dias que se van an restar van a ser los registros que se van a tomar para mostrar
        private IQueryable<TblVenta> RetornarVentas(IQueryable<TblVenta> tablaVentas, int restarCantidadDias)
        {
            //Obtener la última fecha registrada en la tabla min 01.02.10 parte 5
            //Recibe un filtro para devolver las fechas oredenadas de forma desendente de mayor a menor (dev el primero encont)
            DateTime? ultimaFecha = tablaVentas.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

            //A la útlima fecha le restamos dias especificos min 01.02.35 parte 5
            //Se obtiene la últimafecha y se le restan dias para obtener las demás fechas
            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            //Devulve la tabla de venta aplicando el filtro min 01.03.07 parte 5
            //A la última fecha  que se ha modificado 
            return tablaVentas.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        //#-- Método que permite retornar los productos vendidos en la semana 16/01/2024 13.28 pm
        //#-- Recibe como parametros consulatas de tipo(TblVenta, TblDetalleVenta) y un entero con la cantidad de dias -
        private IQueryable<TblDetalleVenta> RetornarProd(IQueryable<TblVenta> tablaVentas, IQueryable<TblDetalleVenta> tblDetalleVentas, int restarCantidadDias)
        {
            //Obtener la última fecha registrada en la tabla min 01.02.10 parte 5
            //#-- 16/01/2024 13.28 pm
            //#-- Recibe un filtro para devolver las fechas oredenadas de forma desendente de mayor a menor (dev el primero encont)
            DateTime? ultimaFecha = tablaVentas.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

            //A la útlima fecha le restamos dias especificos min 01.02.35 parte 5
            //Se obtiene la últimafecha y se le restan dias para obtener las demás fechas
            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            //Devulve la tabla de venta aplicando el filtro min 01.03.07 parte 5
            //A la última fecha  que se ha modificado 
            //return tablaVentas.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
            //#-- Obtiene las ventas que sean mayores a los ultimos 7 dias y selecciona el id de la venta 16/01/2024 13.36 pm
            var obtenrVentas = tablaVentas.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date).Select(p => p.IdVenta).First(); //Obtiene el primer elemento de la última venta

            //obtenrVentas = obtenrVentas.Select(vId => vId.IdVenta).Where()

            //#-- Se retornan el id de las ventas >= al id de la última venta entre el rango de fechas 16/01/2024 13.43 pm
            return tblDetalleVentas.Where(v => v.IdVenta >= obtenrVentas);
        }


        //Método que devuelve un entero con el número de ventas de la última semana min 01.03.45 parte 5
        //De la última semana es decir (-7 dias)
        private async Task<int> TotalVentasUltimaSemana()
        {
            //Contado que va a llevar el total que va a empezar desde 0
            int total = 0;

            //Va a realizar una consulta a la tabla venta
            IQueryable<TblVenta> _ventaQuery = await _ventaRepositorio.Consultar();

            //Si _ventQuery es no es 0 (es decir si existe algun elemento se realiza una accion) min 01.04.36 parte 5
            if (_ventaQuery.Count() > 0)
            {
                //Ejecutamos el método ejecutar ventas (min 01.00.00 parte 5) creado arriba
                //Que recibe un query y un entero(que es el diarestar en este caso una semana -7 dias) min 01.05.00 parte 5
                var tablaVenta = RetornarVentas(_ventaQuery, obtenerNumeroDeSemana());

                //Obtenemos el total de ventas que han sido registradas hace 7 dias
                total = tablaVenta.Count();
            }

            //Finalmente retornamos el total (que es lo que devuelve este metodo) min 01.05.15 parte 5
            return total;
        }


        //Obtener el total de ingresos de la útlima semana min 01.05.50 parte 5
        //Es string ya que al final se va a mandar un formato con cultureinfo
        private async Task<string> TotalIngresosUltimaSemana()
        {
            //Var decimal que va a contener el total
            decimal resultado = 0;

            //Realizamos una consulta a la tabla venta y se guarda como modelo tblventa min 01.06.36 parte 5
            //obtiene todas las ventas
            IQueryable<TblVenta> _ventaQuery = await _ventaRepositorio.Consultar();

            //Valida si existen ventas
            if (_ventaQuery.Count() > 0)
            {
                //Obtenemos la tabla de ventas donde retornamos las ventas de la útlima semana
                //Usamos el método retornarventas de la última semana
                var tablaVenta = RetornarVentas(_ventaQuery, obtenerNumeroDeSemana());

                //Actualizar / aumentar la var resultado con la cantidad de totalingresos
                //Va a obtener todos los totales y va a obtener la suma de los valores
                resultado = tablaVenta.Select(v => v.Total).Sum(v => v.Value);
            }

            //Retornamos el resultado (necesita conversion de string a decimal) min 01.08.03 parte 5
            return Convert.ToString(resultado, new CultureInfo("es-GT"));
        }

        //Método para obtener el total de productos min 01.08.35 parte 5
        private async Task<int> TotalProductos()
        {
            //#-- Quitar comentario 1 vez para dejarlo como al inicio 16/01/2024 13.08 pm

            //////#-- Contendrá el total de productos
            ////int total = 0;

            ////Obtener todos los productos (Query o consulta)
            //IQueryable<TblProducto> _productoQuery = await _productoRepositorio.Consultar();

            //////#-- Agregado para obtener los productos vendidos de la útlima semana
            //////#-- Obtener el total de productos min 01.09.17 parte 5
            ////if (_productoQuery.Count() > 0)
            ////{
            ////    //Ejecutamos el método ejecutar ventas (min 01.00.00 parte 5) creado arriba
            ////    //Que recibe un query y un entero(que es el diarestar en este caso una semana -7 dias) min 01.05.00 parte 5
            ////    var tablaVenta = RetornarVentas(_productoQuery, -7);

            ////    //Obtenemos el total de ventas que han sido registradas hace 7 dias
            ////    total = tablaVenta.Count(); ;
            ////}
            ///
            //////#-- Retornamos el total de productos min 01.09.30 parte 5
            ////return total;

            ////#--SIN MODIFICAR
            ////Obtener el total de productos min 01.09.17 parte 5
            //int total = _productoQuery.Count();

            ////Retornamos el total de productos min 01.09.30 parte 5
            //return total;


            //#-- Para obtener el total de productos vendidos 16/01/ 2024 13.08 pm
            //#-- Contendrá el total de productos vendidos 
            int totalProdVendidos = 0;

            //#-- Obtiene todos los registros de la tabla venta 16/01/2024 
            IQueryable<TblVenta> _ventaQuery = await _ventaRepositorio.Consultar();
            //#-- Obtiene todos los registros de detalleventa
            IQueryable<TblDetalleVenta> _detalleventaQuery = await _detalleVentaRepositorio.Consultar();

            //Valida si existen ventas
            if (_ventaQuery.Count() > 0)
            {
                //Obtenemos la tabla de ventas donde retornamos las ventas de la útlima semana
                //Usamos el método retornarventas de la última semana
                //var tablaVenta = _ventaQuery.Where(v => v.IdVenta > 61).Count();
                //#-- Obtiene el total de detalleventas pasando como parametros todas (las ventas, detallesventas, ultimos 7 dias)
                var tablaVenta = RetornarProd(_ventaQuery,_detalleventaQuery, obtenerNumeroDeSemana());

                
                //#-- Se obtiene la cantidad de productos vendidos de los últimos 7 dias
                totalProdVendidos = tablaVenta.Count();
            }

            //Retornamos el resultado (necesita conversion de string a decimal) min 01.08.03 parte 5
            //#-- Retorna los productos vendidos 16/01/2024 13.50 pm aprox
            return totalProdVendidos;
        }


        //Método que obtiene el total de ventas de las útlimas semanas min 01.09.53 parte 5
        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            //Se crea un nuevo tipo diccionario
            Dictionary<string, int> resultado = new Dictionary<string, int>();

            //Consulta o obtener todas las ventas
            IQueryable<TblVenta> _ventaQuery = await _ventaRepositorio.Consultar();

            //Valida si existen ventas min 01.11.01 parte 5
            if (_ventaQuery.Count() > 0)
            {
                //Se ejecutan las sig intrucciones

                //Obtenemos las ventas de la útlima semana con el método anteriormente creado
                var tablaVenta = RetornarVentas(_ventaQuery, obtenerNumeroDeSemana());

                //Se obtendrá el resultado de la tabla venta y agrupara aplicando un filtro min 01.11.34 parte 5
                //Agrupar por fecha de registro y se ordenara por esa misma columna
                //(Select)Selecciona fecha y el total ya que necesitamos saber la cantidad de ventas * día se obtiene un total de agrupacion por cada fecha
                //(ToDictionary) Estos datos se van a convertir en un diccionario donde la llave selectora va a ser por la fecha
                //Key = fecha, Element = total
                resultado = tablaVenta
                    .GroupBy(v => v.FechaRegistro.Value.Date)
                    .OrderBy(g => g.Key) //Hace referencia a v => v.FechaRegistro.Value.Date
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }

            //Retornamos el resultado min 01.14.45 parte 5
            return resultado;
        }

        //Úncio método de la InterfazDashBoard min 01.15.00 parte 5
        public async Task<DashBoard_DTO> Resumen()
        {
            //Se crea un objeto del tipo DashBoardDTO min 01.15.39 parte 5
            DashBoard_DTO vmdashBoard_DTO = new DashBoard_DTO();

            try
            {
                //Eviamos o actualizamos el dashboard_dto con el valor que obtiene el método totaldeventasultimasem 
                vmdashBoard_DTO.TotalVentas = await TotalVentasUltimaSemana();

                //Actualizamod el total de productos 
                vmdashBoard_DTO.TotalIngresos = await TotalIngresosUltimaSemana();

                //Total de productos se agrego al último min 01.16.57 parte 5
                vmdashBoard_DTO.TotalProductos = await TotalProductos();

                //Objeto que crea o inicializa un nuevo listado de ventas semanas min 01.17.25 parte 5
                List<VentasSemana_DTO> listaVentaSemana = new List<VentasSemana_DTO>();

                //Itera o reccorre cada item del diccionario VentaUltyimaSemana min 01.17.40 parte 5
                //Cada elemento va a ser del tipo KeyValuePair<stirng, int> <texto, valor> 
                //in await VentasUltimaSemana
                foreach (KeyValuePair<string, int> item in await VentasUltimaSemana())
                {
                    //Cada iteración se va a ejecutar o se va a actualizar el listado de ventas 
                    //que corresponde a fecha y total min 01.18.50 parte 5
                    listaVentaSemana.Add(new VentasSemana_DTO
                    {
                        //Propiedades o valores que va a tener
                        Fecha = item.Key,
                        Total = item.Value
                    });                    
                }

                //Al final ya es posible actualizar el módelo ventasUltimaSemana
                vmdashBoard_DTO.VentasUltimaSemana = listaVentaSemana;
            }
            catch
            {
                //Devuelve el error
                throw;
            }

            //Retornamos el modelo con datos min 01.19.30 parte 5
            return vmdashBoard_DTO;
        }
    }
}
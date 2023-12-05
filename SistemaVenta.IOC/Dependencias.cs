using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.DAL.DBContext;


//Agregar referencias min 33.55 pt3
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DAL.Repositorios;

//Usar las dependencias de la clase AutoMapperProfile 49.30 parte 4
using SistemaVenta.Utility;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.BLL.Servicios;

namespace SistemaVenta.IOC
{
    
    /// <summary>
    /// Inyección de dependencias clase para agregar las dependencias
    /// </summary>
    public static class Dependencias
    {
        //Método que va recibir un servicio de colecciones para inyectar las dependencias (Método de extensión)
        //IService Collection es un servicio que pertenece a .net se va a agregar el método
        //Pasamos el iconfiguration
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            //Agregar el contexto de la base de datos (Como existe ref a DAL es posible acceder al contexto de la BD)
            //Estamos asignando nuevamente la cadena de conexión al contexto BdSysventaAngNetContext (min 24.50 aprox)
            services.AddDbContext<BdSysventaAngNetContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });

            //Crear la dependencia de los repositorios creados AddTrasient ya que no necesita compartir dependencias
            //Para poder trabajar de forma generica se debe usar un typeOf min 34.12 pt3
            //Se puede utilizar para cualquier modelo
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Dependencia especifica para las ventas (Lista para compartirse o utilizarse min 36.08)
            //Scoped necesita cambiar cada vez que se apunte a la IVR se apunta a VR min 35.45 pt3
            services.AddScoped<IVentaRepository, VentaRepository>();

            //Usar las dependencias de la clase AutoMapperProfile 49.30 parte 4
            //Agregar dependencia de automapper con todos los mapeos
            //De las claes a DTO y DTO a las clases (modelos)
            services.AddAutoMapper(typeof(AutoMapperProfile));

            //Agregando las dependencias de los métodos / servicios min 01.26.10 parte 5
            //Interfaz, Clase que la implementa
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();

        }
    }
}

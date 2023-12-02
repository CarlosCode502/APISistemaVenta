using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.DAL.DBContext;

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
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositorios.Contratos
{

    //Al ser una clase generica podemos escribir todos los métodos que utilizaremos para trabajar con los modelos
    //Para trabajar de manera generica con todos los modelo (<TModel> where TModel : class) min 02.50 
    /// <summary>
    /// Contiene todos los métodos del contrato de IGenericRepository.
    /// </summary>
    /// <typeparam name="TModel">Recibe un modelo</typeparam>
    public interface IGenericRepository<TModel> where TModel : class
    {
        /// <summary>
        /// Primer valor a respetar para obtener algún modelo
        /// </summary>
        /// <param name="filtro">Si es por id, nombre, etc (valor)</param>
        /// <returns></returns>
        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro);

        /// <summary>
        /// Para poder crear una nueva categoria, usuario, rol, etc. 
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        Task<TModel> Crear(TModel modelo);

        /// <summary>
        /// Para poder editar un modelo.
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        Task<bool> Editar(TModel modelo);

        /// <summary>
        /// Para poder eliminar un modelo.
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        Task<bool> Eliminar(TModel modelo);

        /// <summary>
        /// Realiza una consulta, donde recibe una expresión.
        /// </summary>
        /// <param name="filtro">Es posible que se dese obtener sin pasar el filtro por eso es nulo</param>
        /// <returns>Una consulta del modelo</returns>
        Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro = null);
    }
}

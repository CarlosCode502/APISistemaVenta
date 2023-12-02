using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL.Repositorios.Contratos; //Agregando referencias min 09.23
using SistemaVenta.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositorios
{
    //Pasamos un tmodelo e indicamos que tmodelo es una clase 
    //IGenericRepository exije todos los métodos que contiene min 10.39
    public class GenericRepository<TModelo> : IGenericRepository<TModelo> where TModelo : class
    {
        //Crear campo y pasarle el dbcontext a esta clase min 11.10
        //Dbcontext debe estar en DAL en la carpeta DBContext
        private readonly BdSysventaAngNetContext _dbContext;

        public GenericRepository(BdSysventaAngNetContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Ctrl + . en herencia IGenericRepository e interfaz para que cree los métodos aquí min 11.50 p3
        //Ordenarlos

        public async Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro)
        {
            //Desarrollo del método obtener min 12.30

            //Capturador de errores si existe alguno devolverlo min 12.48
            try
            {
                //El campo modelo usa await porque es async acede al dbcontext asigna este modelo especifico venta
                //Otiene el primer registro o un valor por defecto
                TModelo modelo = await _dbContext.Set<TModelo>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TModelo> Crear(TModelo modelo)
        {
            //Capturador de errores si existe alguno devolverlo min 12.48
            try
            {
                //Accedemos al contexto simplemente le pasamos el modelo
                _dbContext.Set<TModelo>().Add(modelo);
                //Guardamos cambios de manera async
                await _dbContext.SaveChangesAsync();
                //Devolvemos el modelo donde envia el id
                return modelo;
            }
            catch
            {
                //Si no es exitoso devuelve un error
                throw;
            }
        }

        public async Task<bool> Editar(TModelo modelo)
        {
            //Capturador de errores si existe alguno devolverlo min 12.48
            try
            {
                //Accedemos al dbcontext al modelo y lo actualiza
                _dbContext.Set<TModelo>().Update(modelo);
                //Guardamos los cambios
                await _dbContext.SaveChangesAsync();
                //Retorna verdadero si fue exitoso
                return true;
            }
            catch
            {
                //De lo contrario es falso
                throw;
            }
        }

        public async Task<bool> Eliminar(TModelo modelo)
        {
            //Capturador de errores si existe alguno devolverlo min 12.48
            try
            {
                //Accedemos al dbcontext seleccionamos el modelo y eliminamos 
                _dbContext.Set<TModelo>().Remove(modelo);
                //Guardamos los cambios
                await _dbContext.SaveChangesAsync();
                //retornamos si es correcto
                return true;
            }
            catch 
            {
                //Si hay error mostrarlos
                throw;
            }
        }

        public async Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>> filtro = null)
        {
            //Capturador de errores si existe alguno devolverlo min 12.48
            try
            {
                //Lo ejecuta quien sea el que lo llame min 17.15
                //Realizara una consulta que obtiene el resultado del op ternario que si filtro es igual a nulo
                //no se ha especificado ningun filtro en caso de que si exista un filtro se devolvera el modelo con el filtro
                IQueryable<TModelo> queryModelo = filtro == null ? _dbContext.Set<TModelo>() : _dbContext.Set<TModelo>().Where(filtro);

                return queryModelo;
            }
            catch
            {
                throw;
            }
        }
    }
}

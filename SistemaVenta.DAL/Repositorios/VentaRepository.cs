using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregando referencias min 19.18 parte 3
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.Model;

namespace SistemaVenta.DAL.Repositorios
{

    public class VentaRepository : GenericRepository<TblVenta>, IVentaRepository //Contiene 1 metodo ctl + . implement interface
    {
        //Campo donde accedemos al db contex de la bd en dal dbcontext 
        private readonly BdSysventaAngNetContext _dbContext;

        //Generamos el constructor a partir del dbcontext
        public VentaRepository(BdSysventaAngNetContext dbContext) : base(dbContext) //Ya que IVentaRepository recibe _DBContext
        {
            _dbContext = dbContext;
        }


        //Para registrar una venta
        public async Task<TblVenta> Registrar(TblVenta modeloVenta)
        {
            TblVenta ventaGenerada = new TblVenta();

            //Creamos una transacción que va ha ser que cuando exista algun error al momento de hacer un insert
            //Se restablescan los registros guardados que esten relacionados con esta transacción
            //Empezamos a declarar la transacción min 22.50 parte 3
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                //Implementamos la lógica (Si surge un error se restblece todo al principio de hacer el registro)
                try
                {
                    //Restar el stock de cada producto involucrado dentro de la venta
                    //Todo elemento dentro de detalleventa debe ser trabajado
                    //dv es cada item o elemento dentro de la lista productos
                    foreach (TblDetalleVenta dv in modeloVenta.TblDetalleVenta)
                    {
                        //Consulta a la bd de la tabla producto cuando id producto sea igual al producto encontrado obtenerlo
                        TblProducto producto_Encontrado = _dbContext.TblProductos.Where(p => p.IdProducto == dv.IdProducto).First();
                        
                        //Vamos a restar el stock = a menos la cantidad del producto que viene de dv
                        producto_Encontrado.Stock = producto_Encontrado?.Stock - dv.Cantidad;

                        //Accedemos a la bd para actualizar el stock del producto encontrado min 25.20 parte 3
                        _dbContext.TblProductos.Update(producto_Encontrado);
                    }

                    //Va a accedet a la bd y guardar los cambios de manera async
                    await _dbContext.SaveChangesAsync();

                    //Luego de eso se va a crear un nuevo número de documento que devuelva el primero que es 0 min 26.18 parte 3
                    TblNumeroDocumento correlativo = _dbContext.TblNumeroDocumentos.First();

                    //UltimoNumero sera donde guardaremos el ultimo + 1
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    //Accedemos a la propieda gecha registro y obtenemos la fecha actual
                    correlativo.FechaRegistro = DateTime.Now;

                    //Actualizamos la tabla con el numero de docactual
                    _dbContext.TblNumeroDocumentos.Update(correlativo);
                    //Guardamos los cambios min 27.20 part 3
                    await _dbContext.SaveChangesAsync();

                    //Generar el formato del numero de doc venta  0001 min 27.40 parte 3
                    int cantidadDigitos = 4;
                    //Especificar la cantidad de 0 que va a contener se usa un método de repeticion 
                    //Los 0 se va a repetir el valor de cantidadDigitos
                    string ceros = string.Concat(Enumerable.Repeat("0", cantidadDigitos)); //El 0 se rep * cantDig = 0000 

                    //Crear numerodeVenta va a estar conformado por el numero de 0 que han especificado min 28.50 parte 3
                    //string numeroDeVenta = ceros + cantidadDigitos.ToString(); //= 0001
                    //Es correcto se le pasa el correlativo (había escrito lo de arriba y es incorrecto)
                    string numeroDeVenta = ceros + correlativo.UltimoNumero.ToString();

                    //Obtener el numero de venta 4 ceros + 1 dig = 00001 (Debe ser 0001)
                    //Inicia en el la cantidad y le resta 4 luego pasamos la cantidad de digitos que va a obtener
                    numeroDeVenta = numeroDeVenta.Substring(numeroDeVenta.Length - cantidadDigitos, cantidadDigitos);

                    //Actualizar el num de doc de numDeVenta
                    modeloVenta.NumeroDocumento = numeroDeVenta;

                    //agregamos el modelo de manera async
                    await _dbContext.AddAsync(modeloVenta);

                    //Guradamos los cambios 
                    await _dbContext.SaveChangesAsync();
                    //llamar variable y le pasamos el modelo 

                    //A venta generada le pasamos el modelo
                    ventaGenerada = modeloVenta;

                    //Para guardar esta transacción temporalmente
                    transaction.Commit();
                }
                catch
                {
                    //Podrá restablecer todo en caso de que si ocurrio algún error dentro del try
                    transaction.Rollback();
                    //Devuelva el error
                    throw;
                }

                //Devolver la venta generada min 32.50 p3
                return ventaGenerada;
            }
        }
    }
}
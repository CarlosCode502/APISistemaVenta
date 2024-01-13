using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregamos las referencias min 29.29 parte 5
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.Model;


namespace SistemaVenta.BLL.Servicios
{
    public class ProductoService : IProductoService //Agregar herencia de la interfaz para los métodos min 29.44 part 5
    {
        //Creando la clase génerica que contiene una definicion para la tabla producto min 29.55 parte 5
        private readonly IGenericRepository<TblProducto> _productoRepositorio;

        //Contiene la definicion para trabajar con Mapper min 30.05 parte 5
        private readonly IMapper _mapper;

        //#-- Def para detalleventa
        private readonly IGenericRepository<TblDetalleVenta> _detalleVentaRepositorio;

        //Se genero en acciones rápidas (Teniendo seleccionado lo de arriba) min 30.15 part 5
        public ProductoService(IGenericRepository<TblProducto> productoRepositorio, IMapper mapper, IGenericRepository<TblDetalleVenta> detalleVentaRepositorio)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
            _detalleVentaRepositorio = detalleVentaRepositorio;
        }


        public async Task<List<Producto_DTO>> ListaProductos()
        {
            try
            {
                //Obtenemos una consulta de los productos min 31.05 parte 5
                var queryProducto = await _productoRepositorio.Consultar();

                //Listado de productos (con un filtro al que tenemos acceso a las prop de categorias) min 31.10 parte 5
                var listaProductos = queryProducto.Include(cat => cat.IdCategoriaNavigation).ToList();

                //#-- Valida si el stock del producto es <= 0
                //for (int i = 0; i < listaProductos.Count; i++)
                //{
                //    var element = listaProductos[i];

                //    if(element.Stock <= 0)
                //    {
                //        element.EsActivo = false;
                //    }
                //    else
                //    {
                //        element.EsActivo = true;
                //    }

                //    listaProductos[i] = element;
                //}


                //Retorna un mapeo del listado especificando (destino, origen) min 31.15 parte 5
                return _mapper.Map<List<Producto_DTO>>(listaProductos.ToList());
            }
            catch
            {
                //Devuelve el error
                throw;
            }
        }

        public async Task<Producto_DTO> Crear(Producto_DTO modeloC)
        {
            try
            {
                //#-- Si el stock es mayor o igual a 1 el estado sera activo sino inactivo 12/01/24 16.25
                if (modeloC.Stock <= 0)
                {
                    modeloC.EsActivo = 0;
                }
                else
                {
                    modeloC.EsActivo = 1;
                }


                //min 32.45 parte 5

                //Recibe un producto no un modelo para eso se debe mapear como modelo para obtener un producto
                //Se especifica modelo y producto min 33.08 parte 5
                var productoCreado = await _productoRepositorio.Crear(_mapper.Map<TblProducto>(modeloC));

                //Valida si el del producto es 0 quiere decir que no se pudo crear min 33.25 parte 5
                //Retornar tarea cancelada
                if (productoCreado.IdProducto == 0) { throw new TaskCanceledException("No se pudo crear el producto"); }




                //Si se creó devolvemos el producto min 33.59 parte 5
                return _mapper.Map<Producto_DTO>(productoCreado);
            }
            catch
            {
                //Devuelve el error
                throw;
            }
        }

        public async Task<bool> Editar(Producto_DTO modeloE)
        {
            try
            {
                //Obtener el producto en forma de modelo (se esta obteniendo como dto por eso) min 34.35 parte 5
                var productoModelo = _mapper.Map<TblProducto>(modeloE);

                //Obtiene una busqueda para saber si ese producto existe o no en la bd (recibe un filtro) min 35.25 parte 5
                var productoEncontrado = await _productoRepositorio.Obtener(u => u.IdProducto == productoModelo.IdProducto);
                
                //Valida si el producto es igual a nulo y muestra msj de error min 35.49 parte 5
                if(productoEncontrado == null) { throw new TaskCanceledException("El producto no existe."); }

                //Si el producto existe actualizamos cada una de las propiedades min 36.07 parte 5
                productoEncontrado.NombreProducto = productoModelo.NombreProducto;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                //productoEncontrado.EsActivo = productoModelo.EsActivo;

                //#-- Si el stock es mayor o igual a 1 el estado sera activo sino inactivo 12/01/24 16.25
                if (productoEncontrado.Stock >= 1)
                {
                    productoEncontrado.EsActivo = true;
                }
                else
                {
                    productoEncontrado.EsActivo = false;
                }
                

                //Ejecutamos el método editar si todo es correcto devuelve true min 37.45 parte 5
                bool respuesta = await _productoRepositorio.Editar(productoEncontrado);

                //SI es falso devuelve un error min 37.55 parte 5
                if(!respuesta) { throw new TaskCanceledException("No se pudo editar el producto"); }

                //Devolvemos una respuesta si es exitoso min 38.05 parte 5
                return respuesta;
            }
            catch
            {
                //Devuelve el error
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                //Obtiene el producto que cumpla con un filtro de id min 38.26 parte 5
                var productoEncontrado = await _productoRepositorio.Obtener(p => p.IdProducto == id);
                //var productoDetalle = await _detalleVentaRepositorio.Obtener(p => p.IdProducto == id)

                //Valida si existe el producto encontrado (si no se interrumpe) min 38.45 parte 5
                if (productoEncontrado == null) { throw new TaskCanceledException("El producto no existe."); }

                //Si el producto es encontrado se ejecuta el método eliminar min 39.19 parte 5
                bool respuesta = await _productoRepositorio.Eliminar(productoEncontrado);

                //Si no se encuentra el producto se muestra un msj de error min 39.38 parte 5
                if(!respuesta) { throw new TaskCanceledException("No se pudo eliminar el producto."); }

                //Si se encuentra retornamos la respuesta min 39.55 parte 5
                return respuesta;
            }
            catch /*(Exception ex) { }*/
            {
                //Devuelve el error
                throw;
                //await Console.Out.WriteLineAsync(Exception.Refe);
            }
        }
    }
}

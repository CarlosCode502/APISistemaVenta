using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregamos las referencias min 25.32 parte 5
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class CategoriaService : ICategoriaService //Agregar herencia de la interfaz para los métodos min 26.10 part 5
    {
        //Creando la clase génerica que contiene una definicion para la tabla categorias min 25.49 parte 5
        private readonly IGenericRepository<TblCategoria> _categoriaRepositorio;

        //Contiene la definicion para trabajar con Mapper min 25.55 parte 5
        private readonly IMapper _mapper;

        //Se genero en acciones rápidas (Teniendo seleccionado lo de arriba) min 26.08 part 5
        public CategoriaService(IGenericRepository<TblCategoria> categoriaRepositorio, IMapper mapper)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _mapper = mapper;
        }

        //Método agregado luego de implementar / agregar la interfaz min 26.20 part 5
        public async Task<List<Categoria_DTO>> Lista()
        {
            try
            {
                //Obtenemos el listado de categorias (desde IgenericRepository) min 26.45 parte 5
                var listaCategorias = await _categoriaRepositorio.Consultar();

                //Retornamos un listado haciendo el mapeo (destino, origen) min 26.58 parte 5
                return _mapper.Map<List<Categoria_DTO>>(listaCategorias.ToList());
            }
            catch
            {
                //Devuelve el error 
                throw;
            }
        }
    }
}

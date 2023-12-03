using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregar referencias min 23.22 part 4
using AutoMapper;
using SistemaVenta.DTO; //Clases recientes
using SistemaVenta.Model; //Modelos de la BD

namespace SistemaVenta.Utility
{
    //Contiene la definición para convertir un modelo a una función a esto se le conoce como mapeo min 23.10 part 4
    //Esto de Mappear es de gran utilidad (Convertir de una clase a otra) min 53.15 part4
    public class AutoMapperProfile : Profile //Profile le pertenece a AutoMapper
    {
        //ctor + Enter para crear un constructor
        public AutoMapperProfile()
        {
            //Mapeos (Todos los campos)
            //(origen, destino) min 25.43 part 4
            //Solo va a ser referencia con las propiedades en el destino (es importante que tengan el mismo nombre)
            //Para poder invertir o creal el map al contrario min 26.20 p4

            #region Rol
            CreateMap<TblRol, Rol_DTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<TblMenu, Menu_DTO>().ReverseMap();
            #endregion Menu

            #region Usuario
            //Este es un caso especial ya que exiten campos en Usuario_DTO que no están en TblUsuario min 27.55 pt4
            //O campos que se modificaron
            CreateMap<TblUsuario, Usuario_DTO>() //.ReverseMap();
            //Tranajando con el mapeo diferente (ForMember permite espec la conversion hacia el destino)
            //Desde donde vamos a obtener la info y si es necesario hacer algúna conversión
            .ForMember(destino => destino.RolDescripcion, //Campo destino (nuevo)
            opts => opts.MapFrom(origen => origen.IdRolNavigation.Nombre) //Campo de origen (eviar valor a nuevo)
            )
            .ForMember(destino => destino.EsActivo, //Actio en el origen es bool y en el destino es int (necesita map 29.50)
            opts => opts.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
            ); //Si origen.EsActivo es = true entonces devolver un 1 en caso de ser false devolver 0 min 30.35 pt4


            //Al hacer la conversion de usuario va a convertir a roldescipcion
            //Obtener el rolNombre y enviarlos a roldescription
            CreateMap<TblUsuario, Sesion_DTO>() //.ReverseMap();
            .ForMember(destino => destino.RolDescripcion, //Campo destino (nuevo)
            opts => opts.MapFrom(origen => origen.IdRolNavigation.Nombre) //Campo de origen (eviar valor a nuevo)
            );


            //Convertir Usuario_DTO a TblUsuario (Contrario)
            CreateMap<Usuario_DTO, TblUsuario>()
                .ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore() //Ignorar el campo IdRolNavigation (No existe en el origen)
                )
            //Obtener un bool y retornar un entero
            .ForMember(destino => destino.EsActivo, //Activo en el origen es bool y en el destino es int (necesita map 29.50)
            opts => opts.MapFrom(origen => origen.EsActivo == 1 ? true : false)
            ); //Si origen.EsActivo es = true entonces devolver un 1 en caso de ser false devolver 0 min 30.35 pt4
            #endregion Usuario

            #region Categoria
            //Mapeo de categoria 33.42
            CreateMap<TblCategoria, Categoria_DTO>().ReverseMap();
            #endregion Categoria

            #region Productos
            //Mapeo de Productos 34.20
            CreateMap<TblProducto, Producto_DTO>()
                //Obtiene el campo Descripcion del destino (campo creado) y le asigna el valor del campo origen Nombre
                .ForMember(destino => destino.DescripcionCategoria,
                opts => opts.MapFrom(origen => origen.IdCategoriaNavigation.NombreCategoria)
                )
                //Obtiene el campo Precio del destino (es string)
                //Y le asigna el del origen (es decimal) solo hace un convert a string aplicando la cultura de moneda 
                //Precio es decimal se convierte a string a través de un formato de texto según la cultura 
                .ForMember(destino => destino.Precio,
                opts => opts.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-GT")))
                )
                //Obtiene esActivo desde el destino (int)
                //Obtiene esActivo desde el origen y si es bool true envia 1 al destino caso contrario 0
                .ForMember(destino => destino.EsActivo,
                opts => opts.MapFrom(origen => origen.EsActivo == true ? 1 : 0));


            //(Caso contrario a lo de arriba) Producto_DTO a TblProducto min 37.35 part4
            //          (Origen, Destino)
            CreateMap<Producto_DTO, TblProducto>()
                //El campo IdCategoria se ignora porque el origen no contiene una defincion para este
                .ForMember(destino => destino.IdCategoriaNavigation,
                opts => opts.Ignore()
                )
                //Recibe un precio de tipo entero pero necesita uno de tipo decimal así que se hace la conversión
                //Con el mismo culture info (se quita el value)
                .ForMember(destino => destino.Precio,
                opts => opts.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-GT")))
                )
                //Obtiene un bool pero necesita un int
                .ForMember(destino => destino.EsActivo,
                opts => opts.MapFrom(origen => origen.EsActivo == 1 ? true : false));
            #endregion Productos

            #region Venta
            //Mapeo de venta min 39.25 pt4
            //Obtiene los campos de (origen hacia destino)
            CreateMap<TblVenta, Venta_DTO>()
                //Recibe un string pero se le envía del origen un decimal (asi que se hace una conversion)
                .ForMember(destino => destino.TotalTexto,
                opts => opts.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-GT")))
                )
                //Recibe un string pero se le envía del origen un datetime (asi que se hace una conversion)
                .ForMember(destino => destino.FechaRegistro,
                opts => opts.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                );

            //Lo contrario de lo anterior min 40.43 part 4
            CreateMap<Venta_DTO, TblVenta>()
                //Recibe un Decimal pero se le envía un string (necesita una conversión)
                .ForMember(destino => destino.Total,
                opts => opts.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-GT")))
                //)
                ////Este no se aplico (VERIFICAR)
                //.ForMember(destino => destino.FechaRegistro,
                //opts => opts.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                );
            #endregion Venta

            #region DetalleVenta
            //Mapeo detalleVenta min 41.55 part 4
            CreateMap<TblDetalleVenta, DetalleVenta_DTO>()
                //Recibe un string con el nombre del producto
                .ForMember(destino => destino.DescripcionProducto,
                opts => opts.MapFrom(origen => origen.IdProductoNavigation.NombreProducto)
                )
                //Recibe un decimal pero necesita un string (necesita conversion)
                .ForMember(destino => destino.PrecioTexto,
                opts => opts.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-GT")))
                )
                //Recibe un decimal pero necesita un string (necesita conversion)
                .ForMember(destino => destino.TotalTexto,
                opts => opts.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-GT"))));

            //Mapeo Inverso min 43.05 par4 (Solo para campos con conversion)
            CreateMap<DetalleVenta_DTO, TblDetalleVenta>()
                //Recibe un string pero necesita un decimal (se hace una conversion)
                .ForMember(destino => destino.Precio,
                opts => opts.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-GT")))
                )
                //Recibe un string pero necesita un decimal (se hace una conversion)
                .ForMember(destino => destino.Total,
                opts => opts.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-GT")))
                );
            #endregion DetalleVenta

            #region Reporte
            //Mapeo de reporte min 44.20
            //Va a ser convertido a reporteDTO
            CreateMap<TblDetalleVenta, Reporte_DTO>()
                //Recibe un string pero se envia un datetime (necesita conversion)
                .ForMember(destino => destino.FechaRegistro,
                opts => opts.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                //Recibe el numero de NumDocumento
                .ForMember(destino => destino.NumeroDocumento,
                opts => opts.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento))
                //Recibe el Tipo de pago
                .ForMember(destino => destino.TipoPago,
                opts => opts.MapFrom(origen => origen.IdVentaNavigation.TipoPago))
                //Recibe un decimal pero necesita un string (se hace una conversion)
                .ForMember(destino => destino.TotalVenta,
                opts => opts.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-GT")))
                )
                //Recibe el nombre del producto
                .ForMember(destino => destino.Producto,
                opts => opts.MapFrom(origen => origen.IdProductoNavigation.NombreProducto)
                )
                //Recibe un decimal pero necesita un string
                .ForMember(destino => destino.Precio,
                opts => opts.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-GT")))
                )
                //Recibe un decimal pero necesita un string
                .ForMember(destino => destino.Total,
                opts => opts.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-GT")))
                );
            #endregion Reporte
        }
    }
}
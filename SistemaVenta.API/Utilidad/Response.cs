namespace SistemaVenta.API.Utilidad
{
    //Creado en min 0.045 parte 6

    //Servira como respuesta a todas las solicitudes de nuestras apis min 00.55 parte 6
    public class Response<T> //<T> Para volverla una clase Generica
    {
        //Retornar op exitosa min 01.12 parte 6
        public bool Status { get; set; }

        //Devolver el objeto que estamos recibiendo min 01.25 parte 6
        public T? Value { get; set; }

        //Mensaje min 01.38 parte 6
        public string? Msg { get; set; }
    }
}

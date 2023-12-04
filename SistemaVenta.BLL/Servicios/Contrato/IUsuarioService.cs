using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregar referencia min 07.08 part 5
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IUsuarioService
    {
        //Método listar usuarios 07.45 part5
        Task<List<Usuario_DTO>> ListaUsuarios();

        //Método Validar credenciales del usuario recibe 2 parametros min 08.15 part 5
        Task<Sesion_DTO> ValidarCredenciales(string correo, string clave);

        //Método para obtener usuarios min 08.30 parte 5
        Task<Usuario_DTO> Crear(Usuario_DTO modeloC);

        //Método para poder editar recibe un usuario DTO con campo modelo min 08.55 part 5
        Task<bool> Editar(Usuario_DTO modeloE);

        //Método para poder editar recibe el id del usuario a eliminar min 09.15 part 5
        Task<bool> Eliminar(int id);
    }
}

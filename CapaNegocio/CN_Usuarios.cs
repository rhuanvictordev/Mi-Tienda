using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public static class CN_Usuarios
    {
        private static CD_Usuarios objCapaDato = new CD_Usuarios();

        public static List<Usuario> Listar() 
        {
            return objCapaDato.Listar();
        }
    }
}

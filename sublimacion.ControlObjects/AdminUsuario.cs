using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using sublimacion.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;

namespace sublimacion.ControlObjects
{
    public class AdminUsuario
    {

        protected static AdminUsuario Instance = null;
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = new AdminUsuario();
            }
        }

        public static AdminUsuario Instancia
        {
            get
            {
                CreateInstance();
                return Instance;
            }
        }

        public Usuario verificarUsuario(string usuario, string password)
        {

          return  UsuarioDAO.Instancia.obtenerUsuario(usuario, password);
            

        }

    }
}

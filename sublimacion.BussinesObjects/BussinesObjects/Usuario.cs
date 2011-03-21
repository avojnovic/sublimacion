using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects
{
    public class Usuario
    {
        private long _id;

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _apellido;

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _usuario;

        public string User
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _telefono;

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }



        private bool _borrado;

        public bool Borrado
        {
            get { return _borrado; }
            set { _borrado = value; }
        }

        private PerfilesEnum _perfil;

        public PerfilesEnum Perfil
        {
            get { return _perfil; }
            set { _perfil = value; }
        }



        public enum PerfilesEnum
        {
            Diseniador = 1,
            Operario = 2,
            Vendedor = 3,
            JefeSuperior = 4,
            JefeProduccion = 5,
            Administrador = 6
        }



    }
}

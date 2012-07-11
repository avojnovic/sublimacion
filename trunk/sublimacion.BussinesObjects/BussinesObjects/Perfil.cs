using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
    public class Perfil
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
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private bool _borrado;

        public bool Borrado
        {
            get { return _borrado; }
            set { _borrado = value; }
        }


    }
}

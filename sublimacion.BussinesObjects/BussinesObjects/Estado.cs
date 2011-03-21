using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
   public class Estado
    {

        private long _id;

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }


        public override string ToString()
        {
            return Descripcion;
        }


        public enum EstadoEnum
        {
            Nuevo = 1,
            Modificar = 2,
            Eliminar = 3,
            Consultar = 4,
            NoGuardar = 5

        }


    }
}

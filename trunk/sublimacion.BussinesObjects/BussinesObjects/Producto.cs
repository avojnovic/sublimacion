using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
   public class Producto
    {
        private long _idproducto;

        public long Idproducto
        {
            get { return _idproducto; }
            set { _idproducto = value; }
        }
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private float _precio;

        public float Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }
        private bool _borrado;

        public bool Borrado
        {
            get { return _borrado; }
            set { _borrado = value; }
        }
        private float _costo;

        public float Costo
        {
            get { return _costo; }
            set { _costo = value; }
        }
        private float _tiempo;

        public float Tiempo
        {
            get { return _tiempo; }
            set { _tiempo = value; }
        }


        public override string ToString()
        {
            return Nombre;
        }

    }
}

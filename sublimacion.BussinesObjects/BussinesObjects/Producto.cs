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
        private decimal _precio;

        public decimal Precio
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
        private decimal _costo;

        public decimal Costo
        {
            get { return _costo; }
            set { _costo = value; }
        }
        private decimal _tiempo;

        public decimal Tiempo
        {
            get { return _tiempo; }
            set { _tiempo = value; }
        }

        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public override string ToString()
        {
            return Nombre;
        }

        public string ParaPedido
        {
            get { return Nombre +" - "+Cantidad.ToString(); }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
   public class Descuento
    {
        private Producto _producto;

        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        public string productoNombre
        {
            get { return _producto.Nombre; }
            
        }

        public long productoId
        {
            get { return _producto.Idproducto; }

        }

        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        private decimal _descuento;

        public decimal Descuento1
        {
            get { return _descuento; }
            set { _descuento = value; }
        }

        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }


    }
}

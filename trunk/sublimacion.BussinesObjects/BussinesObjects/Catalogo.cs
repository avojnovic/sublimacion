using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
   public  class Catalogo
    {
        private long _idCatalogo;

        public long IdCatalogo
        {
            get { return _idCatalogo; }
            set { _idCatalogo = value; }
        }
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public string FechaVer
        {
            get { return Fecha.ToShortDateString(); }

        }

        private Producto _producto;

        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        public string NombreProducto
        {
            get { return _producto.Nombre; }
        }

        public override string ToString()
        {
            return  Nombre;
        }

        private Dictionary<long, Plantilla> _plantilla;

        public Dictionary<long, Plantilla> Plantilla
        {
            get { return _plantilla; }
            set { _plantilla = value; }
        }

        private bool _borrado;

        public bool Borrado
        {
            get { return _borrado; }
            set { _borrado = value; }
        }


    }
}

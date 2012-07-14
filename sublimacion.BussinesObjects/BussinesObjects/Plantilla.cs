using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
    public class Plantilla
    {
        private long _idPlantilla;

        public long IdPlantilla
        {
            get { return _idPlantilla; }
            set { _idPlantilla = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private decimal _medida_ancho;

        public decimal Medida_ancho
        {
            get { return _medida_ancho; }
            set { _medida_ancho = value; }
        }

        private decimal _medida_largo;

        public decimal Medida_largo
        {
            get { return _medida_largo; }
            set { _medida_largo = value; }
        }

        private bool _pertenece;

        public bool Pertenece
        {
            get { return _pertenece; }
            set { _pertenece = value; }
        }


        private bool _borrado;

        public bool Borrado
        {
            get { return _borrado; }
            set { _borrado = value; }
        }


        public override string ToString()
        {
               return Nombre;
        }
 
    }
}

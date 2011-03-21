using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects
{
    public class Insumo
    {

        private long _idinsumo;

        public long Idinsumo
        {
            get { return _idinsumo; }
            set { _idinsumo = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _nombreFab;

        public string NombreFab
        {
            get { return _nombreFab; }
            set { _nombreFab = value; }
        }

        private float _costo;

        public float Costo
        {
            get { return _costo; }
            set { _costo = value; }
        }

        private int _stock;

        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        private DateTime _fechaAct;

        public DateTime FechaAct
        {
            get { return _fechaAct; }
            set { _fechaAct = value; }
        }

        private bool _borrado;

        public bool Borrado
        {
            get { return _borrado; }
            set { _borrado = value; }
        }


        public string IdInsumoStr
        {
            get { return _idinsumo.ToString(); }
        }


        public string FechaActStr
        {
            get { return _fechaAct.ToShortDateString();}
        }

        public string CostoStr
        {
            get { return _costo.ToString(); }
        }

        public string StockStr
        {
            get { return _stock.ToString(); }
        }
    }
}

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

        private decimal _tiempo;

        public decimal Tiempo
        {
            get { return _tiempo; }
            set { _tiempo = value; }
        }

               
        //Agregar propiedad que genere automaticamente Costo
        public decimal CostoAutomatico
        {

            get
            {
                decimal costo = 0;
                foreach (Insumo i in _insumos.Values)
                {
                    costo = costo + (i.Costo * i.Cantidad);

                }
                return costo;
            }
        }

        //Agregar propiedad que genere automaticamente Stock
        public string StockAutomatico
        {
            get
            {
                List<decimal> _listaStock = new List<decimal>();
                foreach (Insumo i in _insumos.Values)
                {
                    _listaStock.Add(i.Stock / i.Cantidad);
                }

                _listaStock.Sort();

                if (_listaStock.Count() > 0)
                    return _listaStock[0].ToString();
                else
                    return "0";
            }
        }

        private Dictionary<long, Insumo> _insumos;

        public Dictionary<long, Insumo> Insumos
        {
            get { return _insumos; }
            set { _insumos = value; }
        }

    }
}

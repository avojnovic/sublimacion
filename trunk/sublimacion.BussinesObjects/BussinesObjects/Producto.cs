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
            get { return Nombre + " - " + Cantidad.ToString(); }
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
            get {
                /*string desc="";
                int necesito = 0;
                int minimo = 0;
                foreach (Insumo i in _insumos.Values)
                {
                    necesito = necesito + (i.Stock - i.Cantidad);

                    if (necesito <= 0)
                    {
                        desc = "SIN STOCK";
                        break;
                    }
                    else
                    {
                        if (necesito < minimo)
                            minimo = necesito;
                    }
                }*/
                return " - ";
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

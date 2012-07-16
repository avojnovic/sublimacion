using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
    public class LineaPedido
    {

        private Producto _producto;

        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        public long Idproducto
        {
            get { return Producto.Idproducto; }
        }

        public string Nombre
        {
            get { return Producto.Nombre; }
        }

        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        private Plantilla _plantilla;

        public Plantilla Plantilla
        {
            get { return _plantilla; }
            set { _plantilla = value; }
        }

        public string PlantillaNombre
        {
            get
            {
                if (Plantilla != null)
                    return Plantilla.Nombre;
                else
                    return "";
            }
        }
        private Catalogo _catalogo;

        public Catalogo Catalogo
        {
            get { return _catalogo; }
            set { _catalogo = value; }
        }

        public string CatalogoNombre
        {
            get
            {

                if (Catalogo != null)
                    return Catalogo.Nombre;
                else
                    return "";


            }
        }

        private string _archivoCliente;

        public string ArchivoCliente
        {
            get { return _archivoCliente; }
            set { _archivoCliente = value; }
        }

        public string ArchivoClienteNombreMostrable
        {
            get {
                if (_archivoCliente != null && _archivoCliente != "")
                    return _archivoCliente.Remove(0,18); 
                else
                    return "";
                }

        }

        private string _archivoDisenio;

        public string ArchivoDisenio
        {
            get { return _archivoDisenio; }
            set { _archivoDisenio = value; }
        }

        public string ArchivoDisenioNombreMostrable
        {
            get {
                if (_archivoDisenio != null && _archivoDisenio != "")
                    return _archivoDisenio.Remove(0,18); 
                else
                    return "";
                }

        }
    }
}

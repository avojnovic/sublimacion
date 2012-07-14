using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
    public class Pedido
    {

        private long _idPedido;

        public long IdPedido
        {
            get { return _idPedido; }
            set { _idPedido = value; }
        }
        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        private bool _borrado;

        public bool Borrado
        {
            get { return _borrado; }
            set { _borrado = value; }
        }
        private string _comentario;

        public string Comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }
        private int _prioridad;

        public int Prioridad
        {
            get { return _prioridad; }
            set { _prioridad = value; }
        }
        private string _ubicacion;

        public string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }
        private Usuario _usuario;

        public Usuario Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        private OrdenDeTrabajo _ordenDeTrabajo;

        public OrdenDeTrabajo OrdenDeTrabajo
        {
            get { return _ordenDeTrabajo; }
            set { _ordenDeTrabajo = value; }
        }
        private PlanDeProduccion _planDeProduccion;

        public PlanDeProduccion PlanDeProduccion
        {
            get { return _planDeProduccion; }
            set { _planDeProduccion = value; }
        }
        private Cliente _cliente;

        public Cliente Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        public string UserNombre
        {
            get { return _usuario.Nombre+" "+_usuario.Apellido; }
        }

        public string ClienteNombre
        {
            get { return _cliente.Nombre + " " + _cliente.Apellido; }
        }


        private Dictionary<long,EstadosPedido> _estadosPedido;

        public Dictionary<long, EstadosPedido> EstadosPedido
        {
            get { return _estadosPedido; }
            set { _estadosPedido = value; }
        }


        private List<Producto> _lineaPedido;

        public List<Producto> LineaPedido
        {
            get { return _lineaPedido; }
            set { _lineaPedido = value; }
        }

        public string Estado
        {
            get {

                //DateTime? t = DateTime.MinValue;
                Estado est = new Estado();
                //foreach (EstadosPedido e in EstadosPedido.Values.ToList())
                //{
                //    if (e.Fecha_inicio > t)
                //    {
                //        t = e.Fecha_inicio;
                //        est = e.Estado;
                //    }
                //}


                foreach (EstadosPedido e in EstadosPedido.Values.ToList())
                {
                    if (e.Fecha_fin==null)
                    {
                        return e.Estado.Descripcion;
                    }
                }

                return "";



            }
        }

        public string CostoTotalTiempo
        {
            get
            {
                decimal tiempo = 0;
                if (LineaPedido != null && LineaPedido.Count > 0)
                {
                    foreach (Producto p in LineaPedido)
                    {
                        tiempo += p.Tiempo * p.Cantidad;
                    }
                }

                return tiempo.ToString();
            }
        }

        public string CostoTotal
        {
            get
            {
                decimal costo = 0;
                if (LineaPedido != null && LineaPedido.Count > 0)
                {
                    foreach (Producto p in LineaPedido)
                    {
                        costo += p.Precio * p.Cantidad;
                    }
                }

                return costo.ToString();
            }
        }


    }


}

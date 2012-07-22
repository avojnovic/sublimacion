using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
    public class Estimacion
    {
        private long _idPlan;

        public long IdPlan
        {
            get { return _idPlan; }
            set { _idPlan = value; }
        }
        private DateTime _fechaInicioPlan;

        public DateTime FechaInicioPlan
        {
            get { return _fechaInicioPlan; }
            set { _fechaInicioPlan = value; }
        }
        private DateTime _fechaFinPlan;

        public DateTime FechaFinPlan
        {
            get { return _fechaFinPlan; }
            set { _fechaFinPlan = value; }
        }

        private long _idOrden;

        public long IdOrden
        {
            get { return _idOrden; }
            set { _idOrden = value; }
        }
        private DateTime _fechaInicioOrden;

        public DateTime FechaInicioOrden
        {
            get { return _fechaInicioOrden; }
            set { _fechaInicioOrden = value; }
        }
        private DateTime _fechaFinOrden;

        public DateTime FechaFinOrden
        {
            get { return _fechaFinOrden; }
            set { _fechaFinOrden = value; }
        }

        private string _cantPedidos;

        public string CantPedidos
        {
            get { return _cantPedidos; }
            set { _cantPedidos = value; }
        }

        private double _estimado;

        public double Estimado
        {
            get { return _estimado; }
            set { _estimado = value; }
        }

        public string EstimadoMostrar
        {
            get { return _estimado.ToString() + "%"; }
        }
    }
}

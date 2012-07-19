using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
    public class PlanDeProduccion
    {

        private long _idPlan;

        public long IdPlan
        {
            get { return _idPlan; }
            set { _idPlan = value; }
        }
        private DateTime _fecha_inicio;

        public DateTime Fecha_inicio
        {
            get { return _fecha_inicio; }
            set { _fecha_inicio = value; }
        }
        private DateTime _fecha_fin;

        public DateTime Fecha_fin
        {
            get { return _fecha_fin; }
            set { _fecha_fin = value; }
        }
        private bool _borrado;

        public bool Borrado
        {
            get { return _borrado; }
            set { _borrado = value; }
        }


        public string Id
        {
            get { return _idPlan.ToString(); }
        }

        public string Fecha_inicio_str
        {
            get
            {
                if (_fecha_inicio != DateTime.MinValue)
                    return _fecha_inicio.ToString("dd/MM/yyyy") + " " + _fecha_inicio.ToString("HH:mm");
                else
                    return "";

            }
        }

        public string Fecha_fin_str
        {
            get
            {
                if (_fecha_fin != DateTime.MinValue)
                    return _fecha_fin.ToString("dd/MM/yyyy") + " " + _fecha_fin.ToString("HH:mm");
                else
                    return "";
            }
        }
    }
}

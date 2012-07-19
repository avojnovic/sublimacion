using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
    public class OrdenDeTrabajo
    {

        private long _idorden;

        public long Idorden
        {
            get { return _idorden; }
            set { _idorden = value; }
        }
        private DateTime? _fecha_comienzo;

        public DateTime? Fecha_comienzo
        {
            get { return _fecha_comienzo; }
            set { _fecha_comienzo = value; }
        }
        private DateTime? _fecha_finalizacion;

        public DateTime? Fecha_finalizacion
        {
            get { return _fecha_finalizacion; }
            set { _fecha_finalizacion = value; }
        }
        private decimal _tiempo_estimado;

        public decimal Tiempo_estimado
        {
            get { return _tiempo_estimado; }
            set { _tiempo_estimado = value; }
        }


        public string Fecha_inicio_str
        {
            get {

                    if (_fecha_comienzo != null)
                    {
                        DateTime t = (DateTime)_fecha_comienzo;
                        return t.ToString("dd/MM/yyyy") + " " + t.ToString("HH:mm"); 
                    }
                    else
                    {
                        return "";
                    }
               
            
                }
        }

        public string Fecha_fin_str
        {
            get
            {

                if (_fecha_finalizacion != null)
                {
                    DateTime t = (DateTime)_fecha_finalizacion;
                    return t.ToString("dd/MM/yyyy") + " " + t.ToString("HH:mm");
                }
                else
                {
                    return "";
                }


            }
        }
    }
}

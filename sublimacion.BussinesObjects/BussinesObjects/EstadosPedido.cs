﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
    public class EstadosPedido
    {
        public enum EstadosPedidoEnum
        {

            DisenioPendiente =1,
            AceptacionDisenioPendiente=2,
            DisenioAceptado=3,
            Producción=4,
            Terminado=5,
            Entregado=6,
            FaltanteStock=7

        }
    

        private DateTime? _fecha_inicio;

        public DateTime? Fecha_inicio
        {
            get { return _fecha_inicio; }
            set { _fecha_inicio = value; }
        }
        private DateTime? _fecha_fin;

        public DateTime? Fecha_fin
        {
            get { return _fecha_fin; }
            set { _fecha_fin = value; }
        }
        private Estado _estado;

        public Estado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }



    }
}

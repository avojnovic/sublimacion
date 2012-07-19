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

            DisenioPendiente =2,
            AceptacionDisenioPendiente=3,
            DisenioAceptado=4,
            FaltanteStock=5,
            Producción=6,
            Terminado=7,
            Entregado=8

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

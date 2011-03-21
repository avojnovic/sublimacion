using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sublimacion.BussinesObjects.BussinesObjects
{
    public class Cliente
    {

        private long _idCliente;

        public long IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }
        private string _nombre;


        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }private string _apellido;

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }
        private long _dni;

        public long Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }
        private string _direccion;

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
        private string _telefono;

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        private string _mail;

        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
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



        public override string ToString()
        {
            return Nombre+" " + Apellido;
        }
    }
}

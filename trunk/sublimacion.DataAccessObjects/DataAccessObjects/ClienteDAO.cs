using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using sublimacion.BussinesObjects.BussinesObjects;
using Npgsql;

namespace sublimacion.DataAccessObjects.DataAccessObjects
{
   public class ClienteDAO
    {
       #region Singleton
        private static ClienteDAO Instance = null;
        private ClienteDAO() 
        {
            
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = new ClienteDAO();
            }
        }

        public static ClienteDAO Instancia
        {
            get
            {
                CreateInstance();
                return Instance;
            }
        }
        #endregion


        public Dictionary<long,Cliente> obtenerClienteTodos()
        {

            string sql = "";
            sql = @"SELECT idcliente, nombre, apellido, dni, direccion, telefono, mail, fecha, borrado
                FROM cliente
                where borrado=false";

           
            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Cliente> dicCliente = new Dictionary<long, Cliente>();
           
            while (dr.Read())
            {
                Cliente u;
                u = getClienteDelDataReader(dr);


                if (!dicCliente.ContainsKey(u.IdCliente))
                    dicCliente.Add(u.IdCliente,u);

            }

            return dicCliente;

        }


        public Cliente obtenerClientePorId(string id)
        {

            string sql = "";
            sql = @"SELECT idcliente, nombre, apellido, dni, direccion, telefono, mail, fecha, borrado
                FROM cliente
                where borrado=false and idcliente='{0}'";
           
            sql = string.Format(sql, id);
            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Cliente> dicCliente = new Dictionary<long, Cliente>();
            Cliente u = null;
            while (dr.Read())
            {
                u = getClienteDelDataReader(dr);

            }

            return u;

        }

        private Cliente getClienteDelDataReader(NpgsqlDataReader dr)
        {

            Cliente c = new Cliente();

          
            if (!dr.IsDBNull(dr.GetOrdinal("idcliente")))
                c.IdCliente = long.Parse(dr["idcliente"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                c.Nombre = dr.GetString(dr.GetOrdinal("nombre"));

            if (!dr.IsDBNull(dr.GetOrdinal("apellido")))
                c.Apellido = dr.GetString(dr.GetOrdinal("apellido"));

            if (!dr.IsDBNull(dr.GetOrdinal("dni")))
                c.Dni = long.Parse(dr["dni"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("direccion")))
                c.Direccion = dr.GetString(dr.GetOrdinal("direccion"));

            if (!dr.IsDBNull(dr.GetOrdinal("telefono")))
                c.Telefono = dr.GetString(dr.GetOrdinal("telefono"));

            if (!dr.IsDBNull(dr.GetOrdinal("mail")))
               c.Mail = dr.GetString(dr.GetOrdinal("mail"));

              if (!dr.IsDBNull(dr.GetOrdinal("fecha")))
              c.Fecha = dr.GetDateTime(dr.GetOrdinal("fecha"));

             if (!dr.IsDBNull(dr.GetOrdinal("borrado")))
              c.Borrado = dr.GetBoolean(dr.GetOrdinal("borrado"));

             return c;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using sublimacion.BussinesObjects.BussinesObjects;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace sublimacion.DataAccessObjects.DataAccessObjects
{
    public static class ClienteDAO
    {


        public static Dictionary<long, Cliente> obtenerClienteTodos()
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
                    dicCliente.Add(u.IdCliente, u);

            }

            return dicCliente;

        }

        public static Cliente obtenerClientePorId(string id)
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

        private static Cliente getClienteDelDataReader(NpgsqlDataReader dr)
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

        public static void insertarCliente(Cliente i)
        {

            string queryStr;


            queryStr = @"INSERT INTO cliente( nombre, apellido, dni, direccion, telefono, mail, fecha, borrado)
                VALUES (:nombre, :apellido, :dni, :direccion, :telefono, :mail, :fecha, :borrado)";

            NpgsqlDb.Instancia.PrepareCommand(queryStr);

            i.Fecha = DateTime.Now;
            NpgsqlDb.Instancia.AddCommandParameter(":fecha", NpgsqlDbType.Date, ParameterDirection.Input, false, i.Fecha);

            parametrosQuery(i);


            try
            {
                NpgsqlDb.Instancia.ExecuteNonQuery();

            }
            catch (System.OverflowException Ex)
            {
                throw Ex;
            }
        }

        public static void actualizarCliente(Cliente i)
        {
            string queryStr;


            queryStr = @"UPDATE cliente
                        SET nombre=:nombre, apellido=:apelido, dni=:dni, direccion=:direccion, telefono=:telefono, mail=:mail, borrado=:borrado
                    WHERE idcliente=:idcliente";

            NpgsqlDb.Instancia.PrepareCommand(queryStr);
            NpgsqlDb.Instancia.AddCommandParameter(":idcliente", NpgsqlDbType.Bigint, ParameterDirection.Input, false, i.IdCliente);

            parametrosQuery(i);

            try
            {
                NpgsqlDb.Instancia.ExecuteNonQuery();

            }
            catch (System.OverflowException Ex)
            {
                throw Ex;
            }
        }

        private static void parametrosQuery(Cliente i)
        {
            NpgsqlDb.Instancia.AddCommandParameter(":nombre", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Nombre);
            NpgsqlDb.Instancia.AddCommandParameter(":apellido", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Apellido);
            NpgsqlDb.Instancia.AddCommandParameter(":dni", NpgsqlDbType.Integer, ParameterDirection.Input, false, i.Dni);
            NpgsqlDb.Instancia.AddCommandParameter(":direccion", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Direccion);
            NpgsqlDb.Instancia.AddCommandParameter(":telefono", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Telefono);
            NpgsqlDb.Instancia.AddCommandParameter(":mail", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Mail);
            NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, i.Borrado);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sublimacion.BussinesObjects.BussinesObjects;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace sublimacion.DataAccessObjects.DataAccessObjects
{
    public static class OrdenTrabajoDAO
    {

        public static Dictionary<long, OrdenDeTrabajo> obtenerOrdenTrabajoTodos()
        {

            string sql = "";
            sql = @"SELECT idorden, fecha_comienzo, fecha_finalizacion, tiempo_estimado
                    FROM orden_de_trabajo";


            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, OrdenDeTrabajo> dic = new Dictionary <long, OrdenDeTrabajo>();

            while (dr.Read())
            {
                OrdenDeTrabajo u;
                u = getOrdenTrabajoDelDataReader(dr);


                if (!dic.ContainsKey(u.Idorden))
                    dic.Add(u.Idorden, u);

            }

            return dic;

        }

        public static OrdenDeTrabajo obtenerOrdenTrabajoPorId(string id)
        {

            string sql = "";
            sql = @"SELECT idorden, fecha_comienzo, fecha_finalizacion, tiempo_estimado
                    FROM orden_de_trabajo
                    where idorden='{0}'";

            sql = string.Format(sql, id);
            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, OrdenDeTrabajo> dic = new Dictionary<long, OrdenDeTrabajo>();
            OrdenDeTrabajo u = null;

            while (dr.Read())
            {
                u = getOrdenTrabajoDelDataReader(dr);
            }

            return u;

        }

        private static OrdenDeTrabajo getOrdenTrabajoDelDataReader(NpgsqlDataReader dr)
        {
            OrdenDeTrabajo c = new OrdenDeTrabajo();


            if (!dr.IsDBNull(dr.GetOrdinal("idorden")))
                c.Idorden = long.Parse(dr["idorden"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("fecha_comienzo")))
                c.Fecha_comienzo = dr.GetDateTime(dr.GetOrdinal("fecha_comienzo"));

            if (!dr.IsDBNull(dr.GetOrdinal("fecha_finalizacion")))
                c.Fecha_finalizacion = dr.GetDateTime(dr.GetOrdinal("fecha_finalizacion"));

            if (!dr.IsDBNull(dr.GetOrdinal("tiempo_estimado")))
                c.Tiempo_estimado = dr.GetDecimal(dr.GetOrdinal("tiempo_estimado"));

          

             return c;
        
        }
        
        public static OrdenDeTrabajo insertarOrdenTrabajo(OrdenDeTrabajo i)
        {

            string queryStr;


            queryStr = @"INSERT INTO orden_de_trabajo(fecha_comienzo, fecha_finalizacion, tiempo_estimado)
                    VALUES ( :fecha_comienzo, :fecha_finalizacion, :tiempo_estimado); SELECT currval('orden_de_trabajo_idorden_seq');";

            NpgsqlDb.Instancia.PrepareCommand(queryStr);

            parametrosQuery(i);

            try
            {
               i.Idorden= NpgsqlDb.Instancia.ExecuteScalar();

            }
            catch (System.OverflowException Ex)
            {
                throw Ex;
            }

            return i;
        }
                       
        public static void actualizarOrdenTrabajo(OrdenDeTrabajo i)
        {
            string queryStr;


            queryStr = @"UPDATE orden_de_trabajo
            SET  fecha_comienzo=:fecha_comienzo, fecha_finalizacion=:fecha_finalizacion, tiempo_estimado=:tiempo_estimado
            WHERE idorden=:idorden";

            NpgsqlDb.Instancia.PrepareCommand(queryStr);
            NpgsqlDb.Instancia.AddCommandParameter(":idorden", NpgsqlDbType.Bigint, ParameterDirection.Input, false, i.Idorden);

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

        private static void parametrosQuery(OrdenDeTrabajo i)
        {
            NpgsqlDb.Instancia.AddCommandParameter(":fecha_comienzo", NpgsqlDbType.Timestamp, ParameterDirection.Input, false, i.Fecha_comienzo);
            NpgsqlDb.Instancia.AddCommandParameter(":fecha_finalizacion", NpgsqlDbType.Timestamp, ParameterDirection.Input, false, i.Fecha_finalizacion);
            NpgsqlDb.Instancia.AddCommandParameter(":tiempo_estimado", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Tiempo_estimado);
        }
    }


}

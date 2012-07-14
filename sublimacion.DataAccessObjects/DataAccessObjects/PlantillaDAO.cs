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
    public static class PlantillaDAO
    {

        public static Dictionary<long, Plantilla> obtenerPlantillaTodos()
        {

            string sql = "";
            sql = @"SELECT  idplantilla, nombre, medida_ancho, medida_largo, borrado
                FROM plantilla
                where borrado=false";


            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Plantilla> dic = new Dictionary<long, Plantilla>();

            while (dr.Read())
            {
                Plantilla u;
                u = getPlantillaDelDataReader(dr);

                if (!dic.ContainsKey(u.IdPlantilla))
                    dic.Add(u.IdPlantilla, u);

            }

            return dic;

        }

        public static Dictionary<long, Plantilla> obtenerPlantillaPorCatalogo(string id)
        {

            string sql = "";
            sql = @"SELECT  idplantilla, nombre, medida_ancho, medida_largo, borrado
                from plantilla
                where idplantilla in
                (select id_plantilla
                from  plantilla_catalogo 
                where id_catalogo={0}) 
                and borrado=false";

            sql = string.Format(sql, id);
            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Plantilla> dic = new Dictionary<long, Plantilla>();

            while (dr.Read())
            {
                Plantilla u;
                u = getPlantillaDelDataReader(dr);

                if (!dic.ContainsKey(u.IdPlantilla))
                    dic.Add(u.IdPlantilla, u);

            }

            return dic;

        }
        public static Plantilla obtenerPlantillaPorId(string id)
        {

            string sql = "";
            sql = @"SELECT  idplantilla, nombre, medida_ancho, medida_largo, borrado
                FROM plantilla
                where borrado=false and idplantilla='{0}'";

            sql = string.Format(sql, id);
            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Plantilla> dic = new Dictionary<long, Plantilla>();
            Plantilla u = null;
            while (dr.Read())
            {
                u = getPlantillaDelDataReader(dr);

            }

            return u;

        }

        public static Plantilla getPlantillaDelDataReader(NpgsqlDataReader dr)
        {

            Plantilla c = new Plantilla();


            if (!dr.IsDBNull(dr.GetOrdinal("idplantilla")))
                c.IdPlantilla = long.Parse(dr["idplantilla"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                c.Nombre = dr.GetString(dr.GetOrdinal("nombre"));

            if (!dr.IsDBNull(dr.GetOrdinal("medida_ancho")))
                c.Medida_ancho = dr.GetDecimal(dr.GetOrdinal("medida_ancho"));

            if (!dr.IsDBNull(dr.GetOrdinal("medida_largo")))
                c.Medida_largo = dr.GetDecimal(dr.GetOrdinal("medida_largo"));


            if (!dr.IsDBNull(dr.GetOrdinal("borrado")))
                c.Borrado = dr.GetBoolean(dr.GetOrdinal("borrado"));

            return c;
        }

        public static void insertarPlantilla(Plantilla i)
        {

            string queryStr;


            queryStr = @"INSERT INTO plantilla(nombre, medida_ancho, medida_largo, borrado)
             VALUES (:nombre, :medida_ancho, :medida_largo, :borrado)";

            NpgsqlDb.Instancia.PrepareCommand(queryStr);

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

        public static void actualizarPlantilla(Plantilla i)
        {
            string queryStr;


            queryStr = @"UPDATE plantilla
                        SET nombre=:nombre, medida_ancho=:medida_ancho, medida_largo=:medida_largo, borrado=:borrado
                        WHERE  idplantilla=:idplantilla";

            NpgsqlDb.Instancia.PrepareCommand(queryStr);
            NpgsqlDb.Instancia.AddCommandParameter(":idplantilla", NpgsqlDbType.Bigint, ParameterDirection.Input, false, i.IdPlantilla);

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

        private static void parametrosQuery(Plantilla i)
        {
            NpgsqlDb.Instancia.AddCommandParameter(":nombre", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Nombre);
            NpgsqlDb.Instancia.AddCommandParameter(":medida_ancho", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Medida_ancho);
            NpgsqlDb.Instancia.AddCommandParameter(":medida_largo", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Medida_largo);
            NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, i.Borrado);

        }
    }
}

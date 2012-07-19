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
    public static class PlanProdDAO
    {
        

         public static Dictionary<long, PlanDeProduccion> obtenerPendientes()
        {

            string sql = "";
            sql += @"SELECT idplan, fecha_inicio, fecha_fin 
                    FROM plan_produccion 
                    where borrado=False
                    and idplan in
                    (
                    select id_plan_prod 
                    from pedido p 
                    inner join orden_de_trabajo o on p.id_orden_trab=o.idorden
                    where o.fecha_comienzo is null

                    )";

            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, PlanDeProduccion> dicPlan = new Dictionary<long, PlanDeProduccion>();

            while (dr.Read())
            {
                PlanDeProduccion i = getPlanDelDataReader(dr);



                if (!dicPlan.ContainsKey(i.IdPlan))
                    dicPlan.Add(i.IdPlan, i);
            }

            return dicPlan;

        }

        public static Dictionary<long, PlanDeProduccion> obtenerTodos()
        {

            string sql = "";
            sql += @"SELECT idplan, fecha_inicio, fecha_fin 
                FROM plan_produccion where borrado=False";

            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, PlanDeProduccion> dicPlan = new Dictionary<long, PlanDeProduccion>();

            while (dr.Read())
            {
                PlanDeProduccion i = getPlanDelDataReader(dr);



                if (!dicPlan.ContainsKey(i.IdPlan))
                    dicPlan.Add(i.IdPlan, i);
            }

            return dicPlan;

        }


        public static PlanDeProduccion obtenerPorId(string id)
        {

            string sql = "";
            sql += @"SELECT idplan, fecha_inicio, fecha_fin 
                FROM plan_produccion where borrado=False and idplan="+id;

            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            
            PlanDeProduccion i = new PlanDeProduccion();
            while (dr.Read())
            {
                i = getPlanDelDataReader(dr);
            }

            return i;

        }

        private static PlanDeProduccion getPlanDelDataReader(NpgsqlDataReader dr)
        {
            PlanDeProduccion i = new PlanDeProduccion();

            if (!dr.IsDBNull(dr.GetOrdinal("idplan")))
                i.IdPlan = long.Parse(dr["idplan"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("fecha_inicio")))
               i.Fecha_inicio = DateTime.Parse(dr["fecha_inicio"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("fecha_fin")))
                i.Fecha_fin = DateTime.Parse(dr["fecha_fin"].ToString());

            i.Borrado = false;

            return i;
        }


        public static PlanDeProduccion insertarPlan(PlanDeProduccion p)
        {

            string queryStr;


            queryStr = @"INSERT INTO plan_produccion(
             fecha_inicio, fecha_fin, borrado)
            VALUES ( :fecha_inicio, :fecha_fin, :borrado); SELECT currval('plan_produccion_idplan_seq');";

            NpgsqlDb.Instancia.PrepareCommand(queryStr);
            parametrosQuery(p);

            try
            {
               p.IdPlan= NpgsqlDb.Instancia.ExecuteScalar();

            }
            catch (System.OverflowException Ex)
            {
                throw Ex;
            }

            return p;


        }

        private static void parametrosQuery(PlanDeProduccion p)
        {
            NpgsqlDb.Instancia.AddCommandParameter(":fecha_inicio", NpgsqlDbType.Timestamp, ParameterDirection.Input, true, p.Fecha_inicio);
            NpgsqlDb.Instancia.AddCommandParameter(":fecha_fin", NpgsqlDbType.Timestamp, ParameterDirection.Input, true, p.Fecha_fin);
            NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, p.Borrado);

        }


    }
}

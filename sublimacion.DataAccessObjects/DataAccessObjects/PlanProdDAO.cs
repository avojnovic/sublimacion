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
   public  class PlanProdDAO
    {

         #region Singleton
        private static PlanProdDAO Instance = null;
        private PlanProdDAO() 
        {
            
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = new PlanProdDAO();
            }
        }

        public static PlanProdDAO Instancia
        {
            get
            {
                CreateInstance();
                return Instance;
            }
        }
        #endregion

        public Dictionary<long, PlanDeProduccion> obtenerTodos()
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


        public PlanDeProduccion obtenerPorId(string id)
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

        private PlanDeProduccion getPlanDelDataReader(NpgsqlDataReader dr)
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


        public void insertarPlan(PlanDeProduccion p)
        {

            string queryStr;


            queryStr = @"INSERT INTO plan_produccion(
             fecha_inicio, fecha_fin, borrado)
            VALUES ( :fecha_inicio, :fecha_fin, :borrado);";

            NpgsqlDb.Instancia.PrepareCommand(queryStr);
            PrepararParametros(p);

            try
            {
                NpgsqlDb.Instancia.ExecuteNonQuery();

            }
            catch (System.OverflowException Ex)
            {
                throw Ex;
            }

            queryStr = "SELECT currval('plan_produccion_idplan_seq')";
            NpgsqlDb.Instancia.PrepareCommand(queryStr);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                    p.IdPlan = long.Parse(dr[0].ToString());

            }

           
        }

        private void PrepararParametros(PlanDeProduccion p)
        {
            NpgsqlDb.Instancia.AddCommandParameter(":fecha_inicio", NpgsqlDbType.Timestamp, ParameterDirection.Input, true, p.Fecha_inicio);
            NpgsqlDb.Instancia.AddCommandParameter(":fecha_fin", NpgsqlDbType.Timestamp, ParameterDirection.Input, true, p.Fecha_fin);
            NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, p.Borrado);

        }


    }
}

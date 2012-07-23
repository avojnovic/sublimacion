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
    public static class EstimacionDAO
    {

        public static List<Estimacion> obtenerEstimacionesTodas(DateTime fechaDesde, DateTime fechaHasta)
        {

            string sql = "";
            sql = @"select p.id_plan_prod, pp.fecha_inicio as fiPlan, pp.fecha_fin as ffPlan, p.id_orden_trab,
                 o.fecha_comienzo as fiOrden, o.fecha_finalizacion as ffOrden, 
                 count(p.idpedido)as cantPedido
                from pedido as p
                inner join plan_produccion as pp on p.id_plan_prod = pp.idplan
                inner join orden_de_trabajo as o on p.id_orden_trab = o.idorden
                where p.borrado=false and pp.borrado=false 
                and not o.fecha_finalizacion is null
                and (p.fecha between :fechaDesde and :fechaHasta)
                group by  p.id_plan_prod, pp.fecha_inicio, pp.fecha_fin, p.id_orden_trab,
                o.fecha_comienzo, o.fecha_finalizacion
                order by p.id_plan_prod
                ";


            NpgsqlDb.Instancia.PrepareCommand(sql);

            NpgsqlDb.Instancia.AddCommandParameter(":fechaDesde", NpgsqlDbType.Timestamp, ParameterDirection.Input, true, fechaDesde);

            NpgsqlDb.Instancia.AddCommandParameter(":fechaHasta", NpgsqlDbType.Timestamp, ParameterDirection.Input, true, fechaHasta);

            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            List<Estimacion> ListEsti = new List<Estimacion>();

            while (dr.Read())
            {
                Estimacion u;
                u = getEstimacionesDelDataReader(dr);


                ListEsti.Add(u);

            }

            return ListEsti;

        }

        private static Estimacion getEstimacionesDelDataReader(NpgsqlDataReader dr)
        {

            Estimacion e = new Estimacion();


            if (!dr.IsDBNull(dr.GetOrdinal("id_plan_prod")))
                e.IdPlan = long.Parse(dr["id_plan_prod"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("fiPlan")))
                e.FechaInicioPlan = dr.GetDateTime(dr.GetOrdinal("fiPlan"));

            if (!dr.IsDBNull(dr.GetOrdinal("ffPlan")))
                e.FechaFinPlan = dr.GetDateTime(dr.GetOrdinal("ffPlan"));

            if (!dr.IsDBNull(dr.GetOrdinal("id_orden_trab")))
                e.IdOrden = long.Parse(dr["id_orden_trab"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("fiOrden")))
                e.FechaInicioOrden = dr.GetDateTime(dr.GetOrdinal("fiOrden"));

            if (!dr.IsDBNull(dr.GetOrdinal("ffOrden")))
                e.FechaFinOrden = dr.GetDateTime(dr.GetOrdinal("ffOrden"));

            if (!dr.IsDBNull(dr.GetOrdinal("cantPedido")))
                e.CantPedidos =(dr["cantPedido"].ToString());


            TimeSpan tsOrden = e.FechaFinOrden - e.FechaInicioOrden;
            TimeSpan tsPlan = e.FechaFinPlan - e.FechaInicioPlan;

            e.TiempoOrden = tsOrden.TotalMinutes;
            e.TiempoPlan = tsPlan.TotalMinutes;

            e.Estimado=(tsOrden.TotalMinutes * 100 / tsPlan.TotalMinutes);
            


            return e;

        }


    }
}

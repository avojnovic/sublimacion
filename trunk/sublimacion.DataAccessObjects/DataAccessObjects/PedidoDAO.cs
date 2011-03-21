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
   public class PedidoDAO
    {
         #region Singleton
        private static PedidoDAO Instance = null;
        private PedidoDAO() 
        {
            
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = new PedidoDAO();
            }
        }

        public static PedidoDAO Instancia
        {
            get
            {
                CreateInstance();
                return Instance;
            }
        }
        #endregion



        public Dictionary<long, Pedido> obtenerTodos()
        {

            string sql = "";
            sql = @"SELECT p.idpedido, p.fecha, p.borrado, p.comentario, p.prioridad, p.ubicacion, p.id_usuario, 
                   p.id_orden_trab, o.fecha_comienzo as fco,o.fecha_finalizacion as ffo, o.tiempo_estimado,
                   p.id_plan_prod, pl.fecha_inicio as fip ,pl.fecha_fin as ffp, id_cliente
                  FROM pedido p
                  left join usuario u on u.id=p.id_usuario
                  left join orden_de_trabajo o on o.idorden=p.id_orden_trab
                  left join plan_produccion pl on pl.idplan=p.id_plan_prod 
                  where p.borrado=false

                ";
            
            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Pedido> dicPedidos = new Dictionary<long, Pedido>();

            while (dr.Read())
            {
                Pedido p = getPedidosDelDataReader(dr);



                if (!dicPedidos.ContainsKey(p.IdPedido))
                    dicPedidos.Add(p.IdPedido, p);
            }

            return dicPedidos;

        }

        public Dictionary<long, Pedido> obtenerTodosConIdPlanProd()
        {

            string sql = "";
            sql = @"SELECT p.idpedido, p.fecha, p.borrado, p.comentario, p.prioridad, p.ubicacion, p.id_usuario, 
                   p.id_orden_trab, o.fecha_comienzo as fco,o.fecha_finalizacion as ffo, o.tiempo_estimado,
                   p.id_plan_prod, pl.fecha_inicio as fip ,pl.fecha_fin as ffp, id_cliente
                  FROM pedido p
                  left join usuario u on u.id=p.id_usuario
                  left join orden_de_trabajo o on o.idorden=p.id_orden_trab
                  left join plan_produccion pl on pl.idplan=p.id_plan_prod 
                  where p.borrado=false and  p.id_plan_prod is not NULL

                ";

            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Pedido> dicPedidos = new Dictionary<long, Pedido>();

            while (dr.Read())
            {
                Pedido p = getPedidosDelDataReader(dr);



                if (!dicPedidos.ContainsKey(p.IdPedido))
                    dicPedidos.Add(p.IdPedido, p);
            }

            return dicPedidos;

        }

        public  Pedido obtenerPorId(string id)
        {

            string sql = "";
            sql = @"SELECT p.idpedido, p.fecha, p.borrado, p.comentario, p.prioridad, p.ubicacion, p.id_usuario, 
                   p.id_orden_trab, o.fecha_comienzo as fco,o.fecha_finalizacion as ffo, o.tiempo_estimado,
                   p.id_plan_prod, pl.fecha_inicio as fip ,pl.fecha_fin as ffp, id_cliente
                  FROM pedido p
                  left join usuario u on u.id=p.id_usuario
                  left join orden_de_trabajo o on o.idorden=p.id_orden_trab
                  left join plan_produccion pl on pl.idplan=p.id_plan_prod 
                  where p.borrado=false and p.idpedido="+id+" ";

            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Pedido p = null;
            while (dr.Read())
            {
                 p = getPedidosDelDataReader(dr);
            }

            return p;

        }
        public void insertarPedido(Pedido p)
        {

            string queryStr;


            queryStr = @"INSERT INTO pedido(
             fecha, borrado, comentario, prioridad, ubicacion, id_usuario, 
            id_orden_trab, id_plan_prod, id_cliente)
                VALUES ( :fecha, :borrado, :comentario, :prioridad, :ubicacion, :id_usuario, 
                        :id_orden_trab, :id_plan_prod, :id_cliente);";

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

            queryStr = "SELECT currval('pedido_idpedido_seq')";
            NpgsqlDb.Instancia.PrepareCommand(queryStr);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                    p.IdPedido = long.Parse(dr[0].ToString());

            }

            actualizarEstados(p);
            actualizarLineasPedido(p);

        }

       


        public void actualizarPedido(Pedido p)
        {

            string queryStr;


            queryStr = @"
            UPDATE pedido
               SET idpedido=:idpedido, fecha=:fecha, borrado=:borrado, comentario=:comentario, prioridad=:prioridad, ubicacion=:ubicacion, 
                   id_usuario=:id_usuario, id_orden_trab=:id_orden_trab, id_plan_prod=:id_plan_prod, id_cliente=:id_cliente
                WHERE idpedido=:idpedido";

            NpgsqlDb.Instancia.PrepareCommand(queryStr);
            NpgsqlDb.Instancia.AddCommandParameter(":idpedido", NpgsqlDbType.Bigint, ParameterDirection.Input, true, p.IdPedido);
            PrepararParametros(p);

            try
            {
                NpgsqlDb.Instancia.ExecuteNonQuery();

            }
            catch (System.OverflowException Ex)
            {
                throw Ex;
            }



            actualizarEstados(p);
            actualizarLineasPedido(p);

        }

        private void actualizarEstados(Pedido p)
        {
            foreach (EstadosPedido estPedido in p.EstadosPedido.Values.ToList())
            {
                
                     string sql="";

                       sql=@"SELECT fecha_inicio, fecha_fin, id_pedido, id_tipo_estado
                            FROM tiempo_estado
                            where id_pedido="+p.IdPedido+" and id_tipo_estado="+estPedido.Estado.Id+";";

                       NpgsqlDb.Instancia.PrepareCommand(sql);
                       NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
                       Dictionary<long, EstadosPedido> estPed = new Dictionary<long, EstadosPedido>();

                       if (dr.HasRows)
                       {
                           while (dr.Read())
                           {
                               sql = @"
                            UPDATE tiempo_estado
                            SET fecha_inicio=:fecha_inicio, fecha_fin=:fecha_fin
                            WHERE id_pedido=:id_pedido and id_tipo_estado=:id_tipo_estado";

                               NpgsqlDb.Instancia.PrepareCommand(sql);
                               NpgsqlDb.Instancia.AddCommandParameter(":fecha_inicio", NpgsqlDbType.Timestamp, ParameterDirection.Input, true, estPedido.Fecha_inicio);
                               NpgsqlDb.Instancia.AddCommandParameter(":fecha_fin", NpgsqlDbType.Timestamp, ParameterDirection.Input, true, estPedido.Fecha_fin);
                               NpgsqlDb.Instancia.AddCommandParameter(":id_pedido", NpgsqlDbType.Bigint, ParameterDirection.Input, true, p.IdPedido);
                               NpgsqlDb.Instancia.AddCommandParameter(":id_tipo_estado", NpgsqlDbType.Bigint, ParameterDirection.Input, true, estPedido.Estado.Id);

                               try
                               {
                                   NpgsqlDb.Instancia.ExecuteNonQuery();

                               }
                               catch (System.OverflowException Ex)
                               {
                                   throw Ex;
                               }

                           }
                       }
                       else
                       {
                           sql = @"
                            INSERT INTO tiempo_estado(
                            fecha_inicio, fecha_fin, id_pedido, id_tipo_estado)
                            VALUES ( :fecha_inicio, :fecha_fin, :id_pedido, :id_tipo_estado);";

                           NpgsqlDb.Instancia.PrepareCommand(sql);
                           NpgsqlDb.Instancia.AddCommandParameter(":fecha_inicio", NpgsqlDbType.Timestamp, ParameterDirection.Input, true, estPedido.Fecha_inicio);
                           NpgsqlDb.Instancia.AddCommandParameter(":fecha_fin", NpgsqlDbType.Timestamp, ParameterDirection.Input, true, estPedido.Fecha_fin);
                           NpgsqlDb.Instancia.AddCommandParameter(":id_pedido", NpgsqlDbType.Bigint, ParameterDirection.Input, true, p.IdPedido);
                           NpgsqlDb.Instancia.AddCommandParameter(":id_tipo_estado", NpgsqlDbType.Bigint, ParameterDirection.Input, true, estPedido.Estado.Id);

                           try
                           {
                               NpgsqlDb.Instancia.ExecuteNonQuery();

                           }
                           catch (System.OverflowException Ex)
                           {
                               throw Ex;
                           }
                        
                       }


            }
        }


        private void actualizarLineasPedido(Pedido p)
        {
            //Borro e inserto nuevamente
           string sql = @"DELETE FROM linea_pedido WHERE id_pedido="+p.IdPedido;
            NpgsqlDb.Instancia.PrepareCommand(sql);
            try
            {
                NpgsqlDb.Instancia.ExecuteNonQuery();

            }
            catch (System.OverflowException Ex)
            {
                throw Ex;
            }

            if (p.LineaPedido != null)
            {
                foreach (Producto prod in p.LineaPedido.Keys.ToList())
                {
                    sql = @"INSERT INTO linea_pedido(cantidad, subtotal, id_producto, id_pedido) VALUES (:cantidad, :subtotal, :id_producto, :id_pedido)";
                    NpgsqlDb.Instancia.PrepareCommand(sql);
                    NpgsqlDb.Instancia.AddCommandParameter(":cantidad", NpgsqlDbType.Integer, ParameterDirection.Input, true, p.LineaPedido[prod]);
                    NpgsqlDb.Instancia.AddCommandParameter(":subtotal", NpgsqlDbType.Numeric, ParameterDirection.Input, true, p.LineaPedido[prod] * prod.Precio);
                    NpgsqlDb.Instancia.AddCommandParameter(":id_producto", NpgsqlDbType.Bigint, ParameterDirection.Input, true, prod.Idproducto);
                    NpgsqlDb.Instancia.AddCommandParameter(":id_pedido", NpgsqlDbType.Bigint, ParameterDirection.Input, true, p.IdPedido);

                    try
                    {
                        NpgsqlDb.Instancia.ExecuteNonQuery();

                    }
                    catch (System.OverflowException Ex)
                    {
                        throw Ex;
                    }
                }
            }

           
        }

        private static void PrepararParametros(Pedido p)
        {
            NpgsqlDb.Instancia.AddCommandParameter(":fecha", NpgsqlDbType.Timestamp, ParameterDirection.Input, true, p.Fecha);
            NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, p.Borrado);
            NpgsqlDb.Instancia.AddCommandParameter(":comentario", NpgsqlDbType.Varchar, ParameterDirection.Input, false, p.Comentario);
            NpgsqlDb.Instancia.AddCommandParameter(":prioridad", NpgsqlDbType.Integer, ParameterDirection.Input, false, p.Prioridad);
            NpgsqlDb.Instancia.AddCommandParameter(":ubicacion", NpgsqlDbType.Varchar, ParameterDirection.Input, false, p.Ubicacion);
            if (p.Usuario == null)
                NpgsqlDb.Instancia.AddCommandParameter(":id_usuario", NpgsqlDbType.Bigint, ParameterDirection.Input, true, null);
            else
                NpgsqlDb.Instancia.AddCommandParameter(":id_usuario", NpgsqlDbType.Bigint, ParameterDirection.Input, true, p.Usuario.Id);

            if (p.OrdenDeTrabajo == null || p.OrdenDeTrabajo.Idorden == 0)
                NpgsqlDb.Instancia.AddCommandParameter(":id_orden_trab", NpgsqlDbType.Bigint, ParameterDirection.Input, true, null);
            else
                NpgsqlDb.Instancia.AddCommandParameter(":id_orden_trab", NpgsqlDbType.Bigint, ParameterDirection.Input, true, p.OrdenDeTrabajo.Idorden);

            if (p.PlanDeProduccion == null || p.PlanDeProduccion.IdPlan == 0)
                NpgsqlDb.Instancia.AddCommandParameter(":id_plan_prod", NpgsqlDbType.Bigint, ParameterDirection.Input, true, null);
            else
                NpgsqlDb.Instancia.AddCommandParameter(":id_plan_prod", NpgsqlDbType.Bigint, ParameterDirection.Input, true, p.PlanDeProduccion.IdPlan);


            if (p.Cliente == null)
                NpgsqlDb.Instancia.AddCommandParameter(":id_cliente", NpgsqlDbType.Bigint, ParameterDirection.Input, true, null);
            else
                NpgsqlDb.Instancia.AddCommandParameter(":id_cliente", NpgsqlDbType.Bigint, ParameterDirection.Input, true, p.Cliente.IdCliente);

        }




        private Pedido getPedidosDelDataReader(NpgsqlDataReader dr)
        {
            Pedido p = new Pedido();


            if (!dr.IsDBNull(dr.GetOrdinal("idpedido")))
                p.IdPedido = long.Parse(dr["idpedido"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("fecha")))
                p.Fecha = DateTime.Parse(dr["fecha"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("borrado")))
                p.Borrado = dr.GetBoolean(dr.GetOrdinal("borrado"));

            if (!dr.IsDBNull(dr.GetOrdinal("comentario")))
                p.Comentario = dr.GetString(dr.GetOrdinal("comentario"));

            if (!dr.IsDBNull(dr.GetOrdinal("prioridad")))
                p.Prioridad = dr.GetInt32(dr.GetOrdinal("prioridad"));

            if (!dr.IsDBNull(dr.GetOrdinal("ubicacion")))
                p.Ubicacion = dr.GetString(dr.GetOrdinal("ubicacion"));


            string id_user = "";
            if (!dr.IsDBNull(dr.GetOrdinal("id_usuario")))
            {
                id_user = dr.GetInt64(dr.GetOrdinal("id_usuario")).ToString();
                p.Usuario= UsuarioDAO.Instancia.obtenerUsuarioPorId(id_user);
            }

            p.OrdenDeTrabajo = new OrdenDeTrabajo();
             if (!dr.IsDBNull(dr.GetOrdinal("id_orden_trab")))
                   p.OrdenDeTrabajo.Idorden= long.Parse(dr["id_orden_trab"].ToString());
             if (!dr.IsDBNull(dr.GetOrdinal("fco")))
                 p.OrdenDeTrabajo.Fecha_comienzo = DateTime.Parse(dr["fco"].ToString());
             if (!dr.IsDBNull(dr.GetOrdinal("ffo")))
                 p.OrdenDeTrabajo.Fecha_finalizacion = DateTime.Parse(dr["fco"].ToString());
             if (!dr.IsDBNull(dr.GetOrdinal("tiempo_estimado")))
                 p.OrdenDeTrabajo.Tiempo_estimado = float.Parse(dr["tiempo_estimado"].ToString());

             p.PlanDeProduccion = new PlanDeProduccion();

             if (!dr.IsDBNull(dr.GetOrdinal("id_plan_prod")))
             {
                 long id= long.Parse(dr["id_plan_prod"].ToString());
                 p.PlanDeProduccion = PlanProdDAO.Instancia.obtenerPorId(id.ToString());
             }

             if (!dr.IsDBNull(dr.GetOrdinal("fip")))
                 p.PlanDeProduccion.Fecha_inicio = DateTime.Parse(dr["fip"].ToString());
             if (!dr.IsDBNull(dr.GetOrdinal("ffp")))
                 p.PlanDeProduccion.Fecha_fin = DateTime.Parse(dr["ffp"].ToString());


             string id_cliente = "";
             if (!dr.IsDBNull(dr.GetOrdinal("id_cliente")))
             {
                 id_cliente = dr.GetInt64(dr.GetOrdinal("id_cliente")).ToString();
                 p.Cliente = ClienteDAO.Instancia.obtenerClientePorId(id_cliente) ;
             }

             p.EstadosPedido = obtenerEstadosDelPedido(p.IdPedido.ToString());

             p.LineaPedido = obtenerLineasDePedido(p.IdPedido.ToString());

          
            return p;
        }



        private Dictionary<Producto, int> obtenerLineasDePedido(string id)
        {
            string sql = "";

            sql = @"SELECT id_producto,cantidad, subtotal
                    FROM linea_pedido

                where id_pedido=" + id + ";";

            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<Producto, int> _dicLinea = new Dictionary<Producto, int>();

            while (dr.Read())
            {
                

                string idP="";
                if (!dr.IsDBNull(dr.GetOrdinal("id_producto")))
                    idP = dr.GetInt64(dr.GetOrdinal("id_producto")).ToString();

                Producto p = new Producto();
                p = ProductoDAO.Instancia.obtenerPorId(idP.Trim());

                int cant = 0;
                if (!dr.IsDBNull(dr.GetOrdinal("cantidad")))
                    cant = dr.GetInt32(dr.GetOrdinal("cantidad"));

                if (!_dicLinea.ContainsKey(p))
                    _dicLinea.Add(p,cant);
            }

            return _dicLinea;
        }


       public  Dictionary<long,EstadosPedido> obtenerEstadosDelPedido(string id)
       {
        string sql="";

           sql=@"SELECT t.fecha_inicio, t.fecha_fin, e.estado, e.idestado
                FROM tiempo_estado t
                left join tipo_estado e on e.idestado=t.id_tipo_estado
                where id_pedido="+id+";";

           NpgsqlDb.Instancia.PrepareCommand(sql);
           NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
           Dictionary<long, EstadosPedido> estPed = new Dictionary<long, EstadosPedido>();

           while (dr.Read())
           {
             EstadosPedido p = new EstadosPedido();


             if (!dr.IsDBNull(dr.GetOrdinal("fecha_inicio")))
                 p.Fecha_inicio = DateTime.Parse(dr["fecha_inicio"].ToString());
             if (!dr.IsDBNull(dr.GetOrdinal("fecha_fin")))
                 p.Fecha_fin = DateTime.Parse(dr["fecha_fin"].ToString());

             p.Estado = new Estado();
              if (!dr.IsDBNull(dr.GetOrdinal("estado")))
                 p.Estado.Descripcion = dr.GetString(dr.GetOrdinal("estado"));
             if (!dr.IsDBNull(dr.GetOrdinal("idestado")))
                 p.Estado.Id= long.Parse(dr["idestado"].ToString());


              


               if(!estPed.ContainsKey(p.Estado.Id))
                     estPed.Add( p.Estado.Id,p);
           }

           return estPed;
       }


    }
}

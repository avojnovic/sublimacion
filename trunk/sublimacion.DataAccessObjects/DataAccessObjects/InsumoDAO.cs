using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using sublimacion.BussinesObjects;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace sublimacion.DataAccessObjects.DataAccessObjects
{
    public class InsumoDAO
    {
        #region Singleton
        private static InsumoDAO Instance = null;
        private InsumoDAO() 
        {
            
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = new InsumoDAO();
            }
        }

        public static InsumoDAO Instancia
        {
            get
            {
                CreateInstance();
                return Instance;
            }
        }
        #endregion



        public void insertarInsumo(Insumo i)
        {

            string queryStr;


            queryStr = @"INSERT INTO insumo( nombre, nombre_fab, costo, borrado, stock, fecha_act_stock)
                VALUES (:nombre, :nombre_fab, :costo, :borrado, :stock, :fecha_act_stock)";
            
            NpgsqlDb.Instancia.PrepareCommand(queryStr);
           // NpgsqlDb.Instancia.AddCommandParameter(":idinsumo", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Idinsumo);
            NpgsqlDb.Instancia.AddCommandParameter(":nombre", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Nombre);
            NpgsqlDb.Instancia.AddCommandParameter(":nombre_fab", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.NombreFab);
            NpgsqlDb.Instancia.AddCommandParameter(":costo", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Costo);
            NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, i.Borrado);
            NpgsqlDb.Instancia.AddCommandParameter(":stock", NpgsqlDbType.Integer, ParameterDirection.Input, false, i.Stock);
            NpgsqlDb.Instancia.AddCommandParameter(":fecha_act_stock", NpgsqlDbType.Timestamp, ParameterDirection.Input, false, i.FechaAct);

            try
            {
                NpgsqlDb.Instancia.ExecuteNonQuery();

            }
            catch (System.OverflowException Ex)
            {
                throw Ex;
            }




            queryStr = "select currval('pedido_idpedido_seq');";
            NpgsqlDb.Instancia.PrepareCommand(queryStr);
            string idped = "";
            try
            {
                NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("pedido_idpedido_seq")))
                        idped = dr.GetString(dr.GetOrdinal("pedido_idpedido_seq"));
                }

            }
            catch (System.OverflowException Ex)
            {
                throw Ex;
            }

          

          
        }


        public void actualizarInsumo(Insumo i)
        {

            string queryStr;


            queryStr = @"UPDATE insumo
                        SET nombre=:nombre, nombre_fab=:nombre_fab, costo=:costo, borrado=:borrado, stock=:stock, fecha_act_stock=:fecha_act_stock
                    WHERE idinsumo=:idinsumo";

            NpgsqlDb.Instancia.PrepareCommand(queryStr);
            NpgsqlDb.Instancia.AddCommandParameter(":idinsumo", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Idinsumo);
            NpgsqlDb.Instancia.AddCommandParameter(":nombre", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Nombre);
            NpgsqlDb.Instancia.AddCommandParameter(":nombre_fab", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.NombreFab);
            NpgsqlDb.Instancia.AddCommandParameter(":costo", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Costo);
            NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, i.Borrado);
            NpgsqlDb.Instancia.AddCommandParameter(":stock", NpgsqlDbType.Integer, ParameterDirection.Input, false, i.Stock);
            NpgsqlDb.Instancia.AddCommandParameter(":fecha_act_stock", NpgsqlDbType.Timestamp, ParameterDirection.Input, false, i.FechaAct);

            try
            {
                NpgsqlDb.Instancia.ExecuteNonQuery();

            }
            catch (System.OverflowException Ex)
            {
                throw Ex;
            }
        }


        public Dictionary<long, Insumo> obtenerTodos()
        {

            string sql = "";
            sql += "SELECT idinsumo, nombre, nombre_fab, costo, borrado, stock, fecha_act_stock FROM insumo where borrado=False";
          
            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Insumo> dicInsumos = new Dictionary<long, Insumo>();

            while (dr.Read())
            {
                Insumo i = getInsumosDelDataReader(dr);



                if (!dicInsumos.ContainsKey(i.Idinsumo))
                    dicInsumos.Add(i.Idinsumo, i);
            }

            return dicInsumos;

        }


        public  Insumo obtenerPorId(string id)
        {

            string sql = "";
            sql += "SELECT idinsumo, nombre, nombre_fab, costo, borrado, stock, fecha_act_stock FROM insumo where borrado=False and idinsumo=" + id + " ";

            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Insumo> dicInsumos = new Dictionary<long, Insumo>();
            Insumo i = new Insumo();
            while (dr.Read())
            {
                i = getInsumosDelDataReader(dr);

            }

            return i;

        }

        private Insumo getInsumosDelDataReader(NpgsqlDataReader dr)
        {
            Insumo i = new Insumo();

            if (!dr.IsDBNull(dr.GetOrdinal("idinsumo")))
                i.Idinsumo = long.Parse(dr["idinsumo"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                i.Nombre = dr.GetString(dr.GetOrdinal("nombre"));

            if (!dr.IsDBNull(dr.GetOrdinal("nombre_fab")))
                i.NombreFab = dr.GetString(dr.GetOrdinal("nombre_fab"));

            if (!dr.IsDBNull(dr.GetOrdinal("costo")))
                 i.Costo = float.Parse(dr["costo"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("borrado")))
                i.Borrado = dr.GetBoolean(dr.GetOrdinal("borrado"));

            if (!dr.IsDBNull(dr.GetOrdinal("stock")))
                i.Stock = int.Parse(dr["stock"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("fecha_act_stock")))
                i.FechaAct = DateTime.Parse(dr["fecha_act_stock"].ToString());

            return i;
        }
       
    }
}

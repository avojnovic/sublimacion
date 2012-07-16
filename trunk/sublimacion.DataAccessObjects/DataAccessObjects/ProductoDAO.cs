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
    public static class ProductoDAO
    {

        
       public static Dictionary<long, Producto> obtenerProductoTodos()
        {

            string sql = "";
            sql += @"SELECT idproducto, nombre, precio, borrado, tiempo
                FROM producto where borrado=False
                order by nombre";

            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Producto> dicProductos = new Dictionary<long, Producto>();

            while (dr.Read())
            {
                Producto i = getProductosDelDataReader(dr);



                if (!dicProductos.ContainsKey(i.Idproducto))
                    dicProductos.Add(i.Idproducto, i);
            }

            return dicProductos;

        }

       public static Producto obtenerProductoPorId(string id)
        {

            string sql = "";
            sql += @"SELECT idproducto, nombre, precio, borrado, tiempo
                FROM producto where borrado=False and  idproducto="+id;

            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();

            Producto i = null;
            while (dr.Read())
            {
                 i = getProductosDelDataReader(dr);            
            }

            return i;

        }

       public static Producto getProductosDelDataReader(NpgsqlDataReader dr)
        {
            Producto i = new Producto();

            if (!dr.IsDBNull(dr.GetOrdinal("idproducto")))
                i.Idproducto = long.Parse(dr["idproducto"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                i.Nombre = dr.GetString(dr.GetOrdinal("nombre"));


            if (!dr.IsDBNull(dr.GetOrdinal("precio")))
                i.Precio = dr.GetDecimal(dr.GetOrdinal("precio"));

            if (!dr.IsDBNull(dr.GetOrdinal("borrado")))
                i.Borrado = dr.GetBoolean(dr.GetOrdinal("borrado"));

     

           if (!dr.IsDBNull(dr.GetOrdinal("tiempo")))
               i.Tiempo = dr.GetDecimal(dr.GetOrdinal("tiempo"));


           i.Insumos = obtenerTodosInsumos(i.Idproducto);

            return i;
        }

       private static Dictionary<long, Insumo> obtenerTodosInsumos(long p)
       {

           string sql = "";
           sql += @"SELECT idinsumo, nombre, nombre_fab, costo, borrado, stock, fecha_act_stock, cantidad 
                    FROM producto_insumo pi
                left join insumo i on i.idinsumo= pi.id_insumo    
                where pi.id_producto=" + p.ToString() + " order by nombre";

           NpgsqlDb.Instancia.PrepareCommand(sql);
           NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();

           Dictionary<long, Insumo> insumos = new Dictionary<long, Insumo>();

           while (dr.Read())
           {
               Insumo i = InsumoDAO.getInsumosDelDataReader(dr);
               i.Cantidad = dr.GetInt32(dr.GetOrdinal("cantidad"));
               insumos.Add(i.Idinsumo, i);

           }

           return insumos;

       }



       public static void insertarProducto(Producto i)
       {

           string queryStr;


           queryStr = @"INSERT INTO producto( nombre, precio, borrado,  tiempo)
                VALUES (:nombre, :precio, :borrado, :tiempo);SELECT currval('producto_idproducto_seq')";



           NpgsqlDb.Instancia.PrepareCommand(queryStr);

           parametrosQuery(i);

           try
           {
               i.Idproducto = NpgsqlDb.Instancia.ExecuteScalar();

           }
           catch (System.OverflowException Ex)
           {
               throw Ex;
           }

           guardarInsumosEnProd(i);

       }

     

       public static void actualizarProducto(Producto i)
       {

           string queryStr;


           queryStr = @"UPDATE producto
                    SET nombre=:nombre, precio=:precio, borrado=:borrado, tiempo=:tiempo
                    WHERE idproducto=:idproducto";

     


           NpgsqlDb.Instancia.PrepareCommand(queryStr);
           NpgsqlDb.Instancia.AddCommandParameter(":idproducto", NpgsqlDbType.Bigint, ParameterDirection.Input, false, i.Idproducto);

           parametrosQuery(i);

           try
           {
               NpgsqlDb.Instancia.ExecuteNonQuery();

           }
           catch (System.OverflowException Ex)
           {
               throw Ex;
           }

           guardarInsumosEnProd(i);
       }


       private static void guardarInsumosEnProd(Producto i)
       {
           //Borro e inserto nuevamente
           string sql = @"delete from producto_insumo where id_producto=" + i.Idproducto;
           NpgsqlDb.Instancia.PrepareCommand(sql);
           try
           {
               NpgsqlDb.Instancia.ExecuteNonQuery();

           }
           catch (System.OverflowException Ex)
           {
               throw Ex;
           }

           if (i.Insumos != null)
           {
               foreach (Insumo ins in i.Insumos.Values.ToList())
               {
                   sql = @"INSERT INTO producto_insumo(id_producto, id_insumo, cantidad) VALUES (:id_producto, :id_insumo, :cantidad)";
                   NpgsqlDb.Instancia.PrepareCommand(sql);
                   NpgsqlDb.Instancia.AddCommandParameter(":id_producto", NpgsqlDbType.Bigint, ParameterDirection.Input, true, i.Idproducto);
                   NpgsqlDb.Instancia.AddCommandParameter(":id_insumo", NpgsqlDbType.Bigint, ParameterDirection.Input, true, ins.Idinsumo);
                   NpgsqlDb.Instancia.AddCommandParameter(":cantidad", NpgsqlDbType.Integer, ParameterDirection.Input, true, ins.Cantidad);
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

       private static void parametrosQuery(Producto i)
       {
           NpgsqlDb.Instancia.AddCommandParameter(":nombre", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Nombre);
           NpgsqlDb.Instancia.AddCommandParameter(":precio", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Precio);
           NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, i.Borrado);
           NpgsqlDb.Instancia.AddCommandParameter(":tiempo", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Tiempo);

             
       }



    }
}

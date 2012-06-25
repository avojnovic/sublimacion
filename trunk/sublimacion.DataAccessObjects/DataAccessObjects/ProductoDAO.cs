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
            sql += @"SELECT idproducto, nombre, precio, borrado, costo, tiempo
                FROM producto where borrado=False";

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
            sql += @"SELECT idproducto, nombre, precio, borrado, costo, tiempo
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

            if (!dr.IsDBNull(dr.GetOrdinal("costo")))
                i.Costo = dr.GetDecimal(dr.GetOrdinal("costo"));


           if (!dr.IsDBNull(dr.GetOrdinal("tiempo")))
               i.Tiempo = dr.GetDecimal(dr.GetOrdinal("tiempo"));


            return i;
        }

       public static void insertarProducto(Producto i)
       {

           string queryStr;


           queryStr = @"INSERT INTO producto( nombre, precio, borrado, costo, tiempo)
                VALUES (:nombre, :precio, :borrado, :costo, :tiempo)";



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

       public static void actualizarCatalogo(Producto i)
       {

           string queryStr;


           queryStr = @"UPDATE producto
                    SET nombre=:nombre, precio=:precio, borrado=:borrado, costo=:costo, tiempo=:tiempo
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
       }

       private static void parametrosQuery(Producto i)
       {
           NpgsqlDb.Instancia.AddCommandParameter(":nombre", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Nombre);
           NpgsqlDb.Instancia.AddCommandParameter(":precio", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Precio);
           NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, i.Borrado);
           NpgsqlDb.Instancia.AddCommandParameter(":costo", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Costo);
           NpgsqlDb.Instancia.AddCommandParameter(":tiempo", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Tiempo);

             
       }



    }
}

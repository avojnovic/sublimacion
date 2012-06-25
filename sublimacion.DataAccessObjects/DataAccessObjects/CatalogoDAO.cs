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
  public static class CatalogoDAO
    {

      public static Dictionary<long, Catalogo> obtenerCatalogoTodos()
      {

          string sql = "";
          sql = @"SELECT idcatalogo, nombre, fecha, borrado
                FROM catalogo
                where borrado=false";


          NpgsqlDb.Instancia.PrepareCommand(sql);
          NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
          Dictionary<long, Catalogo> dicCatalogo = new Dictionary<long, Catalogo>();

          while (dr.Read())
          {
              Catalogo u;
              u = getCatalogoDelDataReader(dr);


              if (!dicCatalogo.ContainsKey(u.IdCatalogo))
                  dicCatalogo.Add(u.IdCatalogo, u);

          }

          return dicCatalogo;

      }

      public static Catalogo obtenerCatalogoPorId(string id)
      {

          string sql = "";
          sql = @"SELECT idcatalogo, nombre, fecha, borrado
                FROM catalogo
                where borrado=false and idcatalogo='{0}'";

          sql = string.Format(sql, id);
          NpgsqlDb.Instancia.PrepareCommand(sql);
          NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
       
          Catalogo u = null;
          while (dr.Read())
          {
              u = getCatalogoDelDataReader(dr);

          }

          return u;

      }

      private static Catalogo getCatalogoDelDataReader(NpgsqlDataReader dr)
      {
          Catalogo i = new Catalogo();

          if (!dr.IsDBNull(dr.GetOrdinal("idcatalogo")))
              i.IdCatalogo = dr.GetInt64(dr.GetOrdinal("idcatalogo"));

          if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
              i.Nombre = dr.GetString(dr.GetOrdinal("nombre"));


          if (!dr.IsDBNull(dr.GetOrdinal("fecha")))
              i.Fecha = dr.GetDateTime(dr.GetOrdinal("fecha"));


          i.Borrado = false;


          return i;
      }

      public static void insertarCatalogo (Catalogo i)
      {

          string queryStr;


          queryStr = @"INSERT INTO catalogo( nombre, fecha, borrado)
                VALUES (:nombre, :fecha, :borrado)";

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

      public static void actualizarCatalogo(Catalogo i)
      {

          string queryStr;


          queryStr = @"UPDATE catalogo
                        SET nombre=:nombre, fecha=:fecha, borrado=:borrado
                    WHERE idcatalogo=:idcatalogo";

          NpgsqlDb.Instancia.PrepareCommand(queryStr);
          NpgsqlDb.Instancia.AddCommandParameter(":idcatalogo", NpgsqlDbType.Bigint, ParameterDirection.Input, false, i.IdCatalogo);

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

      private static void parametrosQuery(Catalogo i)
      {
          NpgsqlDb.Instancia.AddCommandParameter(":nombre", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Nombre);
          NpgsqlDb.Instancia.AddCommandParameter(":fecha", NpgsqlDbType.Date, ParameterDirection.Input, false, i.Fecha);
          NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, i.Borrado);

      }

     

    }
}

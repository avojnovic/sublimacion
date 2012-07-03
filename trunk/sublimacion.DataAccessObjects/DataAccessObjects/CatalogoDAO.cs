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
          sql = @"SELECT idcatalogo, nombre, fecha, id_producto, borrado
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
          sql = @"SELECT idcatalogo, nombre, fecha, id_producto, borrado
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


          if (!dr.IsDBNull(dr.GetOrdinal("id_producto")))
              i.Producto = ProductoDAO.obtenerProductoPorId((dr.GetInt64(dr.GetOrdinal(("id_producto")))).ToString());

          if(i.Producto!=null)
             i.Plantilla = obtenerPlantilladeCatalogo(i);

          i.Borrado = false;


          return i;
      }

      public static void insertarCatalogo (Catalogo i)
      {

          string queryStr;


          queryStr = @"INSERT INTO catalogo( nombre, fecha, borrado, id_producto)
                VALUES (:nombre, :fecha, :borrado, :id_producto)";

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

          queryStr = "SELECT currval('catalogo_idcatalogo_seq')";
          NpgsqlDb.Instancia.PrepareCommand(queryStr);
          NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
          while (dr.Read())
          {
              if (!dr.IsDBNull(0))
                  i.IdCatalogo = long.Parse(dr[0].ToString());

          }

          guardarPlantillasEnCata(i);

      }

      public static void actualizarCatalogo(Catalogo i)
      {

          string queryStr;


          queryStr = @"UPDATE catalogo
                        SET nombre=:nombre, fecha=:fecha, borrado=:borrado
                    WHERE idcatalogo=:idcatalogo and id_producto=:id_producto";

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

     
          guardarPlantillasEnCata(i);
      }

      private static void parametrosQuery(Catalogo i)
      {
          NpgsqlDb.Instancia.AddCommandParameter(":nombre", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Nombre);
          NpgsqlDb.Instancia.AddCommandParameter(":fecha", NpgsqlDbType.Date, ParameterDirection.Input, false, i.Fecha);
          NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, i.Borrado);
          NpgsqlDb.Instancia.AddCommandParameter(":id_producto", NpgsqlDbType.Bigint, ParameterDirection.Input, false, i.Producto.Idproducto);

      }

      private static Dictionary<long, Plantilla> obtenerPlantilladeCatalogo (Catalogo i)
      {

          string sql = "";
          sql = @"SELECT  p.idplantilla, p.nombre, p.medida_ancho, p.medida_largo, p.borrado
                FROM plantilla p
                inner join plantilla_catalogo pc on p.idplantilla = pc.id_plantilla
                where p.borrado=false and pc.id_catalogo="+i.IdCatalogo.ToString()+" and pc.id_producto="+i.Producto.Idproducto.ToString();


          NpgsqlDb.Instancia.PrepareCommand(sql);
          NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
          Dictionary<long, Plantilla> dic = new Dictionary<long, Plantilla>();

          while (dr.Read())
          {
              Plantilla u;
              u = PlantillaDAO.getPlantillaDelDataReader(dr);

              if (!dic.ContainsKey(u.IdPlantilla))
                  dic.Add(u.IdPlantilla, u);

          }

          return dic;
      }

      private static void guardarPlantillasEnCata(Catalogo i)
      {
          //Borro e inserto nuevamente
          string sql = @"delete from plantilla_catalogo where id_catalogo=" + i.IdCatalogo.ToString() + " and id_producto=" + i.Producto.Idproducto.ToString();
          NpgsqlDb.Instancia.PrepareCommand(sql);
          try
          {
              NpgsqlDb.Instancia.ExecuteNonQuery();

          }
          catch (System.OverflowException Ex)
          {
              throw Ex;
          }

          if (i.Plantilla != null)
          {
              foreach (Plantilla pla in i.Plantilla.Values.ToList())
              {
                  sql = @"INSERT INTO plantilla_catalogo(id_catalogo, id_producto, id_plantilla) VALUES (:id_catalogo, :id_producto, :id_plantilla)";
                  NpgsqlDb.Instancia.PrepareCommand(sql);
                  NpgsqlDb.Instancia.AddCommandParameter(":id_catalogo", NpgsqlDbType.Bigint, ParameterDirection.Input, true, i.IdCatalogo);
                  NpgsqlDb.Instancia.AddCommandParameter(":id_producto", NpgsqlDbType.Bigint, ParameterDirection.Input, true, i.Producto.Idproducto);
                  NpgsqlDb.Instancia.AddCommandParameter(":id_plantilla", NpgsqlDbType.Bigint, ParameterDirection.Input, true, pla.IdPlantilla);
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

    }
}

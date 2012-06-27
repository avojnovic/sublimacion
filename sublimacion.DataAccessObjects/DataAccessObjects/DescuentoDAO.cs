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
    public static class DescuentoDAO
    {
        public static List <Descuento> obtenerDescuentoTodos()
        {

            string sql = "";
            sql = @" SELECT cantidad, descuento, fecha, p.idproducto, p.nombre, p.precio, p.borrado, p.costo,p.tiempo
                     FROM descuento d inner join producto p on d.id_producto=p.idproducto
                        WHERE p.borrado=FALSE";

           

            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            List<Descuento> listDescuento = new List<Descuento> ();

            while (dr.Read())
            {
                Descuento u;
                u = getDescuentoDelDataReader(dr);


                listDescuento.Add(u);

            }

            return listDescuento;

        }

        public static Descuento obtenerDescuentoPorId(string idProducto, string cantidad)
        {

            string sql = "";
            sql = @"SELECT cantidad, descuento, fecha, p.idproducto, p.nombre, p.precio, p.borrado, p.costo,p.tiempo
                     FROM descuento d inner join producto p on d.id_producto=p.idproducto
                        WHERE p.borrado=FALSE and d.id_producto='{0}' and cantidad={1}";

            sql = string.Format(sql, idProducto, cantidad);
            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();

            Descuento u = null;
            while (dr.Read())
            {
                u = getDescuentoDelDataReader(dr);

            }

            return u;

        }

        private static Descuento getDescuentoDelDataReader(NpgsqlDataReader dr)
        {
            Descuento i = new Descuento();

            i.Producto=ProductoDAO.getProductosDelDataReader(dr);

            if (!dr.IsDBNull(dr.GetOrdinal("cantidad")))
                i.Cantidad = dr.GetInt32(dr.GetOrdinal("cantidad"));

            if (!dr.IsDBNull(dr.GetOrdinal("descuento")))
                i.Descuento1 = dr.GetDecimal(dr.GetOrdinal("descuento"));
            
            if (!dr.IsDBNull(dr.GetOrdinal("fecha")))
                i.Fecha = dr.GetDateTime(dr.GetOrdinal("fecha"));


                return i;
        }

        public static void insertarDescuento(Descuento i)
        {

            string queryStr;


            queryStr = @"INSERT INTO descuento(cantidad, descuento, fecha, id_producto)
                         VALUES (:cantidad, :descuento, :fecha, :id_producto);";

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

        public static void actualizarDescuento(Descuento i)
        {

            string queryStr;


            queryStr = @"UPDATE descuento
                SET  descuento=:descuento, fecha=:fecha
                    WHERE id_producto=:id_producto and cantidad=:cantidad";

                            
 
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

        private static void parametrosQuery(Descuento i)
        {
            NpgsqlDb.Instancia.AddCommandParameter(":id_producto", NpgsqlDbType.Bigint, ParameterDirection.Input, false, i.Producto.Idproducto);
            NpgsqlDb.Instancia.AddCommandParameter(":cantidad", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Cantidad);
            NpgsqlDb.Instancia.AddCommandParameter(":descuento", NpgsqlDbType.Numeric, ParameterDirection.Input, false, i.Descuento1);
            NpgsqlDb.Instancia.AddCommandParameter(":fecha", NpgsqlDbType.Date, ParameterDirection.Input, false, i.Fecha);

        }
    }
}

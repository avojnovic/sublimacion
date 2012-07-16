﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using sublimacion.BussinesObjects.BussinesObjects;
using Npgsql;

namespace sublimacion.DataAccessObjects.DataAccessObjects
{
    public static class TipoEstadoDAO
    {



        public static Dictionary<long, Estado> obtenerEstados()
        {

            string sql = "";
            sql = @"SELECT idestado, estado FROM tipo_estado order by estado;";

            
            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Estado> estados = new Dictionary<long, Estado>();
           
            while (dr.Read())
            {
                Estado e = new Estado();
                e = getEstadoDelDataReader(dr);

                if(!estados.ContainsKey(e.Id))
                estados.Add(e.Id,e);
            }

            return estados;

        }
        public static Estado obtenerEstadosPorId(string id)
        {

            string sql = "";
            sql = @"SELECT idestado, estado FROM tipo_estado where idestado="+id+" ";


            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
         
            Estado e = new Estado();
            while (dr.Read())
            {
                
                e = getEstadoDelDataReader(dr);

               
            }

            return e;

        }

        private static Estado getEstadoDelDataReader(NpgsqlDataReader dr)
        {
            Estado e = new Estado();


            if (!dr.IsDBNull(dr.GetOrdinal("idestado")))
                e.Id = long.Parse(dr["idestado"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("estado")))
                e.Descripcion = dr.GetString(dr.GetOrdinal("estado"));

            return e;
        }


    }
}
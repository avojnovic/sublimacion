using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sublimacion.BussinesObjects.BussinesObjects;
using Npgsql;

namespace sublimacion.DataAccessObjects.DataAccessObjects
{
    public static class PerfilDAO
    {
        public static Dictionary<long, Perfil> obtenerTodos()
        {

            string sql = "";
            sql = @"SELECT id, perfil, descripcion, borrado
                FROM perfil
                order by descripcion";


            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Perfil> dicPerfil = new Dictionary<long, Perfil>();

            while (dr.Read())
            {
                Perfil u;
                u = getPerfilDelDataReader(dr);


                if (!dicPerfil.ContainsKey(u.Id))
                    dicPerfil.Add(u.Id, u);

            }

            return dicPerfil;

        }

        private static Perfil getPerfilDelDataReader(NpgsqlDataReader dr)
        {
            Perfil p = new Perfil();


            if (!dr.IsDBNull(dr.GetOrdinal("id")))
                p.Id = long.Parse(dr["id"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("perfil")))
                p.Nombre = dr.GetString(dr.GetOrdinal("perfil"));

            if (!dr.IsDBNull(dr.GetOrdinal("descripcion")))
                p.Descripcion = dr.GetString(dr.GetOrdinal("descripcion"));


            if (!dr.IsDBNull(dr.GetOrdinal("borrado")))
                p.Borrado = dr.GetBoolean(dr.GetOrdinal("borrado"));

            return p;
        }


    }
}

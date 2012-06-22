using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using sublimacion.BussinesObjects;
using Npgsql;

namespace sublimacion.DataAccessObjects.DataAccessObjects
{
    public static class UsuarioDAO
    {


        public static Dictionary<long, Usuario> obtenerTodos()
        {

            string sql = @"SELECT u.id, u.usuario, u.contrasenia, u.nombre, u.apellido, u.telefono, u.mail, u.borrado, p.perfil, p.id as idper FROM usuario u
              left join perfil p on u.id_perfil=p.id where u.borrado=false";
            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Usuario> dicUsuario = new Dictionary<long, Usuario>();

            while (dr.Read())
            {
                Usuario u = getUsuarioDelDataReader(dr);



                if (!dicUsuario.ContainsKey(u.Id))
                    dicUsuario.Add(u.Id, u);
            }

            return dicUsuario;

        }

        public static Usuario obtenerUsuario(string usuario, string pass)
        {

            string sql = @"
            SELECT u.id, u.usuario, u.contrasenia, u.nombre, u.apellido, u.telefono, u.mail, u.borrado, p.perfil, p.id as idper 
            FROM usuario u
            left join perfil p on u.id_perfil=p.id 
            where u.borrado=false and u.usuario='{0}' and u.contrasenia='{1}' ";

            sql=string.Format(sql,usuario, pass);


            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            
            
            Dictionary<long, Usuario> dicUsuario = new Dictionary<long, Usuario>();
            Usuario u=null;

            while (dr.Read())
            {
                u = getUsuarioDelDataReader(dr);

            }
 
            return u;

        }
        public static Usuario obtenerUsuarioPorId(string id)
        {

            string sql = @"SELECT u.id, u.usuario, u.contrasenia, u.nombre, u.apellido, u.telefono, u.mail, u.borrado, p.perfil, p.id as idper FROM usuario u
            left join perfil p on u.id_perfil=p.id where u.borrado=false and u.id='{0}' ";

            sql = string.Format(sql, id);
            NpgsqlDb.Instancia.PrepareCommand(sql);
            NpgsqlDataReader dr = NpgsqlDb.Instancia.ExecuteQuery();
            Dictionary<long, Usuario> dicUsuario = new Dictionary<long, Usuario>();
            Usuario u = null;
            while (dr.Read())
            {
                u = getUsuarioDelDataReader(dr);

            }

            return u;

        }

        private static  Usuario getUsuarioDelDataReader(NpgsqlDataReader dr)
        {
            Usuario u = new Usuario();


            if (!dr.IsDBNull(dr.GetOrdinal("id")))
                u.Id = long.Parse(dr["id"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("usuario")))
                u.User = dr.GetString(dr.GetOrdinal("usuario"));

            if (!dr.IsDBNull(dr.GetOrdinal("contrasenia")))
                u.Password = dr.GetString(dr.GetOrdinal("contrasenia"));

            if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                u.Nombre = dr.GetString(dr.GetOrdinal("nombre"));

            if (!dr.IsDBNull(dr.GetOrdinal("apellido")))
                u.Apellido = dr.GetString(dr.GetOrdinal("apellido"));

            if (!dr.IsDBNull(dr.GetOrdinal("telefono")))
                u.Telefono = dr.GetString(dr.GetOrdinal("telefono"));

            if (!dr.IsDBNull(dr.GetOrdinal("mail")))
                u.Email = dr.GetString(dr.GetOrdinal("mail"));


            switch (long.Parse(dr["idper"].ToString()))
            {
                case 1:
                    u.Perfil = sublimacion.BussinesObjects.Usuario.PerfilesEnum.Diseniador; 
                    break;
                case 2:
                    u.Perfil = sublimacion.BussinesObjects.Usuario.PerfilesEnum.Operario;
                    break;
                case 3:
                    u.Perfil = sublimacion.BussinesObjects.Usuario.PerfilesEnum.Vendedor;
                    break;
                case 4:
                    u.Perfil = sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeSuperior;
                    break;
                case 5:
                    u.Perfil = sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion;
                    break;
                case 6:
                    u.Perfil = sublimacion.BussinesObjects.Usuario.PerfilesEnum.Administrador;
                    break;
                default:
                    u.Perfil = sublimacion.BussinesObjects.Usuario.PerfilesEnum.Vendedor;
                    break;
            }


            return u;
        }
    }
}

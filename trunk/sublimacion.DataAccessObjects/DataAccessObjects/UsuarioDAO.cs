using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using sublimacion.BussinesObjects.BussinesObjects;
using Npgsql;
using System.Data;
using NpgsqlTypes;

namespace sublimacion.DataAccessObjects.DataAccessObjects
{
    public static class UsuarioDAO
    {


        public static Dictionary<long, Usuario> obtenerTodos()
        {

            string sql = @"SELECT u.id as usu_id, u.usuario as usu_usuario, u.contrasenia as usu_contrasenia, u.nombre as usu_nombre,
                      u.apellido as usu_apellido, u.telefono as usu_telefono, u.mail as usu_mail, u.borrado as usu_borrado, 
                      u.id_perfil as usu_id_perfil
                        FROM usuario u
                       inner join perfil p on u.id_perfil=p.id where u.borrado=false
                        order by u.nombre,u.apellido";
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
            SELECT  u.id as usu_id, u.usuario as usu_usuario, u.contrasenia as usu_contrasenia, u.nombre as usu_nombre,
                      u.apellido as usu_apellido, u.telefono as usu_telefono, u.mail as usu_mail, u.borrado as usu_borrado, 
                      u.id_perfil as usu_id_perfil
            FROM usuario u
            inner join perfil p on u.id_perfil=p.id 
            where u.borrado=false and u.usuario='{0}' and u.contrasenia='{1}' ";

            sql = string.Format(sql, usuario, pass);


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
        public static Usuario obtenerUsuarioPorId(string id)
        {

            string sql = @"SELECT 
                        u.id as usu_id, u.usuario as usu_usuario, u.contrasenia as usu_contrasenia, u.nombre as usu_nombre,
                      u.apellido as usu_apellido, u.telefono as usu_telefono, u.mail as usu_mail, u.borrado as usu_borrado, 
                      u.id_perfil as usu_id_perfil
            FROM usuario u
            inner join perfil p on u.id_perfil=p.id where u.borrado=false and u.id='{0}' ";

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

        public static Usuario getUsuarioDelDataReader(NpgsqlDataReader dr)
        {
            Usuario u = new Usuario();


            if (!dr.IsDBNull(dr.GetOrdinal("usu_id")))
                u.Id = long.Parse(dr["usu_id"].ToString());

            if (!dr.IsDBNull(dr.GetOrdinal("usu_usuario")))
                u.User = dr.GetString(dr.GetOrdinal("usu_usuario"));

            if (!dr.IsDBNull(dr.GetOrdinal("usu_contrasenia")))
                u.Password = dr.GetString(dr.GetOrdinal("usu_contrasenia"));

            if (!dr.IsDBNull(dr.GetOrdinal("usu_nombre")))
                u.Nombre = dr.GetString(dr.GetOrdinal("usu_nombre"));

            if (!dr.IsDBNull(dr.GetOrdinal("usu_apellido")))
                u.Apellido = dr.GetString(dr.GetOrdinal("usu_apellido"));

            if (!dr.IsDBNull(dr.GetOrdinal("usu_telefono")))
                u.Telefono = dr.GetString(dr.GetOrdinal("usu_telefono"));

            if (!dr.IsDBNull(dr.GetOrdinal("usu_mail")))
                u.Email = dr.GetString(dr.GetOrdinal("usu_mail"));


            switch (long.Parse(dr["usu_id_perfil"].ToString()))
            {
                case 1:
                    u.Perfil = Usuario.PerfilesEnum.Diseniador;
                    break;
                case 2:
                    u.Perfil = Usuario.PerfilesEnum.Operario;
                    break;
                case 3:
                    u.Perfil = Usuario.PerfilesEnum.Vendedor;
                    break;
                case 4:
                    u.Perfil = Usuario.PerfilesEnum.JefeSuperior;
                    break;
                case 5:
                    u.Perfil = Usuario.PerfilesEnum.JefeProduccion;
                    break;
                case 6:
                    u.Perfil = Usuario.PerfilesEnum.Administrador;
                    break;
                default:
                    u.Perfil = Usuario.PerfilesEnum.Vendedor;
                    break;
            }


            return u;
        }

        public static void insertarUsuario(Usuario i)
        {

            string queryStr;


            queryStr = @"INSERT INTO usuario ( usuario, contrasenia, nombre, apellido, telefono, mail, borrado, 
            id_perfil)
                VALUES (:usuario, :contrasenia, :nombre, :apellido, :telefono, :mail, :borrado, :id_perfil)";

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


        public static void actualizarUsuario(Usuario i)
        {
            string queryStr;


            queryStr = @"UPDATE usuario
                        SET usuario=:usuario, contrasenia=:contrasenia, nombre=:nombre, apellido=:apellido, telefono=:telefono, mail=:mail,  id_perfil=:id_perfil, borrado=:borrado
                    WHERE id=:id";

            NpgsqlDb.Instancia.PrepareCommand(queryStr);
            NpgsqlDb.Instancia.AddCommandParameter(":id", NpgsqlDbType.Bigint, ParameterDirection.Input, false, i.Id);

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

        private static void parametrosQuery(Usuario i)
        {
            NpgsqlDb.Instancia.AddCommandParameter(":nombre", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Nombre);
            NpgsqlDb.Instancia.AddCommandParameter(":apellido", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Apellido);
            NpgsqlDb.Instancia.AddCommandParameter(":usuario", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.User);
            NpgsqlDb.Instancia.AddCommandParameter(":contrasenia", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Password);
            NpgsqlDb.Instancia.AddCommandParameter(":telefono", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Telefono);
            NpgsqlDb.Instancia.AddCommandParameter(":mail", NpgsqlDbType.Varchar, ParameterDirection.Input, false, i.Email);
            NpgsqlDb.Instancia.AddCommandParameter(":id_perfil", NpgsqlDbType.Bigint, ParameterDirection.Input, false, i.Perfil);
            NpgsqlDb.Instancia.AddCommandParameter(":borrado", NpgsqlDbType.Boolean, ParameterDirection.Input, false, i.Borrado);

        }


    }
}

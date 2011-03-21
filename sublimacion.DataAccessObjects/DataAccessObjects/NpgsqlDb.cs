using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Data;
using NpgsqlTypes;
using System.Runtime.CompilerServices;
using sublimacion.DataAccessObjects.Properties;
using sublimacion.BussinesObjects.Exceptions;

namespace sublimacion.DataAccessObjects.DataAccessObjects
{
   
        public class NpgsqlDb
        {
            // _db("localhost", 5432, "postgres", "BlackSquare18", "GESTPRO", true, 20);
            public const string DB_USER = "postgres";
            public const string DB_PASSWORD = "BlackSquare18";


            private static NpgsqlDb _instance = null;

            private NpgsqlConnection _conn = null;
            private NpgsqlCommand command = null;
            private NpgsqlTransaction transaction = null;
            private bool _connDisposed = false;
            private object o;

            private NpgsqlDb()
            {
                o = new object();
                OpenConnection();
            }

            private NpgsqlDb(NpgsqlConnection con)
            {
                o = new object();
                _conn = con;
            }

            ~NpgsqlDb()
            {
                if (_conn != null)
                {
                    _conn.ClearAllPools();
                }
                //CloseConnection();
            }

            private void OpenConnection()
            {
                OpenConnection(string.Format(Settings.Default.sublConnString, DB_USER, DB_PASSWORD));
            }

            private void OpenConnection(string connectionString)
            {
                lock (o)
                {
                    string cs = connectionString;
                    try
                    {
                        if (_conn == null || (_conn.State != ConnectionState.Open && _conn.State != ConnectionState.Executing && _conn.State != ConnectionState.Fetching))
                        {
                            _conn = new NpgsqlConnection(cs);
                            _conn.Open();
                            _connDisposed = false;
                            _conn.Disposed += new EventHandler(conn_Disposed);
                        }
                    }
                    catch (NpgsqlException e)
                    {
                        throw new ExcepcionConexionBDNoEstablecida(e);
                    }
                }
            }

            private void conn_Disposed(object sender, EventArgs e)
            {
                _connDisposed = true;
            }

            private void CloseConnection()
            {
                lock (o)
                {
                    if (_conn != null && !_connDisposed && _conn.State == System.Data.ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }
            }

            public NpgsqlCommand Command
            {
                get
                {
                    return command;
                }
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            private static void CreateInstance()
            {
                CreateInstance(null);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            private static void CreateInstance(NpgsqlConnection con)
            {
                if (con == null)
                {

                    if (_instance == null)
                    {
                        _instance = new NpgsqlDb();
                    }
                    else
                    {
                        _instance.CheckConnStatus();
                    }
                }
                else
                {
                    _instance = new NpgsqlDb(con);
                }
            }

            private void CheckConnStatus()
            {
                if (_conn == null)
                {
                    OpenConnection();
                }
                else if (_conn.State == ConnectionState.Broken || _conn.State == ConnectionState.Closed)
                {

                    string cs = string.Format("Server={3};Port={4};User Id={0};Password={1};Database={5};Pooling=False", DB_USER, DB_PASSWORD, _conn.Host, _conn.Port, _conn.Database);
                    OpenConnection(cs);
                }
            }

            public static NpgsqlDb Instancia
            {
                get
                {
                    CreateInstance();
                    return _instance;
                }
            }

            public static NpgsqlDb CreateAndSetConnection(NpgsqlConnection con)
            {
                CreateInstance(con);
                return _instance;
            }

            public NpgsqlConnection Conexion
            {
                get
                {
                    return _conn;
                }
                set
                {
                    _conn = value;
                }
            }

            public void ExecuteNonQuery()
            {
                lock (o)
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (NpgsqlException e)
                    {
                        throw new ExcepcionComunicacionBD(e);
                    }
                }
            }

            public void ExecuteNonQuery(string sql)
            {
                lock (o)
                {
                    PrepareCommand(sql);
                    ExecuteNonQuery();
                    command.Dispose();
                }
            }

            public NpgsqlDataReader ExecuteQuery(string sql)
            {
                lock (o)
                {
                    PrepareCommand(sql);
                    NpgsqlDataReader retval = ExecuteQuery();
                    command.Dispose();
                    return retval;
                }
            }

            public NpgsqlDataReader ExecuteQuery()
            {
                lock (o)
                {
                    try
                    {
                        return command.ExecuteReader();
                    }
                    catch (NpgsqlException e)
                    {
                        throw new ExcepcionComunicacionBD(e);
                    }
                }
            }

            public long ExecuteScalar(string sql)
            {
                lock (o)
                {
                    PrepareCommand(sql);
                    long retval = ExecuteScalar();
                    command.Dispose();
                    return retval;
                }
            }

            public long ExecuteScalar()
            {
                lock (o)
                {
                    try
                    {
                        long retval = 0;
                        NpgsqlDataReader dr = command.ExecuteReader();
                        if (dr.Read())
                        {
                            retval = dr.GetInt64(0);
                        }
                        dr.Close();
                        return retval;
                    }
                    catch (NpgsqlException e)
                    {
                        throw new ExcepcionComunicacionBD(e);
                    }
                }
            }

            public void BeginTransaction()
            {
                lock (o)
                {
                    try
                    {
                        transaction = _conn.BeginTransaction();
                    }
                    catch (NpgsqlException e)
                    {
                        throw new ExcepcionComunicacionBD(e);
                    }
                }
            }

            public void Commit()
            {
                lock (o)
                {
                    try
                    {
                        if (transaction != null)
                        {
                            transaction.Commit();
                            transaction = null;
                        }
                    }
                    catch (NpgsqlException e)
                    {
                        throw new ExcepcionComunicacionBD(e);
                    }
                }
            }

            public void Rollback()
            {
                lock (o)
                {
                    try
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                            transaction = null;
                        }
                    }
                    catch (NpgsqlException e)
                    {
                        throw new ExcepcionComunicacionBD(e);
                    }
                }
            }

            public void PrepareCommand(string sql)
            {
                lock (o)
                {
                    PrepareCommand(sql, CommandType.Text);
                }
            }

            public void PrepareCommand(string sql, CommandType commandType)
            {
                lock (o)
                {
                    try
                    {
                        if (transaction == null)
                        {
                            command = new NpgsqlCommand(sql, _conn);
                        }
                        else
                        {
                            command = new NpgsqlCommand(sql, _conn, transaction);
                        }
                        command.CommandType = commandType;
                    }
                    catch (NpgsqlException e)
                    {
                        throw new ExcepcionComunicacionBD(e);
                    }
                }
            }

            public void AddCommandParameter(string parameterName, NpgsqlDbType parameterType, ParameterDirection direction, bool IsNullable, object value)
            {
                lock (o)
                {
                    try
                    {
                        NpgsqlParameter param = new NpgsqlParameter(parameterName, parameterType);
                        param.Direction = direction;
                        param.IsNullable = IsNullable;
                        param.Value = value;
                        command.Parameters.Add(param);
                    }
                    catch (NpgsqlException e)
                    {
                        throw new ExcepcionComunicacionBD(e);
                    }
                }
            }

        }

}

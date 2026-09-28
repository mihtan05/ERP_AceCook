using System;
using System.Data;
using System.Data.SqlClient;
using Npgsql;

namespace ERPKho
{
    // 1. Wrapper cho SqlTransaction
    public class SqlTransaction : IDisposable
    {
        private readonly NpgsqlTransaction _transaction;
        public SqlTransaction(NpgsqlTransaction transaction) { _transaction = transaction; }
        public void Commit() => _transaction?.Commit();
        public void Rollback() => _transaction?.Rollback();
        public void Dispose() => _transaction?.Dispose();
        public NpgsqlTransaction UnderlyingTransaction => _transaction;
    }

    // 2. Wrapper cho SqlConnection
    public class SqlConnection : IDisposable
    {
        private readonly NpgsqlConnection _conn;
        public SqlConnection(string connectionString) { _conn = new NpgsqlConnection(connectionString); }

        public void Open()
        {
            if (_conn.State != ConnectionState.Open)
                _conn.Open();
        }

        public void Close()
        {
            if (_conn.State != ConnectionState.Closed)
                _conn.Close();
        }

        public void Dispose() => _conn?.Dispose();

        public ConnectionState State => _conn.State;
        public string ConnectionString
        {
            get => _conn.ConnectionString;
            set => _conn.ConnectionString = value;
        }

        public SqlTransaction BeginTransaction()
        {
            Open();
            return new SqlTransaction(_conn.BeginTransaction());
        }

        public SqlCommand CreateCommand()
        {
            return new SqlCommand("", this);
        }

        public NpgsqlConnection UnderlyingConnection => _conn;
        public static implicit operator NpgsqlConnection(SqlConnection c) => c._conn;
    }

    // 3. Wrapper cho Parameter Collection tương thích cả SqlParameter & NpgsqlParameter
    public class SqlParameterCollection
    {
        private readonly NpgsqlParameterCollection _collection;
        public SqlParameterCollection(NpgsqlParameterCollection collection)
        {
            _collection = collection;
        }

        public NpgsqlParameter AddWithValue(string parameterName, object value)
        {
            return _collection.AddWithValue(parameterName, value ?? DBNull.Value);
        }

        public NpgsqlParameter Add(System.Data.SqlClient.SqlParameter value)
        {
            return _collection.Add(new NpgsqlParameter(value.ParameterName, value.Value ?? DBNull.Value));
        }

        public NpgsqlParameter Add(NpgsqlParameter value)
        {
            return _collection.Add(value);
        }

        public int Add(object value)
        {
            if (value is System.Data.SqlClient.SqlParameter sqlp)
            {
                _collection.Add(new NpgsqlParameter(sqlp.ParameterName, sqlp.Value ?? DBNull.Value));
            }
            else if (value is NpgsqlParameter npgp)
            {
                _collection.Add(npgp);
            }
            else
            {
                _collection.Add(value);
            }
            return _collection.Count - 1;
        }

        public void Clear() => _collection.Clear();
    }

    // 4. Wrapper cho SqlCommand
    public class SqlCommand : IDisposable
    {
        private readonly NpgsqlCommand _cmd;
        public SqlParameterCollection Parameters { get; }

        public SqlCommand(string cmdText, SqlConnection conn)
        {
            _cmd = new NpgsqlCommand(cmdText, conn.UnderlyingConnection);
            Parameters = new SqlParameterCollection(_cmd.Parameters);
        }

        public SqlCommand(string cmdText, SqlConnection conn, SqlTransaction transaction)
        {
            _cmd = new NpgsqlCommand(cmdText, conn.UnderlyingConnection, transaction?.UnderlyingTransaction);
            Parameters = new SqlParameterCollection(_cmd.Parameters);
        }

        public string CommandText
        {
            get => _cmd.CommandText;
            set => _cmd.CommandText = value;
        }

        public int ExecuteNonQuery() => _cmd.ExecuteNonQuery();
        public object ExecuteScalar() => _cmd.ExecuteScalar();

        public SqlDataReader ExecuteReader()
        {
            return new SqlDataReader(_cmd.ExecuteReader());
        }

        public void Dispose() => _cmd?.Dispose();
        public NpgsqlCommand UnderlyingCommand => _cmd;
    }

    // 5. Wrapper cho SqlDataAdapter
    public class SqlDataAdapter : IDisposable
    {
        private readonly NpgsqlDataAdapter _adapter;

        public SqlDataAdapter(SqlCommand cmd)
        {
            _adapter = new NpgsqlDataAdapter(cmd.UnderlyingCommand);
        }

        public int Fill(DataTable dataTable) => _adapter.Fill(dataTable);
        public int Fill(DataSet dataSet) => _adapter.Fill(dataSet);
        public void Dispose() => _adapter?.Dispose();
    }

    // 6. Wrapper cho SqlDataReader
    public class SqlDataReader : IDisposable
    {
        private readonly NpgsqlDataReader _reader;
        public SqlDataReader(NpgsqlDataReader reader) { _reader = reader; }

        public bool Read() => _reader.Read();
        public object this[string name] => _reader[name];
        public object this[int i] => _reader[i];
        public bool HasRows => _reader.HasRows;
        public void Close() => _reader.Close();
        public void Dispose() => _reader?.Dispose();
    }

    // 7. Lớp Helper thao tác CSDL chính
    public static class DatabaseHelper
    {
        private static readonly string connectionString =
            "Host=ep-bitter-heart-b3yu3xlc-pooler.c-4.ap-southeast-1.aws.neon.tech;" +
            "Database=erp_banhang;" +
            "Username=neondb_owner;" +
            "Password=npg_fVzi2bH5uYaj;" +
            "SslMode=Require;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        private static void AddParameters(NpgsqlCommand cmd, SqlParameter[] parameters)
        {
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    if (p != null)
                    {
                        var pgParam = new NpgsqlParameter(p.ParameterName, p.Value ?? DBNull.Value);
                        cmd.Parameters.Add(pgParam);
                    }
                }
            }
        }

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (NpgsqlConnection pgConn = conn.UnderlyingConnection)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, pgConn))
                    {
                        AddParameters(cmd, parameters);
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            return dt;
        }

        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (NpgsqlConnection pgConn = conn.UnderlyingConnection)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, pgConn))
                    {
                        AddParameters(cmd, parameters);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (NpgsqlConnection pgConn = conn.UnderlyingConnection)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, pgConn))
                    {
                        AddParameters(cmd, parameters);
                        return cmd.ExecuteScalar();
                    }
                }
            }
        }
    }
}
using System;
using System.Configuration;
using System.Data;
using Npgsql;

namespace KeToanTaiChinh.Infrastructure
{
    internal static class Db
    {
        public static string ConnectionString
        {
            get
            {
                ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["ERP_Connection"];
                if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
                    throw new ConfigurationErrorsException("Thiếu ERP_Connection trong App.config.");
                var builder = new NpgsqlConnectionStringBuilder(setting.ConnectionString);
                string password = Environment.GetEnvironmentVariable("ERP_PG_PASSWORD");
                string username = Environment.GetEnvironmentVariable("ERP_PG_USERNAME");
                if (!string.IsNullOrEmpty(password)) builder.Password = password;
                if (!string.IsNullOrEmpty(username)) builder.Username = username;
                if (string.IsNullOrWhiteSpace(builder.Password))
                    throw new ConfigurationErrorsException("Chưa đặt mật khẩu cho Neon trong App.config hoặc biến môi trường ERP_PG_PASSWORD.");
                return builder.ConnectionString;
            }
        }

        public static bool TestConnection(out string message)
        {
            try
            {
                using (NpgsqlConnection connection = OpenConnection())
                {
                    message = "Kết nối thành công tới " + connection.Database + ".";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = MoTaLoi(ex);
                return false;
            }
        }

        public static string MoTaLoi(Exception ex)
        {
            PostgresException postgres = ex as PostgresException;
            if (postgres != null)
            {
                if (postgres.SqlState == "28P01") return "Neon từ chối đăng nhập role: kiểm tra ERP_PG_USERNAME và ERP_PG_PASSWORD.";
                if (postgres.SqlState == "42501") return "Role Neon thiếu quyền: chạy Database/Neon_ProductionRoles.sql bằng tài khoản chủ sở hữu.";
                if (postgres.SqlState == "42P01") return "Neon thiếu bảng cần thiết: chạy Database/Neon_PostgreSQL.sql.";
                if (postgres.SqlState == "42703") return "Neon thiếu cột cần thiết: chạy lại Database/Neon_PostgreSQL.sql.";
            }
            return ex.Message;
        }

        public static NpgsqlConnection OpenConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public static DataTable Query(string sql, params NpgsqlParameter[] parameters)
        {
            using (NpgsqlConnection connection = OpenConnection())
            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
            {
                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);
                command.CommandTimeout = 30;
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static object Scalar(string sql, params NpgsqlParameter[] parameters)
        {
            using (NpgsqlConnection connection = OpenConnection())
            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);
                command.CommandTimeout = 30;
                return command.ExecuteScalar();
            }
        }

        public static int Execute(string sql, params NpgsqlParameter[] parameters)
        {
            using (NpgsqlConnection connection = OpenConnection())
            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);
                command.CommandTimeout = 30;
                return command.ExecuteNonQuery();
            }
        }

        public static NpgsqlParameter P(string name, object value)
        {
            return new NpgsqlParameter(name, value ?? DBNull.Value);
        }

        public static decimal Decimal(object value)
        {
            return value == null || value == DBNull.Value ? 0m : Convert.ToDecimal(value);
        }
    }
}

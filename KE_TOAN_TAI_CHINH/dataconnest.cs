using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeToanTaiChinh.Infrastructure;
using System.Configuration;
using Npgsql;

namespace ERP_BanHang
{
    public class DataConnect
    {
        public static string GetConnectionString()
        {
            return Db.ConnectionString;
        }

        public static NpgsqlConnection GetConnection()
        {
            string connectionString = GetConnectionString();
            return new NpgsqlConnection(connectionString);
        }
    }
}
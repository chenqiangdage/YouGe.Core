using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YouGe.Core.DbContexts
{

        public static class EntityFrameworkCoreExtensions
        {
        #region 数据库执行sql 拓展 同步的方式
        private static DbCommand CreateCommand(DatabaseFacade facade, string sql, out DbConnection connection, params object[] parameters)
        {
            var conn = facade.GetDbConnection();
            connection = conn;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            return cmd;
        }

        public static DataTable SqlQuery(this DatabaseFacade facade, string sql, params object[] parameters)
        {
            var dt = new DataTable();
            var command = CreateCommand(facade, sql, out DbConnection conn, parameters);
            try
            {
                var reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            return dt;
        }

        public static List<T> SqlQuery<T>(this DatabaseFacade facade, string sql, params object[] parameters) where T : class, new()
        {
            var dt = SqlQuery(facade, sql, parameters);
            return dt.ToList<T>();
        }

        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            var propertyInfos = typeof(T).GetProperties();
            var list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                var t = new T();
                foreach (PropertyInfo p in propertyInfos)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
                        p.SetValue(t, row[p.Name], null);
                }
                list.Add(t);
            }
            dt.Clear();//清空掉这个数据
            dt.Dispose();
            System.GC.Collect();// 这个只是告诉gc要回收，gc不一定真的会回收掉这些。
            return list;
        }
        #endregion

        #region 数据库执行sql 异步操作
        private static async Task<DbCommand> CreateCommandAsync(DatabaseFacade facade, string sql, DbConnection conn, params object[] parameters)
        {                        
            if (conn.State != ConnectionState.Open)
            {
               await conn.OpenAsync(); //异步打开
            }
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            return cmd;
        }

        private static async Task<DataTable> SqlQueryAsync(this DatabaseFacade facade, string sql, params object[] parameters)
        {
            var dt = new DataTable();
            var connect = facade.GetDbConnection();
            var command = await CreateCommandAsync(facade, sql, connect, parameters);
            try
            {
                var reader = await command.ExecuteReaderAsync();
                dt.Load(reader);
                reader.Close();
                connect.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }

            }
            return dt;
        }
                
        public static async Task<List<T>> SqlQueryAsync<T>(this DatabaseFacade facade, string sql, params object[] parameters) where T : class, new()
        {
            var dt =await SqlQueryAsync(facade, sql, parameters);

            return dt.ToList<T>();
        }

        #endregion
    }


}

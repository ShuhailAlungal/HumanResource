using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HumanResourceMVC.Models
{
    public static class DapperORM
    {
        private static string conStr = @"Data Source=(local)\sqlexpress;Initial Catalog=HumanResourseDB;Integrated Security=True;";

        public static void ExecuteWithOutReturn(string procName, DynamicParameters param=null)
        {
            using (SqlConnection sqlCon = new SqlConnection(conStr))
            {
                sqlCon.Open();
                sqlCon.Execute(procName, param, commandType: CommandType.StoredProcedure);
                sqlCon.Close();
            }
        }

        public static T ExecuteScalar<T>(string procName, DynamicParameters param=null)
        {
            using (SqlConnection sqlCon = new SqlConnection(conStr))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.ExecuteScalar(procName, param, commandType: CommandType.StoredProcedure),typeof(T));
            }
        }

        public static IEnumerable<T> GetList<T>(string procName, DynamicParameters param=null)
        {
            using (SqlConnection sqlCon = new SqlConnection(conStr))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procName, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
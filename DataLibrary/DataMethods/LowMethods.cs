using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;

namespace DataLibrary.DataMethods
{
    public static class LowMethods
    {
        // ბაზასთან კავშირი                                                                
        private static string GetConnectionString(string connString = "APIProject1") //connstring = default database
        {
            return ConfigurationManager.ConnectionStrings[connString].ConnectionString;
        }


        // ------------------------------------------------------------------------------------
        // მოგვაქ ინფორმაციას ბაზიდან, ხოლო შემდეგ მეთოდი აბრუნებს ლისტს
        public static async Task<IEnumerable<T>> LoadInformationsAsync<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return await cnn.QueryAsync<T>(sql).ConfigureAwait(false);
            }
        }

        public static T LoadInformations<T>(string sql, DynamicParameters dp)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql, dp).FirstOrDefault();
            }
        }

        public static void InsertInformation<T>(string sql, DynamicParameters dp)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                cnn.Query<T>(sql, dp).FirstOrDefault();
            }
        }
         
        public static async Task<T> SelectAsync<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return await cnn.QueryFirstOrDefaultAsync<T>(sql).ConfigureAwait(false);
            }
        }
    }
}

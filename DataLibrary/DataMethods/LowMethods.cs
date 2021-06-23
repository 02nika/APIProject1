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

        public static async Task InsertInformationAsync<T>(string procname, DynamicParameters dp)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                //exec name
                await cnn.QueryFirstOrDefaultAsync<T>(procname, dp, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }
         
        public static async Task<T> SelectAsync<T>(DynamicParameters dp, string procname)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return await cnn.QueryFirstOrDefaultAsync<T>(procname, dp, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }
    }
}

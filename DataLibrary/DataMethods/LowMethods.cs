using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;



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
        public static List<T> LoadInformations<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static T LoadInformations<T>(string sql, DynamicParameters dp)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql, dp).FirstOrDefault();
            }
        }
    }
}

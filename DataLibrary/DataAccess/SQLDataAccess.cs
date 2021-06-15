using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataLibrary.Models;
using DataLibrary.DataMethods;

namespace DataLibrary.DataAccess
{
    public static class SQLDataAccess
    {
        // using DynamicParameters
        // ------------------------------------------------------------------------------------
        public static Dictionary<string, AcctID> LoadJsonRequest(AcctID acctID)
        {

            //DynamicParameters
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@acctID", acctID.AcctId);


            string sql = string.Format(@"exec SelectByAccID @ID = @acctID;");


            Dictionary<string, AcctID> dict1 = new Dictionary<string, AcctID>();
            dict1.Add("acctInfo", LowMethods.LoadInformations<AcctID>(sql, dp));

            return dict1;
        }
        // ------------------------------------------------------------------------------------




        // მეთოდი რომელიც აბრუნებს მონაცემებს, იმის მიხედვით
        // თუ რა query-ის ჩავწერთ *sql ცვლადში
        public static List<AcctID> LoadAccount(string acctId)
        {
            string sql = string.Format(@"exec SelectByAccID @ID = '{0}';", acctId);
            
            return LowMethods.LoadInformations<AcctID>(sql);
        }



        // მეთოდი რომელიც აბრუნებს ლექსიკონს, ცხრილის სახელით და ცხრილის გაფილტრული 
        // მონაცემებით (რომელსაც LoadAccount მეთოდიდან ვიღებთ)
        public static Dictionary<string, AcctID> LoadAccDictTable(string ID)
        {
            string name = "AcctInfo";

            var dict1 = new Dictionary<string, AcctID>();
            dict1.Add(name, LoadAccount(ID).FirstOrDefault());

            return dict1;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataLibrary.Models;
using DataLibrary.DataMethods;


namespace DataLibrary.DataAccess
{
    public static class SQLDataAccess
    {
        // using DynamicParameters
        // ------------------------------------------------------------------------------------
        public static JsonAuthorize LoadJsonRequest(AcctID acctID)
        {

            //DynamicParameters
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@acctID", acctID.AcctId);


            string sql = string.Format(@"exec SelectByAccID @ID = @acctID;");


            AcctID acctID1 = LowMethods.LoadInformations<AcctID>(sql, dp);

            JsonAuthorize JA = new JsonAuthorize()
            {
                AcctId = acctID1,
                MerchantCode = "M888",
                Msg = acctID1 != null ? "success" : "fail",
                Code = acctID1 != null ? 0 : 50100,
                SerialNo = "20120722224255982841"
                
            };

            return JA;
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

using Dapper;
using DataLibrary.DataMethods;
using DataLibrary.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.ValidateMethods
{
    public static class AcctID
    {
        public static TransferResponse AllValidations(string transferId, string acctid, 
                                                      string currency, double amount)
        {
            bool transfer = CheckDuplicateTransferId(transferId);
            bool actId = GetAcctID(acctid, currency);
            bool amnt = CheckAmount(amount);

            if (!actId)
                return ReturnNullTransferResponse(113);
            else if (!transfer)
                return ReturnNullTransferResponse(2);
            else if (!amnt)
                return ReturnNullTransferResponse(50113);

            
            return null;
        }

        private static TransferResponse ReturnNullTransferResponse(int code)
        {

            return new TransferResponse()
            {
                TransferId = null,
                MerchantCode = null,
                AcctId = null,
                Balance = 0,
                Msg = "fail",
                Code = code,
                SerialNo = null
            };
        }

        private static bool CheckAmount(double amount)
        {
            if (amount >= 0)
                return true;
            return false;
        }

        private static bool GetAcctID(string name, string curr)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@acctId", name);
            dp.Add("@Curr", curr);
            //GetAcctRow 
            
            string acctID1 = LowMethods.SelectAsync<string>(dp, "GetAcctRow").Result;
            
            if (acctID1 != null)
                return true;
            return false;
        }

        private static bool CheckDuplicateTransferId(string transferId)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@TransId", transferId);
            //GetTransferIdFromTransferCheckType
            
            string transferId1 = LowMethods.SelectAsync<string>(dp, "GetTransferIdFromTransferCheckType").Result;

            if (transferId1 == null)
                return true;
            return false;
        }
    }
}

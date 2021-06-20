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
        public static TransferResponse AllValidations(string transferId, int type,
                                       string acctid, string currency, double amount)
        {
            string transfer = CheckDuplicateTransferId(transferId, type);
            string actId = GetAcctID(acctid, currency);
            string amnt = CheckAmount(amount);

            if (actId == null)
                return ReturnNullTransferResponse(113);
            else if (transfer == null)
                return ReturnNullTransferResponse(2);
            else if (amnt == null)
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

        private static string CheckAmount(double amount)
        {
            if (amount >= 0)
                return "Valid";
            return null;
        }

        private static string GetAcctID(string name, string curr)
        {
            string sql = string.Format(@"exec GetAcctRow @AcctId = '{0}', @curr = '{1}'", name, curr);
            
            string acctID1 = LowMethods.SelectAsync<string>(sql).Result;
            
            if (acctID1 != null)
                return "Valid";
            return acctID1;
        }

        private static string CheckDuplicateTransferId(string transferId, int type)
        {
            string sql = string.Format(@"exec GetTransferIdFromTransferCheckType @TransId = '{0}', @Type = '{1}'", transferId, type);

            string transferId1 = LowMethods.SelectAsync<string>(sql).Result;

            if (transferId1 == null)
                return "Valid";
            return null;
        }
    }
}

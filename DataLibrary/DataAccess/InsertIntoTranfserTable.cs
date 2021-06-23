using Dapper;
using DataLibrary.DataMethods;
using DataLibrary.Models.DbModels;
using DataLibrary.Models.JsonModels;
using DataLibrary.ValidateMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public class InsertIntoTranfserTable
    {
        public static TransferResponse TransResponse(Transfer transfer)
        {
            //string n = AcctID.GetAcctID(transfer.AcctId);
            TransferResponse validation = AcctID.AllValidations(transfer.TransferId, transfer.AcctId, 
                                                                transfer.Currency, transfer.Amount);

            if (validation == null)
            {
                TransferResponse tR = CheckType(transfer, transfer.Type);
                return tR;
            }
            //TransferResponse transferResponse = new TransferResponse();
            
            return validation;
        }



        public static TransferResponse CheckType(Transfer trans, int number)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@TransferId", trans.TransferId);
            dp.Add("@AcctId", trans.AcctId);
            dp.Add("@Currency", trans.Currency);
            dp.Add("@Amount", trans.Amount);
            dp.Add("@Type", trans.Type);
            dp.Add("@Channel", trans.Channel);
            dp.Add("@GameCode", trans.GameCode);
            dp.Add("@TicketId", trans.TicketId);
            dp.Add("@ReferenceId", trans.ReferenceId);
            dp.Add("@SpecialGameType", trans.SpecialGame.Type);
            dp.Add("@SpecialGameCount", trans.SpecialGame.Count);
            dp.Add("@SpecialGameSequence", trans.SpecialGame.Sequence);
            dp.Add("@RefTicketIds", trans.RefTicketIds);

            //InsertTransferTableType{0}
            var _ = LowMethods.InsertInformationAsync<Transfer>($"InsertTransferTableType{number}", dp);

            //GetTransferMerchantTxId
            DynamicParameters mercharDp = new DynamicParameters();
            mercharDp.Add("@TransferId", trans.TransferId);

            //GetBalanceFromAcctInfo
            DynamicParameters balanceDp = new DynamicParameters();
            balanceDp.Add("@AcctId", trans.AcctId);

            CreateNullTransferResponse(trans.TransferId, LowMethods.SelectAsync<int>(mercharDp, "GetTransferMerchantTxId").Result,
                                       trans.AcctId, double.Parse(LowMethods.SelectAsync<string>(balanceDp, "GetBalanceFromAcctInfo").Result),
                                       out TransferResponse Tres);

            return Tres;
        }

        private static void CreateNullTransferResponse(string transferId, int merchantxId, string acctId, double balance, out TransferResponse tResponse,
                                                       string merchantCode = "M888", string msg = "success",
                                                       int code = 0, string serialNo = "20120722231413699735")
        {
            tResponse = new TransferResponse()
            {
                TransferId = transferId, MerchantCode = merchantCode,
                MerchantTxId = merchantxId, AcctId = acctId, Balance = balance,
                Msg = msg, Code = code, SerialNo = serialNo
            };
            
        }

    }
}

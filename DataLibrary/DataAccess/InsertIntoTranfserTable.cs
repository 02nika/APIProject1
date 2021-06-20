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
            TransferResponse validation = AcctID.AllValidations(transfer.TransferId, transfer.Type,
                                               transfer.AcctId, transfer.Currency, transfer.Amount);

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

            string sql = string.Format(@"exec InsertTransferTableType{0} 
                @transferId = @TransferId, @acctId = @AcctId, @currency = @Currency, 
                @amount = @Amount, @type = @Type, @channel = @Channel, @gameCode = @GameCode,
                @ticketId = @TicketId, @referenceId = @ReferenceId, @specialGametype = @SpecialGameType,
                @specialGamecount = @SpecialGameCount, @specialGamesequence = @SpecialGameSequence,
                @refTicketIds = @RefTicketIds", number);


            LowMethods.InsertInformation<Transfer>(sql, dp);

            string merchantSql = string.Format("exec GetTransferMerchantTxId @transferId = '{0}', @type = {1}", trans.TransferId, trans.Type);

            TransferResponse tResponse = new TransferResponse() { 
                TransferId = trans.TransferId, MerchantCode = "M888",
                MerchantTxId = LowMethods.SelectAsync<int>(merchantSql).Result,
                AcctId = trans.AcctId,
                Balance = double.Parse(LowMethods.SelectAsync<string>(ReturnAcctInfoBalanceQuery(trans.AcctId)).Result),
                Msg = "success", Code = 0, SerialNo = "20120722231413699735"
            };

            return tResponse;
        }

        private static string ReturnAcctInfoBalanceQuery(string acctId)
        {
            string sql = string.Format("exec GetBalanceFromAcctInfo @acctId = '{0}'", acctId);
            return sql;
        }
    }
}

using Dapper;
using DataLibrary.DataMethods;
using DataLibrary.Models.UserBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public class GetBalance
    {
        public static UserBalance GetBalanceInformation(UserBalance uB)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@AcctIdd", uB.AcctId);
            dp.Add("@gameeCode", uB.GameCode);

            string sql = string.Format(@"exec SelectGetBalance @acctId = @AcctIdd, @gameCode = @gameeCode");


            UserBalance acctID1 = LowMethods.LoadInformations<UserBalance>(sql, dp);

            return acctID1;
        }
    }
}

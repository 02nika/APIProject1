using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models.JsonModels
{
    public class TransferResponse
    {
        public string TransferId { get; set; }
        public string MerchantCode { get; set; }
        public int MerchantTxId { get; set; }
        public string AcctId { get; set; }
        public double Balance { get; set; }
        public string Msg { get; set; }
        public int Code { get; set; }
        public string SerialNo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class JsonAuthorize
    {
        public AcctID AcctId { get; set; }
        public string MerchantCode { get; set; }
        public string Msg { get; set; }
        public int Code { get; set; }
        public string SerialNo { get; set; }
    }
}

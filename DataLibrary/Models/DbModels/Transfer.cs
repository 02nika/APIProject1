using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models.DbModels
{
    public class Transfer
    {
        public string TransferId { get; set; }
        public string AcctId { get; set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
        public int Type { get; set; }
        public string Channel { get; set; }
        public string GameCode { get; set; }
        public string TicketId { get; set; }
        public string ReferenceId { get; set; }
        public string RefTicketIds { get; set; }
        public SpecialGame SpecialGame { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace BMSZamanBrokerAddOn.Model
{
    class AccountAction
    {
        public int Type { get; set; }
        public int OppNumber { get; set; }
        public string ContractNumber { get; set; }
        public DateTime RunDate { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public AccountCodes FromAccount { get; set; }
        public AccountCodes ToAccount { get; set; }
        public String Status { get; set; }
    }
}

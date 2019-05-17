using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSZamanBrokerAddOn.Model
{
    class Partner
    {
        public string Customer { get; set; }
        public string Vendor { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Rate { get; set; }
        public decimal Amount { get; set; }
    }
}

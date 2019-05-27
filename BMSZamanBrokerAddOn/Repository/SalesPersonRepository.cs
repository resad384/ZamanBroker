using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMSZamanBrokerAddOn.Helpers;
using SAPbobsCOM;

namespace BMSZamanBrokerAddOn.Repository
{
    class SalesPersonRepository
    {
        public static string GetNameById(int id)
        {
            var salesPerson =
                (SalesPersons)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes
                    .oSalesPersons);
            salesPerson.GetByKey(id);
            return salesPerson.SalesEmployeeName;
        }
    }
}

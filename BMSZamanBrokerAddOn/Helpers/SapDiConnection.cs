using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SAPbouiCOM;
using Company = SAPbobsCOM.Company;

namespace BMSZamanBrokerAddOn.Helpers
{
    internal static class SapDiConnection
    {
        private const string Constring =
            "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";

        private static Company _company;
        private static readonly SboGuiApi _sboGuiApi = new SboGuiApi();


        public static Company Instance
        {
            get
            {
                if (_company == null)
                {
                    _sboGuiApi.Connect(Constring);
                    _company = (Company)_sboGuiApi.GetApplication(-1).Company.GetDICompany();
                }

                return _company;
            }
        }
    }
}


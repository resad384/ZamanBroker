
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace BMSZamanBrokerAddOn.Helpers
{
    class InternalConverters
    {
        public static DateTime EditTextToDateTime(string value)
        {
            SAPbobsCOM.SBObob objBridge = (SAPbobsCOM.SBObob)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.BoBridge);
            var result = DateTime.ParseExact(value, "yyyyMMdd", CultureInfo.InvariantCulture);
            return result;
        }

        public static string StringToDateTypeEdittext(string value)
        {
            return Convert.ToDateTime(value).ToString("yyyyMMdd");
        }

        public static decimal ConvertStringToDecimal(string stringValue = "0")
        {
            var result = Convert.ToDecimal(stringValue.Replace(",", "."), CultureInfo.GetCultureInfo("en"));
            return result;
        }
        public static string ConvertDecimalToString(decimal decimalValue = 0)
        {
            var result = Convert.ToString(decimalValue, CultureInfo.GetCultureInfo("en"));
            return result;
        }
    }
}

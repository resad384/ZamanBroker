using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMSZamanBrokerAddOn.Helpers;
using BMSZamanBrokerAddOn.Model;
using SAPbobsCOM;

namespace BMSZamanBrokerAddOn
{
    class OpportunityRepository
    {
        public static IList<Item> GetOpportunityItemsById(int opprotunityId)
        {
            IList<Item> items = new List<Item>();
            var recordset = (Recordset)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.BoRecordset);
            string sql = "SELECT t0.\"U_OpporItem\", t2.\"ItemName\"\r\nFROM \"OOPR\" t0\r\nleft join \"OITM\" t2 on t0.\"U_OpporItem\" = t2.\"ItemCode\"\r\nwhere t0.\"OpprId\" = {0}\r\nUNION ALL\r\nSELECT t1.\"U_OpporElavteminat\", t4.\"ItemName\"\r\nFROM \"OPR1\" t1\r\nleft join \"OITM\" t4 on t1.\"U_OpporElavteminat\" = t4.\"ItemCode\"\r\nwhere t1.\"OpprId\" = {0} and t1.\"U_OpporElavteminat\" IS NOT NULL\r\n\r\n\r\n";
            sql = String.Format(sql,opprotunityId);
            recordset.DoQuery(sql);

            for (int i = 0; i < recordset.RecordCount; i++)
            {
                items.Add(
                    new Item { Id = recordset.Fields.Item(0).Value.ToString(), Name = recordset.Fields.Item(1).Value.ToString() });
                recordset.MoveNext();
            }
            return items;
        }

        public static decimal GetCompanyRevenuePercent(int opprotunityId)
        { 
            var recordset = (Recordset)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.BoRecordset);
            string sql = "select ifnull(\"U_Opporcomm\",0) from \"OPR1\" where \"Line\" = \r\n(select max(\"Line\") from \"OPR1\" where \"OpprId\" = \'{0}\' )\r\nand \"OpprId\" = \'{0}\'";
            sql = String.Format(sql, opprotunityId);
            recordset.DoQuery(sql);
            recordset.MoveFirst();

            return InternalConverters.ConvertStringToDecimal(recordset.Fields.Item(0).Value.ToString());
        }

        public static decimal GetOpportunityUsedAmount(int opprotunityId)
        {
            var recordset = (Recordset)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.BoRecordset);
            string sql =
                "select IFNULL(SUM(t1.\"DocTotalFC\"),0) as \"Total\" from \"OPR1\" t0 \r\nleft join \"OINV\" t1 on t1.\"DocNum\" = t0.\"DocNumber\"  where t0.\"OpprId\" = {0} and t0.\"ObjType\" = 13\r\nand t1.\"CardCode\" not in (SELECT t2.\"RelatCard\" FROM \"OPR2\" t2 where t2.\"OpportId\" = {0} )\r\n";
            sql = String.Format(sql, opprotunityId);
            recordset.DoQuery(sql);
            recordset.MoveFirst();

            return InternalConverters.ConvertStringToDecimal(recordset.Fields.Item(0).Value.ToString());
        }


        public static IList<Partner> GetPartnerList(int opprotunityId)
        {
            IList<Partner> partners = new List<Partner>();

            var recordset = (Recordset)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.BoRecordset);
            string sql = "select t1.\"RelatCard\",  t1.\"U_BPrate\", t1.\"U_BPamount\", t2.\"ConnBP\" from \"OPR2\" t1 \r\nleft join \"OCRD\" t2  on t1.\"RelatCard\" = t2.\"CardCode\"\r\nwhere \"OpportId\" = \'{0}\'";

            sql = String.Format(sql, opprotunityId);
            recordset.DoQuery(sql);

            for (int i = 0; i < recordset.RecordCount; i++)
            {
                partners.Add(new Partner
                {
                    Customer = recordset.Fields.Item(0).Value.ToString(),
                    Rate = Convert.ToInt32(recordset.Fields.Item(1).Value),
                    Amount = Convert.ToDecimal(recordset.Fields.Item(2).Value),
                    Vendor = recordset.Fields.Item(3).Value.ToString()
                });
                recordset.MoveNext();
            }

            return partners;
        }

        public static int  GetOpportunityContractType(int opprotunityId)
        {
            var recordset = (Recordset)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.BoRecordset);
            string sql = "select t1.\"U_Itemsigorqrupu\", \r\ncase when t1.\"U_Itemsigorqrupu\" = 1 then \'Insurance\'\r\nwhen t1.\"U_Itemsigorqrupu\" = 2 then \'Reinsurance\'\r\nend as \"Type\"\r\nfrom \"ZB_QAS\".\"OOPR\" t0 \r\nleft join \"ZB_QAS\".\"OITM\" t1 on t0.\"U_OpporItem\" = t1.\"ItemCode\"\r\nwhere \"OpprId\" = \'{0}\'\r\n";
            sql = String.Format(sql, opprotunityId);
            recordset.DoQuery(sql);
            recordset.MoveFirst();
            return Convert.ToInt32(recordset.Fields.Item(0).Value.ToString());
        }

        public static int GetNewCodeForPlanningJE()
        {
            var recordset = (Recordset)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.BoRecordset);
            string sql = "select IFNULL(MAX(CAST(\"Code\"as int)),0)+1 from \"@BMSINSPSCH\" ";
            recordset.DoQuery(sql);
            recordset.MoveFirst();
            return Convert.ToInt32(recordset.Fields.Item(0).Value.ToString());
        }

        public static IList<ComboboxValues> GetDistRuleListForB2B()
        {
            var values = new List<ComboboxValues>();
            var recordset = (Recordset)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.BoRecordset);
            string sql = "select \"OcrCode\", \"OcrName\" from \"ZB_QAS\".\"OOCR\"  where \"OcrCode\" in (\'Energy\',\'NonEnerg\',\'DirectLL\')";
            recordset.DoQuery(sql);

            for (int i = 0; i < recordset.RecordCount; i++)
            {
                values.Add(new ComboboxValues()
                {
                    Code = recordset.Fields.Item(0).Value.ToString(),
                    Name = recordset.Fields.Item(1).Value.ToString()
                });
                recordset.MoveNext();
            }

            return values;
        }

    }
}

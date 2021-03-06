
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace BMSZamanBrokerAddOn
{

    [FormAttribute("320", "Opportunity.b1f")]
    class Opportunity : SystemFormBase
    {
        public Opportunity()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;

        private void OnCustomInitialize()
        {

        }

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                var opportunityNumber = ((SAPbouiCOM.EditText)(this.GetItem("74").Specific)).Value;
                int number = Convert.ToInt32(opportunityNumber);
                var btbPaymentForm = new BTBPaymentForm(number);
                btbPaymentForm.Show();
            }
            catch (Exception exception)
            {
                Application.SBO_Application.SetStatusBarMessage(exception.Message);
            }

        }

        private void Button1_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

        }

        private SAPbouiCOM.StaticText StaticText0;
    }
}

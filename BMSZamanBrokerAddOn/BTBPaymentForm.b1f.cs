using System;
using System.Collections.Generic;
using System.Linq;
using BMSZamanBrokerAddOn.Helpers;
using BMSZamanBrokerAddOn.Model;
using BMSZamanBrokerAddOn.Repository;
using SAPbobsCOM;
using SAPbouiCOM;
using SAPbouiCOM.Framework;
using Application = SAPbouiCOM.Framework.Application;

namespace BMSZamanBrokerAddOn
{
    [Form("BMSZamanBrokerAddOn.BTBPaymentForm", "BTBPaymentForm.b1f")]
    internal class BTBPaymentForm : UserFormBase
    {
        private readonly int _opportunityType ;
        private  string _distrule = "";
        private readonly int _opportunityNumber;
        private double _contractMonths = 0;
        private readonly SalesOpportunities _opportunity =
            (SalesOpportunities) SapDiConnection.Instance.GetBusinessObject(BoObjectTypes
                .oSalesOpportunities);
        private  SalesOpportunities _opportunityLines = (SalesOpportunities)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes
            .oSalesOpportunities);
        private string _selectedBP = "";
        private string _selectedPList = "";
        private StaticText StaticText1;
        private EditText EditText0;
        private LinkedButton LinkedButton0;
        private EditText EditText1;
        private LinkedButton LinkedButton1;
        private StaticText StaticText2;
        private StaticText StaticText0;
        private EditText EditText2;
        private StaticText StaticText3;
        private StaticText StaticText4;
        private StaticText StaticText5;
        private StaticText StaticText6;
        private StaticText StaticText7;
        private StaticText StaticText8;
        private EditText EditText3;
        private StaticText StaticText9;
        private EditText EditText4;
        private StaticText StaticText10;
        private EditText EditText5;
        private StaticText StaticText11;
        private EditText EditText6;
        private StaticText StaticText12;
        private EditText EditText7;
        private StaticText StaticText13;
        private EditText EditText8;
        private Folder Folder0;
        private Folder Folder1;
        private Folder Folder2;
        private StaticText StaticText14;
        private EditText EditText9;
        private StaticText StaticText15;
        private EditText EditText10;
        private StaticText StaticText16;
        private EditText EditText11;
        private StaticText StaticText17;
        private EditText EditText12;
        private StaticText StaticText18;
        private EditText EditText13;
        private StaticText StaticText19;
        private EditText EditText14;
        private ComboBox ComboBox0;
        private StaticText StaticText20;
        private Button Button0;
        private CheckBox CheckBox0;
        private StaticText StaticText24;
        private EditText EditText16;
        private EditText EditText17;
        private StaticText StaticText25;
        private StaticText StaticText21;
        private LinkedButton LinkedButton2;
        private StaticText StaticText22;
        private EditText EditText19;
        private Matrix Matrix0;
        private Folder Folder3;
        private StaticText StaticText23;
        private EditText EditText15;
        private LinkedButton LinkedButton3;

        public BTBPaymentForm(int opportunityNumber)
        {
            _opportunityType = OpportunityRepository.GetOpportunityContractType(opportunityNumber);
            _opportunityNumber = opportunityNumber;
            BindOpportunityToForm();
            Matrix0.AutoResizeColumns();
        }

        private void BindOpportunityToForm()
        {
            try
            {
                _opportunity.GetByKey(_opportunityNumber);
                EditText0.Value = _opportunityNumber.ToString();
                EditText3.Value = _opportunity.OpportunityName;
                EditText1.Value = _opportunity.CardCode;
                EditText4.Value = _opportunity.CustomerName;
                EditText12.Value = (_opportunity.WeightedSumLC - (double) InternalConverters.ConvertStringToDecimal(OpportunityRepository.GetOpportunityUsedAmount(_opportunityNumber).ToString())).ToString();
                EditText6.Value = SalesPersonRepository.GetNameById(_opportunity.SalesPerson);
                EditText8.Value = _opportunity.ClosingPercentage + "%";

              //  var businessPartners =
              //      (BusinessPartners) SapDiConnection.Instance.GetBusinessObject(BoObjectTypes
              //          .oBusinessPartners);
              //  businessPartners.GetByKey(_opportunity.CardCode);
              // // businessPartners.ContactEmployees.SetCurrentLine(_opportunity.ContactPerson - 1);
              ////  EditText5.Value = businessPartners.ContactEmployees.Name;

                EditText7.Value = _opportunity.StartDate.ToString("dd.MM.yy");


                //add items
                var items = OpportunityRepository.GetOpportunityItemsById(_opportunityNumber);
                var count = 1;
                foreach (var item in items)
                {
                    Matrix0.AddRow();
                    ((EditText) Matrix0.Columns.Item("#").Cells.Item(count).Specific).Value = count.ToString();
                    ((EditText) Matrix0.Columns.Item("Col_0").Cells.Item(count).Specific).Value = item.Id;
                    ((EditText) Matrix0.Columns.Item("Col_1").Cells.Item(count).Specific).Value = item.Name;
                    count++;
                }

                //dates
                EditText9.Value = DateTime.Today.ToString("yyyyMMdd");
                EditText10.Value = DateTime.Today.ToString("yyyyMMdd");
                EditText11.Value = DateTime.Today.ToString("yyyyMMdd");

                EditText16.Value =  InternalConverters.StringToDateTypeEdittext(_opportunity.UserFields.Fields.Item("U_ctrctdateto").Value.ToString());
                EditText17.Value =  InternalConverters.StringToDateTypeEdittext(_opportunity.UserFields.Fields.Item("U_cntrctdateFrom").Value.ToString());
                _distrule = _opportunity.UserFields.Fields.Item("U_DistRule").Value.ToString();
                _contractMonths = InternalConverters.EditTextToDateTime(EditText17.Value).Subtract(InternalConverters.EditTextToDateTime(EditText16.Value)).Days /
                                  (365.25 / 12);
                _contractMonths = Math.Round(_contractMonths);
                StaticText25.Caption = _contractMonths + " months";

            }
            catch (Exception exception)
            {
                Application.SBO_Application.SetStatusBarMessage(exception.Message);
                UIAPIRawForm.Close();
            }
        }

        /// <summary>
        ///     Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_2").Specific));
            this.LinkedButton0 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_3").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_4").Specific));
            this.LinkedButton1 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_5").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_14").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_15").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_16").Specific));
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_17").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_18").Specific));
            this.StaticText11 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_19").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_20").Specific));
            this.StaticText12 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_21").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_22").Specific));
            this.StaticText13 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_23").Specific));
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("Item_24").Specific));
            this.StaticText14 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_30").Specific));
            this.EditText9 = ((SAPbouiCOM.EditText)(this.GetItem("Item_31").Specific));
            this.StaticText15 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_32").Specific));
            this.EditText10 = ((SAPbouiCOM.EditText)(this.GetItem("Item_33").Specific));
            this.StaticText16 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_34").Specific));
            this.EditText11 = ((SAPbouiCOM.EditText)(this.GetItem("Item_35").Specific));
            this.StaticText17 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_36").Specific));
            this.EditText12 = ((SAPbouiCOM.EditText)(this.GetItem("Item_37").Specific));
            this.StaticText18 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_38").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_42").Specific));
            this.ComboBox0.ComboSelectAfter += this.ComboBox0_ComboSelectAfter;
            this.StaticText20 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_43").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_44").Specific));
            this.Button0.PressedAfter += this.Button0_PressedAfter;
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("Item_7").Specific));
            this.Folder3 = ((SAPbouiCOM.Folder)(this.GetItem("Item_9").Specific));
            this.CheckBox0 = ((SAPbouiCOM.CheckBox)(this.GetItem("Item_6").Specific));
            this.StaticText24 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_27").Specific));
            this.EditText16 = ((SAPbouiCOM.EditText)(this.GetItem("Item_28").Specific));
            this.EditText17 = ((SAPbouiCOM.EditText)(this.GetItem("Item_29").Specific));
            this.StaticText25 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_39").Specific));
            this.StaticText21 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.LinkedButton2 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_12").Specific));
            this.StaticText22 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_25").Specific));
            this.EditText19 = ((SAPbouiCOM.EditText)(this.GetItem("Item_40").Specific));
            this.EditText19.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText19_ChooseFromListAfter);
            this.StaticText23 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_11").Specific));
            this.EditText15 = ((SAPbouiCOM.EditText)(this.GetItem("Item_26").Specific));
            this.EditText15.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText15_ChooseFromListAfter);
            this.LinkedButton3 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_41").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        ///     Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            RightClickBefore += Form_RightClickBefore;
            RightClickAfter += Form_RightClickAfter;
            ActivateAfter += Form_ActivateAfter;
        }
        private void OnCustomInitialize()
        {
        }
        private void ComboBox0_ComboSelectAfter(object sboObject, SBOItemEventArg pVal)
        {
                StaticText20.Caption = ComboBox0.Selected.Description;
        }
        private void Form_RightClickBefore(ref ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;

            if (eventInfo.ItemUID == "Item_7")
            {
                MenuCreationParams oCreationPackage;
                Menus oMenus;
                MenuItem oMenuItem;

                oCreationPackage =
                    (MenuCreationParams) Application.SBO_Application.CreateObject(BoCreatableObjectType
                        .cot_MenuCreationParams);
                //oCreationPackage.Type = BoMenuType.mt_STRING;
                //oCreationPackage.UniqueID = "BTBPFItemsMatrixAddRow";
                //oCreationPackage.String = "Add Row";
                //oCreationPackage.Enabled = true;

                oCreationPackage.Type = BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "BTBPFItemsMatrixDelRow";
                oCreationPackage.String = "Delete Row";
                oCreationPackage.Enabled = true;

                oMenuItem = Application.SBO_Application.Menus.Item("1280");
                oMenus = oMenuItem.SubMenus;
                oMenus.AddEx(oCreationPackage);
            }
        }
        private void Form_RightClickAfter(ref ContextMenuInfo eventInfo)
        {
            if (eventInfo.ItemUID == "Item_7") Application.SBO_Application.Menus.RemoveEx("BTBPFItemsMatrixDelRow");
        }
        private void EditText15_ChooseFromListAfter(object sboObject, SBOItemEventArg pVal)
        {
            if (((SBOChooseFromListEventArg) pVal).SelectedObjects != null)
                _selectedPList = ((SBOChooseFromListEventArg) pVal).SelectedObjects.GetValue(0, 0)
                    .ToString();
        }
        private void EditText19_ChooseFromListAfter(object sboObject, SBOItemEventArg pVal)
        {
            if (((SAPbouiCOM.SBOChooseFromListEventArg)(pVal)).SelectedObjects != null)
                _selectedBP = ((SAPbouiCOM.SBOChooseFromListEventArg)(pVal)).SelectedObjects.GetValue(0, 0)
                    .ToString();

        }
        private void Form_ActivateAfter(SBOItemEventArg pVal)
        {
            if (EditText19.Value != _selectedBP)
            {
                EditText19.Value = _selectedBP;
                var businessPartners =
                    (SAPbobsCOM.BusinessPartners) SapDiConnection.Instance.GetBusinessObject(SAPbobsCOM.BoObjectTypes
                        .oBusinessPartners);
                businessPartners.GetByKey(_selectedBP);
                StaticText22.Caption = businessPartners.CardName;
            }

            if (EditText15.Value != _selectedPList)
            {
                EditText15.Value = _selectedPList;
            }
        }
        private void Button0_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            var answer = Application.SBO_Application.MessageBox("Create Documents For  Opportunity ?", 2, "Yes", "No");
            if (answer == 2) return;

            if (InternalConverters.ConvertStringToDecimal(EditText12.Value ) == 0)
            {
                Application.SBO_Application.SetStatusBarMessage("Amount Must Be More Than Zero");
                return;
            }

            if (ComboBox0.Selected == null)
            {
                Application.SBO_Application.SetStatusBarMessage("Please Select Operation Type");
                return;
            }

            if (Convert.ToDecimal(EditText12.Value) == 0)
            {
                Application.SBO_Application.SetStatusBarMessage("Please Fill Amount");
                return;
            }

            if (string.IsNullOrEmpty(EditText9.Value) || string.IsNullOrEmpty(EditText9.Value) ||
                string.IsNullOrEmpty(EditText9.Value))
            {
                Application.SBO_Application.SetStatusBarMessage("Please Fill Dates");
                return;
            }

            if (String.IsNullOrEmpty(EditText19.Value))
            {
                Application.SBO_Application.SetStatusBarMessage("Please Select Business Partner");
                return;
            }


            if (String.IsNullOrEmpty(EditText15.Value))
            {
                Application.SBO_Application.SetStatusBarMessage("Please Select Payment List");
                return;
            }

            try
            {
                var result = new Result();
                SapDiConnection.Instance.StartTransaction();

                //cretae A/R A/P Documents
                switch (ComboBox0.Selected.Description)
                {
                    case "Forma 1":
                        result = CreateType1();
                        break;
                    case "Forma 2":
                        result = CreateType2();
                        break;
                    case "Forma 3":
                        result = CreateType3();
                        break;
                }

                if (result.Code != 0)
                {
                    throw new Exception(result.Message);
                }

                //create JE schedule
                if (!CheckBox0.Checked)
                {
                    result = CreateJESchedule();
                }

                if (result.Code != 0)
                {
                    throw new Exception(result.Message);
                }


                SapDiConnection.Instance.EndTransaction(BoWfTransOpt.wf_Commit);
                Application.SBO_Application.MessageBox("Documents Succesfully Created");
                UIAPIRawForm.Close();

            }
            catch (Exception exception)
            {
                Application.SBO_Application.SetStatusBarMessage(exception.Message);
                if (SapDiConnection.Instance.InTransaction)
                    SapDiConnection.Instance.EndTransaction(BoWfTransOpt.wf_RollBack);
                Application.SBO_Application.MessageBox(exception.Message);
            }
        }
        private Result CreateJESchedule()
        {
            int errCode;
            string errMSG;
            var newCode = OpportunityRepository.GetNewCodeForPlanningJE();
            var accountActions = CreateListOfActionsForAccounts();

            foreach (var accountAction in accountActions)
            {
                switch (accountAction.Type)
                {
                    case 1:
                        AddJournalEntry(accountAction);
                        break;
                    case 2:
                        AddPlanningJournalEntry(accountAction, newCode);
                        newCode++;
                        break;
                }

                SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
                if (errCode != 0) return new Result { Code = errCode, Message = errMSG };
            }

            SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
            return new Result { Code = errCode, Message = errMSG };

          
        }

        private void AddPlanningJournalEntry(AccountAction accountAction, int newCode)
        {
            UserTable myUDT = SapDiConnection.Instance.UserTables.Item("BMSINSPSCH");
            myUDT.Code = newCode.ToString();
            myUDT.Name = newCode.ToString();
            myUDT.UserFields.Fields.Item("U_OPPNUM").Value = EditText0.Value;
            myUDT.UserFields.Fields.Item("U_RUNDATE").Value = accountAction.RunDate;
            myUDT.UserFields.Fields.Item("U_AMOUNT").Value = accountAction.Amount;
            myUDT.UserFields.Fields.Item("U_DEBITACC").Value = ((int)accountAction.FromAccount).ToString();
            myUDT.UserFields.Fields.Item("U_CREDITACC").Value = ((int)accountAction.ToAccount).ToString();
            myUDT.UserFields.Fields.Item("U_CURRENCy").Value = accountAction.Currency;

            myUDT.Add();
        }

        private void AddJournalEntry(AccountAction accountAction)
        {
            var journalEntries = (JournalEntries)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.oJournalEntries);

            journalEntries.DueDate = accountAction.RunDate;
            journalEntries.TaxDate = accountAction.RunDate;
            journalEntries.ReferenceDate = accountAction.RunDate;
            journalEntries.VatDate = accountAction.RunDate;
            journalEntries.StornoDate = accountAction.RunDate;

            journalEntries.Lines.AccountCode =  ((int)accountAction.FromAccount).ToString();
            journalEntries.Lines.Debit = accountAction.Amount;
           // journalEntries.Lines.FCCurrency = accountAction.Currency;
            journalEntries.Lines.Add();

            journalEntries.Lines.AccountCode = ((int)accountAction.ToAccount).ToString();
            journalEntries.Lines.Credit = accountAction.Amount;
           // journalEntries.Lines.FCCurrency = accountAction.Currency;
            journalEntries.Lines.Add();

            journalEntries.Add();

        }

        private List<AccountAction> CreateListOfActionsForAccounts()
        {
            List<AccountAction> actions = new List<AccountAction>();
            var currency = _opportunity.UserFields.Fields.Item("U_CURRENCY").Value.ToString();
            var begda = InternalConverters.EditTextToDateTime(EditText16.Value);
            var endda = InternalConverters.EditTextToDateTime(EditText17.Value);
            var diffMonth = Math.Round(endda.Subtract(begda).Days / (365.25 / 12)) ;
            var recordset =  ((SBObob)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.BoBridge)).GetCurrencyRate(currency, DateTime.Today);
            recordset.MoveFirst();
            var rate =  InternalConverters.ConvertStringToDecimal(recordset.Fields.Item(0).Value.ToString());
            var amount =
                (double) ((InternalConverters.ConvertStringToDecimal(EditText12.Value) *
                           (decimal) _opportunity.GrossProfit) / 100) / diffMonth;
            AccountCodes toAccount = AccountCodes.insuranceBrokerage;

            if (_opportunityType == 1)
            {
                toAccount = AccountCodes.insuranceBrokerage;
            }
            else if (_opportunityType == 2)
            {
                toAccount = AccountCodes.reInsuranceBrokerage;
            }

        
            amount = amount * (double) rate;
            for (int i = 0; i < diffMonth; i++)
            {
                if (begda < DateTime.Today)
                {
                    actions.Add(new AccountAction()
                    {
                        //JE
                        Type = 1,
                        OppNumber = _opportunityNumber,
                        ContractNumber = "",
                        RunDate = begda,
                        Amount = amount,
                        Currency = "AZN",
                        FromAccount = AccountCodes.clearingFirst,
                        ToAccount = toAccount
                    });
                }
                else
                {
                    actions.Add(new AccountAction()
                    {
                        //JE Planning
                        Type = 2,
                        OppNumber = _opportunityNumber,
                        ContractNumber = "",
                        RunDate = begda,
                        Amount = amount,
                        Currency = "AZN",
                        FromAccount = AccountCodes.clearingFirst,
                        ToAccount = toAccount
                    });
                }

                begda = begda.AddMonths(1);
            }

            return actions;
        }

        private Result CreateType1()
        {
            int errCode;
            string errMSG;
            var newObjectCode = "";
            var partners = OpportunityRepository.GetPartnerList(_opportunityNumber);

           if (partners.Count > 1 || partners.Count == 0) return new Result {Code = 10, Message = "Check Partners"};

            var ARInvoice = (Documents) SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.oInvoices);

            ARInvoice.CardCode = EditText19.Value;
            ARInvoice.DocDate = InternalConverters.EditTextToDateTime(EditText9.Value);
            ARInvoice.DocDueDate = InternalConverters.EditTextToDateTime(EditText10.Value);
            ARInvoice.TaxDate = InternalConverters.EditTextToDateTime(EditText11.Value);
            ARInvoice.SalesPersonCode = _opportunity.SalesPerson;
            ARInvoice.DocCurrency = _opportunity.UserFields.Fields.Item("U_CURRENCY").Value.ToString();
            ARInvoice.UserFields.Fields.Item("U_OppId").Value = _opportunityNumber;
            for (var i = 1; i <= Matrix0.RowCount; i++)
            {
                ARInvoice.Lines.ItemCode = ((EditText) Matrix0.Columns.Item(1).Cells.Item(i).Specific).Value;
                ARInvoice.Lines.Quantity = 1;

                if (_opportunityType == 1 && CheckBox0.Checked)
                {
                    ARInvoice.Lines.AccountCode = ((int)AccountCodes.insuranceBrokerage).ToString();
                }
                else if (_opportunityType == 2 && CheckBox0.Checked)
                {
                    ARInvoice.Lines.AccountCode = ((int)AccountCodes.reInsuranceBrokerage).ToString();
                }
                else if (!CheckBox0.Checked)
                {
                    ARInvoice.Lines.AccountCode = ((int)AccountCodes.clearingFirst).ToString();
                }

                ARInvoice.Lines.Price =  (double) ((InternalConverters.ConvertStringToDecimal(EditText12.Value) * (decimal) _opportunity.GrossProfit) / 100);

                ARInvoice.Lines.CostingCode = "B2B";
                ARInvoice.Lines.CostingCode2 = _distrule;

                ARInvoice.Lines.Add();
            }
            
            ARInvoice.Add();

            SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
            if (errCode != 0) return new Result {Code = errCode, Message = errMSG};
            SapDiConnection.Instance.GetNewObjectCode(out newObjectCode);

            AddStageToOpportunity(newObjectCode, BoAPARDocumentTypes.bodt_Invoice);

            SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
            return new Result {Code = errCode, Message = errMSG};
        }

        private Result CreateType3()
        {
            int errCode;
            string errMSG;
            var newObjectCode = "";
            var percent = OpportunityRepository.GetCompanyRevenuePercent(_opportunityNumber);
            var partners = OpportunityRepository.GetPartnerList(_opportunityNumber);
           
            if (partners.Count == 0) return new Result { Code = 10, Message = "Check Partners" };

            var ARInvoice = (Documents)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.oInvoices);
            ARInvoice.CardCode = EditText19.Value;
            ARInvoice.DocDate = InternalConverters.EditTextToDateTime(EditText9.Value);
            ARInvoice.DocDueDate = InternalConverters.EditTextToDateTime(EditText10.Value);
            ARInvoice.TaxDate = InternalConverters.EditTextToDateTime(EditText11.Value);
            ARInvoice.DocCurrency = _opportunity.UserFields.Fields.Item("U_CURRENCY").Value.ToString();
            ARInvoice.SalesPersonCode = _opportunity.SalesPerson;
            ARInvoice.UserFields.Fields.Item("U_OppId").Value = _opportunityNumber;

            for (var i = 1; i <= Matrix0.RowCount; i++)
            {
                ARInvoice.Lines.ItemCode = ((EditText)Matrix0.Columns.Item(1).Cells.Item(i).Specific).Value;
                ARInvoice.Lines.Quantity = 1;

                if (_opportunityType == 1 && CheckBox0.Checked)
                {
                    ARInvoice.Lines.AccountCode = ((int)AccountCodes.insuranceBrokerage).ToString();
                }
                else if (_opportunityType == 2 && CheckBox0.Checked)
                {
                    ARInvoice.Lines.AccountCode = ((int)AccountCodes.reInsuranceBrokerage).ToString();
                }
                else if (!CheckBox0.Checked)
                {
                    ARInvoice.Lines.AccountCode = ((int)AccountCodes.clearingFirst).ToString();
                }

                ARInvoice.Lines.Price = (double)(InternalConverters.ConvertStringToDecimal(EditText12.Value));

                ARInvoice.Lines.CostingCode = "B2B";
                ARInvoice.Lines.CostingCode2 = _distrule;

                ARInvoice.Lines.Add();
            }


            ARInvoice.Add();

            SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
            if (errCode != 0) return new Result { Code = errCode, Message = errMSG };
            SapDiConnection.Instance.GetNewObjectCode(out newObjectCode);

            AddStageToOpportunity(newObjectCode, BoAPARDocumentTypes.bodt_Invoice);

            SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
            if (errCode != 0) return new Result { Code = errCode, Message = errMSG };

            //partners
            foreach (var partner in partners)
            {

                //ap for partner
                var APInvoiceForPartner = (Documents)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.oPurchaseInvoices);
                APInvoiceForPartner.CardCode = partner.Vendor;
                APInvoiceForPartner.DocDate = InternalConverters.EditTextToDateTime(EditText9.Value);
                APInvoiceForPartner.DocDueDate = InternalConverters.EditTextToDateTime(EditText10.Value);
                APInvoiceForPartner.TaxDate = InternalConverters.EditTextToDateTime(EditText11.Value);
                APInvoiceForPartner.SalesPersonCode = _opportunity.SalesPerson;
                APInvoiceForPartner.DocCurrency = _opportunity.UserFields.Fields.Item("U_CURRENCY").Value.ToString();
                APInvoiceForPartner.UserFields.Fields.Item("U_OppId").Value = _opportunityNumber;

                for (var i = 1; i <= Matrix0.RowCount; i++)
                {
                    APInvoiceForPartner.Lines.ItemCode = ((EditText)Matrix0.Columns.Item(1).Cells.Item(i).Specific).Value;
                    APInvoiceForPartner.Lines.Quantity = 1;

                    if (_opportunityType == 1 && CheckBox0.Checked)
                    {
                        APInvoiceForPartner.Lines.AccountCode = ((int)AccountCodes.insuranceBrokerage).ToString();
                    }
                    else if (_opportunityType == 2 && CheckBox0.Checked)
                    {
                        APInvoiceForPartner.Lines.AccountCode = ((int)AccountCodes.reInsuranceBrokerage).ToString();
                    }
                    else if (!CheckBox0.Checked)
                    {
                        APInvoiceForPartner.Lines.AccountCode = ((int)AccountCodes.clearingFirst).ToString();
                    }

                    APInvoiceForPartner.Lines.Price = (double)((InternalConverters.ConvertStringToDecimal(EditText12.Value) / partners.Sum(a => a.Rate)) * partner.Rate);
                    APInvoiceForPartner.Lines.CostingCode = "B2B";
                    APInvoiceForPartner.Lines.CostingCode2 = _distrule;
                    APInvoiceForPartner.Lines.Add();
                }


                APInvoiceForPartner.Add();

                SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
                if (errCode != 0) return new Result { Code = errCode, Message = errMSG };
                SapDiConnection.Instance.GetNewObjectCode(out newObjectCode);

                AddStageToOpportunity(newObjectCode, BoAPARDocumentTypes.bodt_PurchaseInvoice);
                SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
                if (errCode != 0) return new Result { Code = errCode, Message = errMSG };

                //ar for patner
                var ARInvoiceForPartner = (Documents)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.oInvoices);
                ARInvoiceForPartner.CardCode = partner.Customer;
                ARInvoiceForPartner.DocDate = InternalConverters.EditTextToDateTime(EditText9.Value);
                ARInvoiceForPartner.DocDueDate = InternalConverters.EditTextToDateTime(EditText10.Value);
                ARInvoiceForPartner.TaxDate = InternalConverters.EditTextToDateTime(EditText11.Value);
                ARInvoiceForPartner.SalesPersonCode = _opportunity.SalesPerson;
                ARInvoiceForPartner.DocCurrency = _opportunity.UserFields.Fields.Item("U_CURRENCY").Value.ToString();
                ARInvoiceForPartner.UserFields.Fields.Item("U_OppId").Value = _opportunityNumber;

                for (var i = 1; i <= Matrix0.RowCount; i++)
                {
                    ARInvoiceForPartner.Lines.ItemCode = ((EditText)Matrix0.Columns.Item(1).Cells.Item(i).Specific).Value;
                    ARInvoiceForPartner.Lines.Quantity = 1;

                    if (_opportunityType == 1 && CheckBox0.Checked)
                    {
                        ARInvoiceForPartner.Lines.AccountCode = ((int)AccountCodes.insuranceBrokerage).ToString();
                    }
                    else if (_opportunityType == 2 && CheckBox0.Checked)
                    {
                        ARInvoiceForPartner.Lines.AccountCode = ((int)AccountCodes.reInsuranceBrokerage).ToString();
                    }
                    else if (!CheckBox0.Checked)
                    {
                        ARInvoiceForPartner.Lines.AccountCode = ((int)AccountCodes.clearingFirst).ToString();
                    }

                    ARInvoiceForPartner.Lines.Price = (double)(((InternalConverters.ConvertStringToDecimal(EditText12.Value) / partners.Sum(a => a.Rate)) * partner.Rate) * (decimal)_opportunity.GrossProfit / 100);
                    ARInvoiceForPartner.Lines.CostingCode = "B2B";
                    ARInvoiceForPartner.Lines.CostingCode2 = _distrule;
                    ARInvoiceForPartner.Lines.Add();
                }


                ARInvoiceForPartner.Add();

                SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
                if (errCode != 0) return new Result { Code = errCode, Message = errMSG };
                SapDiConnection.Instance.GetNewObjectCode(out newObjectCode);

                AddStageToOpportunity(newObjectCode, BoAPARDocumentTypes.bodt_Invoice);

                SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
                if (errCode != 0) return new Result { Code = errCode, Message = errMSG };
            }

            return new Result { Code = errCode, Message = errMSG };
        }

        private Result CreateType2()
        {
            int errCode;
            string errMSG;
            var newObjectCode = "";
            var percent = OpportunityRepository.GetCompanyRevenuePercent(_opportunityNumber);
            var partners = OpportunityRepository.GetPartnerList(_opportunityNumber);
            if (partners.Count == 0) return new Result { Code = 10, Message = "Check Partners" };

            var ARInvoice = (Documents)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.oInvoices);
            ARInvoice.CardCode = EditText19.Value;
            ARInvoice.DocDate = InternalConverters.EditTextToDateTime(EditText9.Value);
            ARInvoice.DocDueDate = InternalConverters.EditTextToDateTime(EditText10.Value);
            ARInvoice.TaxDate = InternalConverters.EditTextToDateTime(EditText11.Value);
            ARInvoice.SalesPersonCode = _opportunity.SalesPerson;
            ARInvoice.DocCurrency = _opportunity.UserFields.Fields.Item("U_CURRENCY").Value.ToString();
            ARInvoice.UserFields.Fields.Item("U_OppId").Value = _opportunityNumber;

            for (var i = 1; i <= Matrix0.RowCount; i++)
            {
                ARInvoice.Lines.ItemCode = ((EditText)Matrix0.Columns.Item(1).Cells.Item(i).Specific).Value;
                ARInvoice.Lines.Quantity = 1;

                if (_opportunityType == 1 && CheckBox0.Checked)
                {
                    ARInvoice.Lines.AccountCode = ((int)AccountCodes.insuranceBrokerage).ToString();
                }
                else if (_opportunityType == 2 && CheckBox0.Checked)
                {
                    ARInvoice.Lines.AccountCode = ((int)AccountCodes.reInsuranceBrokerage).ToString();
                }
                else if (!CheckBox0.Checked)
                {
                    ARInvoice.Lines.AccountCode = ((int)AccountCodes.clearingFirst).ToString();
                }

                ARInvoice.Lines.Price = (double) InternalConverters.ConvertStringToDecimal(EditText12.Value);
                ARInvoice.Lines.CostingCode = "B2B";
                ARInvoice.Lines.CostingCode2 = _distrule;
                ARInvoice.Lines.Add();
            }

            ARInvoice.Add();

            SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
            if (errCode != 0) return new Result { Code = errCode, Message = errMSG };
            SapDiConnection.Instance.GetNewObjectCode(out newObjectCode);
            AddStageToOpportunity(newObjectCode, BoAPARDocumentTypes.bodt_Invoice);

            SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
            if (errCode != 0) return new Result { Code = errCode, Message = errMSG };

            //partners
            foreach (var partner in partners)
            {

                //ap for partner
                var APInvoiceForPartner = (Documents)SapDiConnection.Instance.GetBusinessObject(BoObjectTypes.oPurchaseInvoices);
                APInvoiceForPartner.CardCode = partner.Vendor;
                APInvoiceForPartner.DocDate = InternalConverters.EditTextToDateTime(EditText9.Value);
                APInvoiceForPartner.DocDueDate = InternalConverters.EditTextToDateTime(EditText10.Value);
                APInvoiceForPartner.TaxDate = InternalConverters.EditTextToDateTime(EditText11.Value);
                APInvoiceForPartner.SalesPersonCode = _opportunity.SalesPerson;
                APInvoiceForPartner.DocCurrency = _opportunity.UserFields.Fields.Item("U_CURRENCY").Value.ToString();
                APInvoiceForPartner.UserFields.Fields.Item("U_OppId").Value = _opportunityNumber;

                for (var i = 1; i <= Matrix0.RowCount; i++)
                {
                    APInvoiceForPartner.Lines.ItemCode = ((EditText)Matrix0.Columns.Item(1).Cells.Item(i).Specific).Value;
                    APInvoiceForPartner.Lines.Quantity = 1;

                    if (_opportunityType == 1 && CheckBox0.Checked)
                    {
                        APInvoiceForPartner.Lines.AccountCode = ((int)AccountCodes.insuranceBrokerage).ToString();
                    }
                    else if (_opportunityType == 2 && CheckBox0.Checked)
                    {
                        APInvoiceForPartner.Lines.AccountCode = ((int)AccountCodes.reInsuranceBrokerage).ToString();
                    }
                    else if (!CheckBox0.Checked)
                    {
                        APInvoiceForPartner.Lines.AccountCode = ((int)AccountCodes.clearingFirst).ToString();
                    }

                    APInvoiceForPartner.Lines.Price = (double) ((InternalConverters.ConvertStringToDecimal(EditText12.Value) / 100 ) * partner.Rate);
                    APInvoiceForPartner.Lines.CostingCode = "B2B";
                    APInvoiceForPartner.Lines.CostingCode2 = _distrule;
                    APInvoiceForPartner.Lines.Add();
                }


                APInvoiceForPartner.Add();
                SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
                if (errCode != 0) return new Result { Code = errCode, Message = errMSG };
                SapDiConnection.Instance.GetNewObjectCode(out newObjectCode);

                AddStageToOpportunity(newObjectCode, BoAPARDocumentTypes.bodt_PurchaseInvoice);

                SapDiConnection.Instance.GetLastError(out errCode, out errMSG);
                if (errCode != 0) return new Result { Code = errCode, Message = errMSG };
            
            }
            return new Result { Code = errCode, Message = errMSG };
        }

        private void AddStageToOpportunity(string newObjectCode, BoAPARDocumentTypes docType)
        {
            _opportunityLines.GetByKey(_opportunityNumber);
            _opportunityLines.Lines.Add();
            _opportunityLines.Lines.SetCurrentLine(_opportunityLines.Lines.Count - 1);
            _opportunityLines.Lines.StartDate = DateTime.Today;
            _opportunityLines.Lines.ClosingDate = DateTime.Today;
            _opportunityLines.Lines.SalesPerson = _opportunity.SalesPerson;
            _opportunityLines.Lines.StageKey = 4;
            _opportunityLines.Lines.DocumentType = docType;
            _opportunityLines.Lines.DocumentNumber = Convert.ToInt32(newObjectCode);
            _opportunityLines.Lines.DataOwnershipfield = _opportunity.DataOwnershipfield;
            _opportunityLines.Lines.MaxLocalTotal = _opportunity.MaxLocalTotal;
            _opportunityLines.Lines.UserFields.Fields.Item("U_PAYLIST").Value = EditText15.Value;
            _opportunityLines.Update();
        }

    }
}
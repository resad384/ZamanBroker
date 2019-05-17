using System;
using System.Collections.Generic;
using System.Text;
using SAPbouiCOM.Framework;
using Matrix = SAPbouiCOM.Matrix;

namespace BMSZamanBrokerAddOn
{
    class Menu
    {
        public void AddMenuItems()
        {
            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;

            oMenus = Application.SBO_Application.Menus;

            SAPbouiCOM.MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));
            oMenuItem = Application.SBO_Application.Menus.Item("43520"); // moudles'

            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
            oCreationPackage.UniqueID = "BMSZamanBrokerAddOn";
            oCreationPackage.String = "Zaman Broker";
            oCreationPackage.Enabled = true;
            oCreationPackage.Position = -1;

            oMenus = oMenuItem.SubMenus;

            try
            {
                //  If the manu already exists this code will fail
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception e)
            {

            }

            try
            {
                // Get the menu collection of the newly added pop-up item
                oMenuItem = Application.SBO_Application.Menus.Item("BMSZamanBrokerAddOn");
                oMenus = oMenuItem.SubMenus;

                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "BMSZamanBrokerAddOn.BTBPaymentForm";
                oCreationPackage.String = "Zmaan Broker";
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception er)
            { //  Menu already exists
                Application.SBO_Application.SetStatusBarMessage("Menu Already Exists", SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                //if (pVal.BeforeAction && pVal.MenuUID == "BTBPFItemsMatrixAddRow")
                //{
                //    SAPbouiCOM.Form oform;
                //    SAPbouiCOM.Matrix omatrix;
                //    oform = Application.SBO_Application.Forms.ActiveForm;
                //    omatrix = (Matrix) oform.Items.Item("Item_7").Specific;
                //    omatrix.AddRow(1);
                //}

                if (pVal.BeforeAction && pVal.MenuUID == "BTBPFItemsMatrixDelRow")
                {
                    SAPbouiCOM.Form oform;
                    SAPbouiCOM.Matrix omatrix;
                    oform = Application.SBO_Application.Forms.ActiveForm;
                    omatrix = (Matrix)oform.Items.Item("Item_7").Specific;
                    var nextSelectedRow =  omatrix.GetNextSelectedRow();
                    omatrix.DeleteRow(nextSelectedRow);
                }

            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }

    }
}

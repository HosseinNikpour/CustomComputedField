using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;

namespace CustomComputedField
{
    public class CustomComputedControl : TextField
    {
        protected override string DefaultTemplateName
        {
            get
            {
                if (this.ControlMode == SPControlMode.Display)
                {
                    return this.DisplayTemplateName;
                }
                else
                {
                    return "CustomComputedControl";
                }
            }
        }


        public override string DisplayTemplateName
        {
            get
            {
                return "CustomComputedControlForDisplay";
            }
            set
            {
                base.DisplayTemplateName = value;
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            EnsureChildControls();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }


        protected override void CreateChildControls()
        {
            if (this.Field != null)
            {
                // Make sure inherited child controls are completely rendered.
                base.CreateChildControls();

                // Associate child controls in the .ascx file with the 
                // fields allocated by this control.

                //this.
                //this.ISBNPrefix = (Label)TemplateContainer.FindControl("ISBNPrefix");
                //this.textBox = (TextBox)TemplateContainer.FindControl("TextField");
                //this.ISBNValueForDisplay = (Label)TemplateContainer.FindControl("ISBNValueForDisplay");

                //if (this.ControlMode != SPControlMode.Display)
                //{
                //    if (!this.Page.IsPostBack)
                //    {
                //        if (this.ControlMode == SPControlMode.New)
                //        {
                //            textBox.Text = "0-000-00000-0";

                //        } // end assign default value in New mode

                //    }// end if this is not a postback 

                //    // Do not reinitialize on a postback.

                //}// end if control mode is not Display
                //else // control mode is Display 
                //{
                //    // Assign current value from database to the label control
                //    ISBNValueForDisplay.Text = (String)this.ItemFieldValue;

                //}// end control mode is Display

            }// end if there is a non-null underlying ISBNField 

            // Do nothing if the ISBNField is null.
        }





        public override object Value
        {
            get
            {
                EnsureChildControls();
                return base.Value;
            }
            set
            {
                EnsureChildControls();



                base.Value = (String)value;
                // The value of the ISBNPrefix field is hardcoded in the
                // template, so it is not set here.
            }
        }


    }

}

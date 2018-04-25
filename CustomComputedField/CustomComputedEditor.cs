using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint.WebControls;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CustomComputedField
{
    public class CustomComputedEditor : UserControl, IFieldEditor
    {

        protected DropDownList ddlListNameQuery;
        protected TextBox txtQuery;
        protected DropDownList ddlAggregatorFunction;
        protected DropDownList ddlFieldNameQuery;
        protected Label lblSelectedListLookup;

        private string listNameValue = "";
        private string fieldNameValue = "";
        private string textQueryValue = "";
        private string aggregatorFunctionValue = "";

        public void InitializeWithField(SPField field)
        {
            CustomComputedField myField = field as CustomComputedField;
            if (myField != null)
            {
                this.listNameValue = myField.ListNameQuery;
                this.fieldNameValue = myField.FieldNameQuery;
                this.textQueryValue = myField.TextQuery;
                this.aggregatorFunctionValue = myField.AggregatorFunction;

                // ddlListNameQuery.SelectedValue = this.listNameValue;
                //ddlFieldNameQuery.SelectedValue = this.fieldNameValue;
            }

            //The field exists, so just show the list name and does not allow
            //the selection of another list

        }
        public void OnSaveChange(SPField field, bool isNew)
        {
            string listValue = this.ddlListNameQuery.SelectedValue;
            string fieldValue = this.ddlFieldNameQuery.SelectedItem.Text;
            string queryValue = this.txtQuery.Text;
            string aggreValue = this.ddlAggregatorFunction.SelectedValue;

            CustomComputedField myField = field as CustomComputedField;
            float sum = 0;
            //

            if (isNew)
            {
                myField.UpdateListNameQuery(listValue);
                myField.UpdateFieldNameQuery(fieldValue);
                myField.UpdateTextQuery(queryValue);
                myField.UpdateAggregatorFunction(aggreValue);
            }
            else
            {
                myField.ListNameQuery = listValue;
                myField.FieldNameQuery = fieldValue;
                myField.TextQuery = queryValue;
                myField.AggregatorFunction = aggreValue;
            }
            using (SPWeb web = SPContext.Current.Site.OpenWeb())
            {

                SPList list = web.Lists[new Guid(listValue)];
                SPQuery query = new SPQuery();
                query.Query = string.Format(queryValue);
                //query.ViewFields=string.Format("<ViewFields>"+
                //                        "<FieldRef Name={0}></FieldRef>"+
                //                    "</ViewFields>",fieldValue);
                SPListItemCollection col = list.GetItems(query);

                float[] numbers = new float[col.Count];
                foreach (SPListItem itm in col)
                {
                    int i = 0;
                    numbers[i++] = float.Parse(itm[fieldValue].ToString());

                }

                if (aggreValue == "Sum")
                    sum = numbers.Sum(x => x);
                else if (aggreValue == "Multi")
                    sum = numbers.Aggregate((a, b) => b * a);
                else if (aggreValue == "Avg")
                    sum = numbers.Average(x => x);
                else if (aggreValue == "Max")
                    sum = numbers.Max(x => x);
                else if (aggreValue == "Min")
                    sum = numbers.Min(x => x);


            }

            myField.DefaultValue = sum.ToString();


        }
        public bool DisplayAsNewSection
        {
            get
            {
                return true;
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // load values



            if (!IsPostBack)
            {

                ddlAggregatorFunction.Items.Clear();
                ddlAggregatorFunction.Items.Add("Sum");
                ddlAggregatorFunction.Items.Add("Avg");
                ddlAggregatorFunction.Items.Add("Multi");
                ddlAggregatorFunction.Items.Add("Min");
                ddlAggregatorFunction.Items.Add("Max");


                using (SPWeb Web = SPContext.Current.Site.OpenWeb())
                {

                    foreach (SPList List in Web.Lists)
                    {
                        if (!List.Hidden)
                            ddlListNameQuery.Items.Add(new ListItem(List.Title, List.ID.ToString()));
                    }



                    ListItem aggreitem = ddlAggregatorFunction.Items.FindByText(this.aggregatorFunctionValue);
                    if (aggreitem != null)
                        aggreitem.Selected = true;


                    if (this.textQueryValue != "")
                        txtQuery.Text = this.textQueryValue;

                    if (this.listNameValue != "")
                    {
                        ddlListNameQuery.SelectedValue = this.listNameValue;
                        lblSelectedListLookup.Text = ddlListNameQuery.SelectedItem.Text;
                        lblSelectedListLookup.Visible = true;
                        ddlListNameQuery.Visible = false;
                    }

                    if (ddlListNameQuery.SelectedIndex != -1)
                    {
                        SPList list = Web.Lists[new Guid(ddlListNameQuery.SelectedValue)];
                        BindDisplayColumns(list);
                        ListItem fieldItem = ddlFieldNameQuery.Items.FindByText(this.fieldNameValue);
                        if (fieldItem != null)
                            fieldItem.Selected = true;
                    }

                }
            }



        }


        protected void ddlListNameQuery_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList dropDownList = sender as DropDownList;

            if (dropDownList.SelectedItem != null)
            {
                using (SPWeb web = SPContext.Current.Site.OpenWeb())
                {
                    Guid listId = new Guid(dropDownList.SelectedItem.Value);
                    SPList list = web.Lists.GetList(listId, false);
                    BindDisplayColumns(list);

                }
                ddlFieldNameQuery.Enabled = true;
                //lblSelectedLookupList.Visible = false;

            }
            else
            {
                ddlFieldNameQuery.Enabled = false;
            }



        }


        private void BindDisplayColumns(SPList list)
        {

            ddlFieldNameQuery.Items.Clear();


            foreach (SPField field in list.Fields)
            {
                if (!field.FromBaseType || field.InternalName == "Title" || field.InternalName == "ID")
                {
                    ddlFieldNameQuery.Items.Add(new ListItem { Text = field.Title, Value = field.Id.ToString() });
                }
            }

        }
    }


 

}

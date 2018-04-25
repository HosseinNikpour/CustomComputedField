using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Security;
using System.Windows;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;


namespace CustomComputedField
{
    public class CustomComputedField : SPFieldText
    {
        public CustomComputedField(SPFieldCollection fields, string fieldName)
            : base(fields, fieldName)
        {
            this.Init();
        }
        public CustomComputedField(SPFieldCollection fields, string typeName, string displayName)
            : base(fields, typeName, displayName)
        {
            this.Init();
        }
        private void Init()
        {
            this.ListNameQuery = this.GetCustomProperty("ListNameQuery") + "";
            this.FieldNameQuery = this.GetCustomProperty("FieldNameQuery") + "";
            this.TextQuery = this.GetCustomProperty("TextQuery") + "";
            this.AggregatorFunction = this.GetCustomProperty("AggregatorFunction") + "";
        }
        private static Dictionary<int, string> updatedListNameQuery = new Dictionary<int, string>();
        private string listNameQuery;
        public string ListNameQuery
        {
            get
            {
                return updatedListNameQuery.ContainsKey(ContextId) ? updatedListNameQuery[ContextId] : listNameQuery;
            }
            set
            {
                this.listNameQuery = value;
            }
        }
        public void UpdateListNameQuery(string value)
        {
            updatedListNameQuery[ContextId] = value;
        }


        private static Dictionary<int, string> updatedFieldNameQuery = new Dictionary<int, string>();
        private string fieldNameQuery;
        public string FieldNameQuery
        {
            get
            {
                return updatedFieldNameQuery.ContainsKey(ContextId) ? updatedFieldNameQuery[ContextId] : fieldNameQuery;
            }
            set
            {
                this.fieldNameQuery = value;
            }
        }
        public void UpdateFieldNameQuery(string value)
        {
            updatedFieldNameQuery[ContextId] = value;
        }


        private static Dictionary<int, string> updatedTextQuery = new Dictionary<int, string>();
        private string textQuery;
        public string TextQuery
        {
            get
            {
                return updatedTextQuery.ContainsKey(ContextId) ? updatedTextQuery[ContextId] : textQuery;
            }
            set
            {
                this.textQuery = value;
            }
        }
        public void UpdateTextQuery(string value)
        {
            updatedTextQuery[ContextId] = value;
        }
        private static Dictionary<int, string> updatedAggregatorFunction = new Dictionary<int, string>();
        private string aggregatorFunction;
        public string AggregatorFunction
        {
            get
            {
                return updatedAggregatorFunction.ContainsKey(ContextId) ? updatedAggregatorFunction[ContextId] : aggregatorFunction;
            }
            set
            {
                this.aggregatorFunction = value;
            }
        }
        public void UpdateAggregatorFunction(string value)
        {
            updatedAggregatorFunction[ContextId] = value;
        }
        public int ContextId
        {
            get
            {
                return SPContext.Current.GetHashCode();
            }
        }
        public override void Update()
        {
            this.SetCustomProperty("ListNameQuery", this.ListNameQuery);
            this.SetCustomProperty("FieldNameQuery", this.FieldNameQuery);
            this.SetCustomProperty("TextQuery", this.TextQuery);
            this.SetCustomProperty("AggregatorFunction", this.AggregatorFunction);
            base.Update();
            if (updatedListNameQuery.ContainsKey(ContextId))
                updatedListNameQuery.Remove(ContextId);
            if (updatedFieldNameQuery.ContainsKey(ContextId))
                updatedFieldNameQuery.Remove(ContextId);
            if (updatedTextQuery.ContainsKey(ContextId))
                updatedTextQuery.Remove(ContextId);
            if (updatedAggregatorFunction.ContainsKey(ContextId))
                updatedAggregatorFunction.Remove(ContextId);

        }
        public override void OnAdded(SPAddFieldOptions op)
        {
            base.OnAdded(op);
            Update();
        }





    }


}

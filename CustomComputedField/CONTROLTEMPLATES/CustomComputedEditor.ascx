<%@ Control Language="C#" Inherits="CustomComputedField.CustomComputedEditor,CustomComputedField, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a562177864c784be"   AutoEventWireup="false" compilationMode="Always" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" src="~/_controltemplates/15/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" src="~/_controltemplates/15/InputFormSection.ascx" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
 <%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Import Namespace="Microsoft.SharePoint" %>
<script runat="server">

   
    
</script>

<wssuc:InputFormSection runat="server" id="MySections" Title="My Custom Section">
       <Template_InputFormControls>
             <wssuc:InputFormControl runat="server"
                    LabelText="Select List">
                    <Template_Control>
                           <asp:DropDownList id="ddlListNameQuery" runat="server" OnSelectedIndexChanged="ddlListNameQuery_SelectedIndexChanged" AutoPostBack="true">
                           </asp:DropDownList>
                         <asp:Label id="lblSelectedListLookup" runat="server" Visible="false"></asp:Label>
                    </Template_Control>


             </wssuc:InputFormControl>
          

             <wssuc:InputFormControl runat="server"
                    LabelText="Query">
                    <Template_Control>
                           <asp:TextBox  ID="txtQuery" TextMode="MultiLine" runat="server" ></asp:TextBox>
                    </Template_Control>


             </wssuc:InputFormControl>
             <wssuc:InputFormControl runat="server"
                    LabelText="Aggregate Function">
                    <Template_Control>
                           <asp:DropDownList id="ddlAggregatorFunction" runat="server">
                           </asp:DropDownList>
                    </Template_Control>


             </wssuc:InputFormControl>
            <wssuc:InputFormControl runat="server"
                    LabelText="Select Field for Aggregation">
                    <Template_Control>
                           <asp:DropDownList id="ddlFieldNameQuery" runat="server"  >
                           </asp:DropDownList>
                    </Template_Control>


             </wssuc:InputFormControl>
       </Template_InputFormControls>
</wssuc:InputFormSection>
 

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Security</h2>

    <ol>
        <li><%: Html.ActionLink("Agree on Definitions", "SecurityStep1") %></li>
        <li><%: Html.ActionLink("Identify Security Goals", "SecurityStep2") %></li>
        <li><%: Html.ActionLink("Develop Artifacts", "SecurityStep3") %></li>
        <li><%: Html.ActionLink("Risk Assessment", "SecurityStep4") %></li>
        <li><%: Html.ActionLink("Select Elicitation Technique", "SecurityStep5") %></li>
        <li><%: Html.ActionLink("Elicit Security Requirements", "SecurityStep6") %></li>
        <li><%: Html.ActionLink("Categorize Requirements", "SecurityStep7") %></li>
        <li><%: Html.ActionLink("Prioritize Requirements", "SecurityStep8") %></li>
        <li><%: Html.ActionLink("Requirements Inspection", "SecurityStep9") %></li>
    </ol>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

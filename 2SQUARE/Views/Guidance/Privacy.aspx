<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Privacy
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Privacy</h2>

    <ol>
        <li><%: Html.ActionLink("Agree on Definitions", "PrivacyStep1") %></li>
        <li><%: Html.ActionLink("Identify assets and privacy goals", "PrivacyStep2") %></li>
        <li><%: Html.ActionLink("Develop Artifacts", "PrivacyStep3") %></li>
        <li><%: Html.ActionLink("Risk Assessment", "PrivacyStep4") %></li>
        <li><%: Html.ActionLink("Select Elicitation Technique", "PrivacyStep5") %></li>
        <li><%: Html.ActionLink("Elicit Privacy Requirements", "PrivacyStep6") %></li>
        <li><%: Html.ActionLink("Categorize Requirements", "PrivacyStep7") %></li>
        <li><%: Html.ActionLink("Prioritize Requirements", "PrivacyStep8") %></li>
        <li><%: Html.ActionLink("Requirements Inspection", "PrivacyStep9") %></li>
    </ol>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

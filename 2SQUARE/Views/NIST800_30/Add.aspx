<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.NIST800_30EditViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NIST 800-30 Risk Assessment Module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Risk</h2>

    <% Html.RenderPartial("RiskForm"); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">



</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<NIST800_30Controller>(a=>a.Index(Model.ProjectStep.Id,Model.Project.Id), "NIST 800-30 Home", new {@class="button ui-state-default"}) %>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.PRAUCEditViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Privacy Risk Analysis for Ubiquitous Computing - Risk Assessment Module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Risk</h2>

    <% Html.RenderPartial("RiskForm");  %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<PRAUCController>(a=>a.Index(Model.ProjectStep.Id,Model.Project.id), "PRAUC Home", new {@class="button ui-state-default"}) %>

</asp:Content>

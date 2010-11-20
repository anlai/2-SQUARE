<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ProjectTermPredefinedTermsViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Step 1 - Predefined Terms
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Predefined Terms</h2>

    <% Html.RenderPartial("PredefinedTermsView"); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    <%--<%: Html.ActionLink<SecurityController>(a=>a.Step1(Model.Step.id, Model.Project.id), "Back To Step 1", new {@class="button ui-state-default"}) %>--%>
    <%: Html.ActionLink("Back to Step 1", "Step1", Model.Step.SquareType.Name, new {id=Model.Step.id, projectId=Model.Project.id}, new {@class="button ui-state-default"}) %>
</asp:Content>

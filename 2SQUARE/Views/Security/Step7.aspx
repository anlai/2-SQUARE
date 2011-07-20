<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step7ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 7 - Categorize Requirements
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("CategorizedRequirementsView"); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%= Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.Id), string.Format("{0} Home", Model.Project.Name), new {@class="button ui-state-default"}) %>

    <%= Html.ActionLink<CategoryController>(a=>a.Index(Model.ProjectStep.Id, Model.Project.Id), "Manage Categories", new {@class="button ui-state-default"}) %>

</asp:Content>

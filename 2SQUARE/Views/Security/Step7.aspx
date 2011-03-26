<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step7ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 7 - Categorize Requirements
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Security Step 7 - Categorize Requirements</h2>

    <h3>Categorized Requirements</h3>

    <h3>UnCategorized Requirements</h3>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%= Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), string.Format("{0} Home", Model.Project.Name), new {@class="button ui-state-default"}) %>

    <%= Html.ActionLink<CategoryController>(a=>a.Index(Model.ProjectStep.Id, Model.Project.id), "Manage Categories", new {@class="button ui-state-default"}) %>

</asp:Content>

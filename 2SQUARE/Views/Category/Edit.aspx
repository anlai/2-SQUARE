<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.CategoryEditViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Requirement Category
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Requirement Category</h2>

    <% Html.RenderPartial("CategoryForm"); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%= Html.ActionLink<CategoryController>(a=>a.Index(Model.ProjectStep.Id, Model.Project.Id), "Back to List", new {@class="button ui-state-default"}) %>

</asp:Content>

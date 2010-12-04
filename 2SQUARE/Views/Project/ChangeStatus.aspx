<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Project>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ChangeStatus
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Change Project Status</h2>

    <% foreach (var a in Model.ProjectSteps.Select(a => a.Step.SquareType).Distinct()) { %>
        <ul>
        <% foreach (var b in Model.ProjectSteps.Where(b => b.Step.SquareTypeId == a.id).OrderBy(b=>b.Step.Order)) { %>
            <li><%: b.Step.Name %></li>
        <% } %>
        </ul>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.id), Model.Name + " Home", new {@class="button ui-state-default"}) %>
</asp:Content>

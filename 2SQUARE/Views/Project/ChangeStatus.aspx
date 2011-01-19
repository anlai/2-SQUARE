<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ChangeStatusViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ChangeStatus
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Change Project Status</h2>

    <% foreach (var a in Model.Project.ProjectSteps.Select(a => a.Step.SquareType).Distinct()) { %>
        <ul class="editing-form">
        <% foreach (var b in Model.Project.ProjectSteps.Where(b => b.Step.SquareTypeId == a.id).OrderBy(b=>b.Step.Order)) { %>
            <li><strong><%= this.Select("Status").Options(Model.Status, x=>x.Key, x=>x.Value) %></strong>
                <%: b.Step.Name %></li>
        <% } %>
        </ul>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>
</asp:Content>

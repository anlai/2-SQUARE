<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ChangeStatusViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ChangeStatus
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Change Project Status</h2>

    <% foreach (var a in Model.Project.ProjectSteps.Select(a=>a.Step.SquareType).Distinct()) { %>
        <div class="squaretype-container">
        <strong class="header"><%: a.Name %></strong>
        <ul class="editing-form">
            <% foreach (var b in Model.ChangeStatusProjectSteps.Where(b => b.SquareTypeId == a.id).OrderBy(b => b.Order)) { %>
                <li>
                    <strong>
                        <%--<%= this.Select("Status").Options(Model.Status,x=>x.Key, x=>x.Value).Selected(b.CurrentStepStatus) %>--%>
                        <select class="status" data-id="<%: b.ProjectStepId %>" <%: b.CanEdit ? "disabled" : string.Empty %>>
                            <% foreach (var s in Model.Status) { %>
                                <option value="<%: s.Key %>" <%: (int)b.CurrentStepStatus == s.Key ? "selected" : string.Empty %>><%: s.Value %></option>
                            <% } %>
                        </select>
                    </strong>
                    <%: b.Name %>
                </li>
            <% } %>
        </ul>
        </div>
    <% } %>


    <div style="clear:both;">&nbsp;</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>
</asp:Content>

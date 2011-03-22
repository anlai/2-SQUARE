<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.GenericElicitationViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>
<%@ Import Namespace="_2SQUARE.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.ProjectStep.Step.SquareType.Name %> Step 6 - Elicit Requirements
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink<GenericElicitationController>(a => a.Add(Model.ProjectStep.Id, Model.Project.id), "Add Requirement", new { @class = "button ui-corner-all ui-state-default", style = "float:right; margin-top: 8px; margin-right: 5px; top: -1em;" })%>

    <h2><%: Model.ProjectStep.Step.SquareType.Name %> Step 6 - Elicit Requirements</h2>

    <% if (Model.Requirements.Count() == 0) { %>
        <% Html.RenderPartial("Message", new MessageModel("No requirements have been entered.")); %>
    <% } else { %>
    <table>
        <thead>
            <tr>
                <th></th>
                <th>Id</th>
                <th>Requirement</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var a in Model.Requirements) { %>
                <tr>
                    <td>
                        <%: Html.ActionLink<GenericElicitationController>(b=>b.Edit(Model.ProjectStep.Id, Model.Project.id, a.id), "Edit", new {@class="button ui-corner-all ui-state-default"}) %>
                        <%: Html.ActionLink<GenericElicitationController>(b=>b.Delete(Model.ProjectStep.Id, Model.Project.id, a.id), "Delete", new {@class="button ui-corner-all ui-state-default"}) %>
                    </td>
                    <td><%: a.RequirementId ?? a.id.ToString() %></td>
                    <td><%: a.Requirement1 %></td>
                </tr>
            <% } %>
        </tbody>
    </table>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>

</asp:Content>

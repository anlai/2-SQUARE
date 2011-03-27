<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step9ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.ProjectStep.Step.SquareType.Name %> Step 9 - Requirements Inspection
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Model.ProjectStep.Step.SquareType.Name %> Step 9 - Requirements Inspection</h2>

    <table>
        <thead>
            <tr>
                <th></th>
                <th>Id</th>
                <th>Requirement</th>
                <th>Category</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var a in Model.Requirements.OrderBy(b => b.Order)) { %>
                <tr>
                    <td style="width: 110px;">
                        <%: Html.ActionLink<RequirementDefectController>(b=>b.Create(Model.ProjectStep.Id, Model.Project.id, a.id), "Add Defect", new {@class="button ui-corner-all ui-state-default"}) %>
                    </td>
                    <td><%: a.RequirementId %></td>
                    <td><%: a.Requirement1 %></td>
                    <td><%: a.Category.Name %></td>
                </tr>
                <% if (a.RequirementDefects.Any()) { %>
                    <% foreach (var b in a.RequirementDefects) { %>
                        <tr>
                            <td></td>
                            <td colspan="2">Defect: <%: b.Description %></td>
                            <td>
                                <% if (!b.Solved) { %>
                                <%: Html.ActionLink<RequirementDefectController>(c=>c.Resolve(Model.ProjectStep.Id, Model.Project.id, a.id), "Resolve", new {@class="button ui-corner-all ui-state-default"}) %>
                                <% } %>
                            </td>
                        </tr>
                    <% } %>
                <% } %>
            <% } %>
        </tbody>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), string.Format("{0} Home", Model.Project.Name), new {@class="button ui-state-default"}) %>

</asp:Content>

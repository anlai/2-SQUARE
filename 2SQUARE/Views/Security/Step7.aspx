<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step7ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 7 - Categorize Requirements
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Security Step 7 - Categorize Requirements</h2>

    <h3>Categorized Requirements</h3>
    <table>
        <thead>
            <tr>
                <th></th>
                <th>Id</th>
                <th>Requirement</th>
                <th>Category</th>
                <th>Essential</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var a in Model.CategorizedRequirements) { %>
                <tr>
                    <td style="width: 105px;">
                        <%: Html.ActionLink<RequirementController>(b=>b.Categorize(Model.ProjectStep.Id, Model.Project.id, a.id), "Categorize", new {@class="button ui-state-default ui-corner-all"}) %>
                    </td>
                    <td><%: a.RequirementId %></td>
                    <td><%: a.Requirement1 %></td>
                    <td><%: a.Category.Name %></td>
                    <td><%: a.Essential %></td>
                </tr>
            <% } %>
        </tbody>
    </table>

    <h3>UnCategorized Requirements</h3>
    <table>
        <thead>
            <tr>
                <th></th>
                <th>Id</th>
                <th>Requirement</th>
                <th>Essential</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var a in Model.UncategorizedRequirements) { %>
                <tr>
                    <td style="width: 105px;">
                        <%: Html.ActionLink<RequirementController>(b=>b.Categorize(Model.ProjectStep.Id, Model.Project.id, a.id), "Categorize", new {@class="button ui-state-default ui-corner-all"}) %>
                    </td>
                    <td><%: a.RequirementId %></td>
                    <td><%: a.Requirement1 %></td>
                    <td><%: a.Essential %></td>
                </tr>
            <% } %>
        </tbody>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%= Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), string.Format("{0} Home", Model.Project.Name), new {@class="button ui-state-default"}) %>

    <%= Html.ActionLink<CategoryController>(a=>a.Index(Model.ProjectStep.Id, Model.Project.id), "Manage Categories", new {@class="button ui-state-default"}) %>

</asp:Content>

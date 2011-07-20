<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.Step7ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

    <h2><%: Model.ProjectStep.Step.SquareType.Name %> Step 7 - Categorize Requirements</h2>

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
                        <%: Html.ActionLink<RequirementController>(b=>b.Categorize(Model.ProjectStep.Id, Model.Project.Id, a.Id), "Categorize", new {@class="button ui-state-default ui-corner-all"}) %>
                    </td>
                    <td><%: a.RequirementId %></td>
                    <td><%: a.RequirementText %></td>
                    <td><%: a.Category.Name %></td>
                    <td><%: a.Essential %></td>
                </tr>
            <% } %>
        </tbody>
    </table>

    <h3>Uncategorized Requirements</h3>
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
                        <%: Html.ActionLink<RequirementController>(b=>b.Categorize(Model.ProjectStep.Id, Model.Project.Id, a.Id), "Categorize", new {@class="button ui-state-default ui-corner-all"}) %>
                    </td>
                    <td><%: a.RequirementId %></td>
                    <td><%: a.RequirementText %></td>
                    <td><%: a.Essential %></td>
                </tr>
            <% } %>
        </tbody>
    </table>
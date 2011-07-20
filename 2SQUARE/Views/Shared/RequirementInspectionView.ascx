<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.Step9ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

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
                        <%: Html.ActionLink<RequirementDefectController>(b=>b.Create(Model.ProjectStep.Id, Model.Project.Id, a.Id), "Add Defect", new {@class="button ui-corner-all ui-state-default"}) %>
                    </td>
                    <td><%: a.RequirementId %></td>
                    <td><%: a.RequirementText %></td>
                    <td><%: a.Category.Name %></td>
                </tr>
                <% if (a.RequirementDefects.Any()) { %>
                    <% foreach (var b in a.RequirementDefects) { %>
                        <tr style="background-color: #E6DEDE;">
                            <td></td>
                            <td colspan="2"><strong>Defect:</strong> <%: b.Description %></td>
                            <td>
                                <% if (!b.Solved) { %>
                                    <% using (Html.BeginForm("Resolve", "RequirementDefect", new { id = Model.ProjectStep.Id, projectId = Model.Project.Id, defectId = b.Id }, FormMethod.Post, new { style = "display:inline-block;" })) { %>
                                        <%: Html.SubmitButton("Resolve", "Resolve", new {@class="button ui-corner-all ui-state-default"}) %>
                                    <% } %>
                                <% } else { %>
                                    <strong>Resolved</strong>
                                <% } %>
                            </td>
                        </tr>
                    <% } %>
                <% } %>
            <% } %>
        </tbody>
    </table>

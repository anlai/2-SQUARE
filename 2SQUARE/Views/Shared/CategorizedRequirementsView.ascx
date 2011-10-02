<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.Step7ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

    <div class="section-container">

        <div class="section-header">
        
            <div><h2><%: Model.ProjectStep.Step.SquareType.Name %> Step 7 - Categorize Requirements</h2></div>

        </div>    

        <div class="section-contents">
        
        </div>

    </div>

    

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1"><h3>Categorized Requirements</h3></div>
            <div class="col2"></div>

        </div>    

        <div class="section-contents">
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
        </div>

    </div>

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1"><h3>Uncategorized Requirements</h3></div>
            <div class="col2"></div>

        </div>    

        <div class="section-contents">
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
        </div>

    </div>

<% Html.RenderPartial("_ProjectStepCollaboration", Model.ProjectStep); %>
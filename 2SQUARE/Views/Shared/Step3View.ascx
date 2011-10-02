<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.Step3ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<div class="section-container">

    <div class="section-header">
        
        <div class="col1"><h2><%: Model.ProjectStep.Step.SquareType.Name %> Step 3 - Develop Artifacts</h2></div>
        <div class="col2"><%: Html.ActionLink<ArtifactController>(a=>a.Add(Model.ProjectStep.Id), "Add Artifact", new {@class="button ui-corner-all ui-state-default", style="float:right;"}) %></div>

    </div>    

    <div class="section-contents">
        <% if (Model.Artifacts.Count == 0)
           { %>
            <div class="ui-widget messages">
                <div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; margin-bottom: 10px; padding: 0pt 0.7em;">
                    <p>
                        <span class="ui-icon ui-icon-info" style="float:left; margin-right: 0.3em;"></span>
                        <span >No artifacts have been entered.</span>
                    </p>
                </div>
            </div>
        <% } else { %>
        <table cellpadding="5px">
            <thead>
                <tr>
                    <th></th>
                    <th>Name</th>
                    <th>Artifact Type</th>
                    <th>Created By</th>
                    <th>Date Created</th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var a in Model.Artifacts)
                   { %>
                    <tr class="definition-row">
                        <td class="button-cell" style="width: 215px;">
                            <%: Html.ActionLink<ArtifactController>(b=>b.Details(Model.ProjectStep.Id, a.Id), "Details", new {@class="button ui-state-default ui-corner-all"}) %>
                            <%: Html.ActionLink<ArtifactController>(b=>b.Edit(Model.ProjectStep.Id, a.Id), "Edit", new {@class="button ui-state-default ui-corner-all"}) %>
                            <%: Html.ActionLink<ArtifactController>(b=>b.Delete(Model.ProjectStep.Id, a.Id), "Delete", new {@class="button ui-state-default ui-corner-all"}) %>
                        </td>
                        <td><%: a.Name%></td>
                        <td><%: a.ArtifactType.Name%></td>
                        <td><%: a.CreatedBy %></td>
                        <td><%: a.DateCreated %></td>
                    </tr>
                <% } %>
            </tbody>
        </table>
        <% } %>        
    </div>

</div>

    

<% Html.RenderPartial("_ProjectStepCollaboration", Model.ProjectStep); %>
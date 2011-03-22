﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.RequirementViewModel>" %>

    <%: Html.ValidationSummary("Please correct the following errors.") %>

    <% using (Html.BeginForm()) { %>
        <ul class="entry-form">
            <li><strong>Requirement Id:</strong>
                <%: Html.TextBoxFor(a=>a.Requirement.RequirementId) %>
                <em>*Leaving blank will be filled with auto generated id.</em>
            </li>
            <li><strong>Name:</strong>
                <%: Html.TextBoxFor(a=>a.Requirement.Name) %>
            </li>
            <li><strong>Requirement:</strong>
                <%: Html.TextAreaFor(a=>a.Requirement.Requirement1) %>
            </li>
            <li><strong></strong>
                <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
                <%: Html.ActionLink("Cancel", "Step6", Model.ProjectStep.Step.SquareType.Name, new {id=Model.ProjectStep.Step.id, projectId=Model.Project.id}, new {@class="button ui-state-default ui-corner-all"}) %>
            </li>
        </ul>
    <% } %>
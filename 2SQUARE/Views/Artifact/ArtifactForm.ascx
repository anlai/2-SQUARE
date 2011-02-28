﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.ArtifactViewModel>" %>

    <%: Html.ValidationSummary() %>

    <ul class="entry-form">
        <li><strong>Name:</strong>
            <%: Html.TextBoxFor(a=>a.Artifact.Name) %>
        </li>
        <li><strong>Description:</strong>
            <%: Html.TextAreaFor(a=>a.Artifact.Description) %>
        </li>
        <li><strong>Artifact Type:</strong>
            <%= this.Select("Artifact.ArtifactTypeId").Options(Model.ArtifactTypes,x=>x.id, x=>x.Name).Selected(Model.Artifact != null ? Model.Artifact.ArtifactTypeId.ToString() : string.Empty) %>
        </li>
        <li><strong>File:</strong>
            <input type='file' name='file' id='file' />
        </li>
        <li><strong></strong>
            <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
            <%: Html.ActionLink("Cancel", Model.ProjectStep.Step.Action, Model.ProjectStep.Step.Controller, new {id=Model.ProjectStep.Id, projectId=Model.ProjectStep.ProjectId}, new {@class="button ui-corner-all ui-state-default"}) %>
        </li>
    </ul>
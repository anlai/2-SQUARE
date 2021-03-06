﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.RequirementDefectViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Requirement Defect
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create Requirement Defect</h2>

    

    <% using (Html.BeginForm()) { %>
    
        <%: Html.ValidationMessage("") %>
    
        <ul class="entry-form">
            <li><strong>Requirement Id:</strong>
                <%: Model.Requirement.RequirementId %>
            </li>
            <li><strong>Requirement:</strong>
                <%: Model.Requirement.RequirementText %>
            </li>
            <li><strong>Defect:</strong>
                <%: Html.TextArea("defect") %>
            </li>
            <li><strong></strong>
                <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
                <%: Html.ActionLink("Cancel", Model.ProjectStep.Step.Action, Model.ProjectStep.Step.Controller, new {id=Model.ProjectStep.Id, projectId=Model.Project.Id}, new {@class="button ui-state-default ui-corner-all"}) %>
            </li>
        </ul>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink("Back to Step 9", Model.ProjectStep.Step.Action, Model.ProjectStep.Step.Controller, new {id=Model.ProjectStep.Id, projectId=Model.Project.Id}, new {@class="button ui-state-default"}) %>

</asp:Content>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.GoalViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<% Html.EnableClientValidation(); %>

<% using (Html.BeginForm()) { %>

<%: Html.ValidationSummary() %>

<ul class="entry-form">
        
    <li><strong>Goal:</strong>
        <%: Html.TextAreaFor(a=>a.Goal.Description) %>
    </li>
    <li><strong>Goal Type:</strong>
        <%= this.Select("goalTypeId").Options(Model.GoalTypes, x => x.Id, x => x.Name).Selected(Model.Goal.GoalType != null ? Model.Goal.GoalType.Id : string.Empty)%>
    </li>
    <li><strong></strong>
        <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
        <%--<%: Html.ActionLink<SecurityController>(a=>a.Step2(Model.ProjectStep.Id, Model.ProjectStep.Project.Id), "Cancel", new {@class="button ui-corner-all ui-state-default"}) %>--%>
        <%: Html.ActionLink("Cancel", Model.ProjectStep.Step.Action, Model.ProjectStep.Step.Controller, new {id=Model.ProjectStep.Id, projectId=Model.ProjectStep.Project.Id}, new {@class="button"}) %>
    </li>
</ul>

<% } %>
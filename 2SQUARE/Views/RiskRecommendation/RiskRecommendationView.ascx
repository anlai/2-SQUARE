<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.RiskRecommendationViewModel>" %>
<%@ Import Namespace="_2SQUARE.Models" %>

    <% Html.RenderPartial("Message", new MessageModel(Model.Risk.Name)); %>

    <% using (Html.BeginForm()) { %>

    <%: Html.ValidationSummary() %>

    <ul class="entry-form">
        <li><strong>Recommendated Controls:</strong>
            <%: Html.TextAreaFor(a=>a.RiskRecommendation.Controls) %>
        </li>
        <li><strong>Impact:</strong>
            <%: Html.TextAreaFor(a=>a.RiskRecommendation.Impact) %>
        </li>
        <li><strong>Feasibility:</strong>
            <%: Html.TextAreaFor(a=>a.RiskRecommendation.Feasibility) %>
        </li>
        <li><strong></strong>
            <%: Html.SubmitButton("Save", "Save", new { @class = "button ui-corner-all ui-state-default" })%>
            <%: Html.ActionLink("Cancel", "Index", Model.Risk.AssessmentType.Controller, new {id=Model.ProjectStepId, projectId=Model.Risk.Project.Id}, new {@class="button ui-state-default ui-corner-all"}) %>
        </li>
    </ul>
    <% } %>
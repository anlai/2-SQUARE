<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.AddGoalViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

    <% using (Html.BeginForm()) { %>

    <ul class="entry-form">
        <%: Html.HiddenFor(a=>a.Goal.ProjectId) %>
        <%: Html.HiddenFor(a=>a.Goal.SquareTypeId) %>
        <li><strong>Goal:</strong>
            <%: Html.TextAreaFor(a=>a.Goal.Description) %>
        </li>
        <li><strong>Goal Type:</strong>
            <%= this.Select("Goal.GoalTypeId").Options(Model.GoalTypes, x=>x.id, x=>x.Name).Selected(Model.Goal != null ? Model.Goal.GoalTypeId : string.Empty) %>
        </li>
        <li><strong></strong>
            <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
            <%: Html.ActionLink<SecurityController>(a=>a.Step2(Model.ProjectStep.Id, Model.ProjectStep.ProjectId), "Cancel", new {@class="button ui-corner-all ui-state-default"}) %>
        </li>
    </ul>
    <% } %>
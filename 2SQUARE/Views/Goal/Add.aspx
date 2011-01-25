<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.AddGoalViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add <%: Model.ProjectStep.Step.SquareType.Name %> Goal
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add <%: Model.ProjectStep.Step.SquareType.Name %> Goal</h2>

    <% using (Html.BeginForm()) { %>

    <ul class="entry-form">
        <%: Html.HiddenFor(a=>a.Goal.ProjectId) %>
        <%: Html.HiddenFor(a=>a.Goal.SquareTypeId) %>
        <li><strong>Goal:</strong>
            <%: Html.TextAreaFor(a=>a.Goal.Description) %>
        </li>
        <li><strong>Goal Type:</strong>
            <%= this.Select("Goal.GoalTypeId").Options(Model.GoalTypes, x=>x.id, x=>x.Name) %>
        </li>
        <li><strong></strong>
            <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
        </li>
    </ul>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink("Back to Step 2", "Step2", Model.ProjectStep.Step.SquareType.Name, new {id=Model.ProjectStep.Id, projectId=Model.ProjectStep.ProjectId}, new {@class="button ui-state-default"}) %>

</asp:Content>

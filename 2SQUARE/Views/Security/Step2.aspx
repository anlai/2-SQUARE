<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step2ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 2 - Identify Security Goals
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Security Step 2 - Identify Security Goals</h2>

    <h3>Business Goal</h3>

    <div id="business-goal-container">
    <%: Html.TextArea("BusinessGoal", Model.BusinessGoal != null ? Model.BusinessGoal.Description : string.Empty, new {style="width: 80%; height: 60px;"}) %>
    <br />
    <%: Html.SubmitButton("SaveBusinessGoal", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
    </div>

    <%: Html.ActionLink<GoalController>(a=>a.Add(Model.ProjectStep.Id), "Add Security Goal", new {@class="button ui-corner-all ui-state-default", style="float:right; margin-top: 8px;"}) %>

    <h3>Security Goals</h3>
    <table cellpadding="5px">
        <thead>
            <tr>
                <th></th>
                <th>Security Goal</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var a in Model.SecurityGoals) { %>
                <tr class="definition-row">
                    <td class="button-cell"></td>
                    <td><%: a.Description %></td>
                </tr>
            <% } %>
        </tbody>
    </table>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

    <style type="text/css">
        #business-goal-container
        {
            padding: 1em;
        }
    </style>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>

</asp:Content>

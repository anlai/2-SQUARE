<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step2ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 2 - Identify Security Goals
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div ><h2>Security Step 2 - Identify Security Goals</h2></div>

        </div>    

        <div class="section-contents">
        
        </div>

    </div>

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1"><h3>Business Goal</h3></div>
            <div class="col2"></div>

        </div>    

        <div class="section-contents">
            <% using (Html.BeginForm("SaveBusinessGoal", "Goal", FormMethod.Post)) { %>
                <%: Html.Hidden("Id", Model.ProjectStep.Id) %>
                <div id="business-goal-container">
                <%: Html.TextArea("BusinessGoal", Model.BusinessGoal != null ? Model.BusinessGoal.Description : string.Empty, new {style="width: 80%; height: 60px;"}) %>
                <br />
                <%: Html.SubmitButton("SaveBusinessGoal", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
                </div>
            <% } %>
        </div>

    </div>

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1"><h3>Security Goals</h3></div>
            <div class="col2"><%: Html.ActionLink<GoalController>(a=>a.Add(Model.ProjectStep.Id), "Add Security Goal", new {@class="button"}) %></div>

        </div>    

        <div class="section-contents">
            <% if (Model.SecurityGoals.Count == 0) { %>
                <div class="ui-widget messages">
                    <div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; margin-bottom: 10px; padding: 0pt 0.7em;">
                        <p>
                            <span class="ui-icon ui-icon-info" style="float:left; margin-right: 0.3em;"></span>
                            <span >No security goals have been entered.</span>
                        </p>
                    </div>
                </div>
            <% } else { %>
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
                                <td class="button-cell" style="width: 135px;">
                                    <%: Html.ActionLink<GoalController>(b=>b.Edit(Model.ProjectStep.Id, a.Id), "Edit", new {@class="button ui-state-default ui-corner-all"}) %>
                                    <%: Html.ActionLink<GoalController>(b=>b.Delete(Model.ProjectStep.Id, a.Id), "Delete", new {@class="button ui-state-default ui-corner-all"}) %>
                                </td>
                                <td style="width: 700px;"><%: a.Description %></td>
                            </tr>
                        <% } %>
                    </tbody>
                </table>
            <% } %>
        </div>
    </div>

    <div class="pstep-container">
    <% Html.RenderPartial("_ProjectStepNotes", Model.ProjectStep); %>

    <% Html.RenderPartial("_ProjectStepFile", Model.ProjectStep); %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.Id), Model.Project.Name + " Home", new {@class="nav-button"}) %>

</asp:Content>

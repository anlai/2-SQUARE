<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ProjectDetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Model.Project.Name %></h2>

    <div id="description">
        <%: Model.Project.Description %>
    </div>

    <h2>SQUARE Steps</h2>

    <% foreach (var a in Model.SquareTypes) { %>

        <div class="steps-container">
            
            <div class="ui-widget-header box-header ui-corner-all">
                <%: a.Name %>
            </div>

            <% foreach (var b in Model.Project.ProjectSteps.Where(c => c.Step.SquareType.id == a.id).OrderBy(c => c.Step.Order)) { %>            
                
                <a href="<%: Url.Action(b.Step.Action, b.Step.Controller, new {id=b.ProjectId, stepId=b.StepId}) %>">

                <div class="step button ui-corner-all">

                <% if (!b.DateStarted.HasValue) { %>
                <span class="icon ui-icon ui-icon-circle-minus"></span>
                <% } else if (b.Complete) { %>
                    <span class="icon ui-icon ui-icon-check"></span>
                <% } else { %>
                    <span class="icon ui-icon ui-icon-play"></span>
                <% } %>
                
                <span>
                    <ul>
                        <li><%: b.Step.Name %></li>
                        <li><strong>Date Started:</strong><%: b.DateStarted.HasValue ? b.DateStarted.Value.ToString("d") : "n/a" %></li>
                        <li><strong>Date Completed:</strong><%: b.DateCompleted.HasValue ? b.DateCompleted.Value.ToString("d") : "n/a" %></li>
                    </ul>
                </span>
                </div>

                </a>
            <% } %>

        </div>

    <% } %>
    


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

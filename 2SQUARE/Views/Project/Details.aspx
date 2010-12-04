<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ProjectDetailsViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Project Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Model.Project.Name %></h2>

    <div id="description">
        <%: Model.Project.Description %>
    </div>

    <%: Html.ActionLink<ProjectController>(a => a.ChangeStatus(Model.Project.id), "Change Status", new { @class = "button ui-state-default ui-corner-all", style = "float:right; margin-top: 8px; margin-right: 5px;" })%>

    <h3>SQUARE Steps</h3>

    <% foreach (var a in Model.SquareTypes) { %>

        <div class="steps-container">
            
            <div class="ui-widget-header box-header ui-corner-all">
                <%: a.Name %>
            </div>

            <% foreach (var b in Model.Project.ProjectSteps.Where(c => c.Step.SquareType.id == a.id).OrderBy(c => c.Step.Order)) { %>            
                
                <a href="<%: Url.Action(b.Step.Action, b.Step.Controller, new {id=b.StepId, projectId=b.ProjectId}) %>">

                <div class="step button ui-corner-all">

                <% if (!b.DateStarted.HasValue) { %>
                <span class="icon ui-icon ui-icon-circle-minus"></span>
                <% } else if (b.Complete) { %>
                    <span class="icon ui-icon ui-icon-check"></span>
                <% } else { %>
                    <span class="icon ui-icon ui-icon-play"></span>
                <% } %>
                
                <span class="step-text">
                    <%: b.Step.Name %>
                </span>
                </div>

                </a>
            <% } %>

        </div>

    <% } %>
    
    <div style="clear:both;"></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

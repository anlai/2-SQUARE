<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ProjectDetailsViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Project Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">
    
        <div class="section-header">
            <div class="col1"><h2><%: Model.Project.Name %></h2>    </div>
            <div class="col2">
                <%: Html.ActionLink("Edit Project", "Edit", new {id=Model.Project.Id}, new {@class="button"})%>
                <%: Html.ActionLink("Permissions", "Permissions", new {id=Model.Project.Id}, new {@class="button"})%>
            </div>
        </div>

        <div class="section-contents">
            <%: Model.Project.Description %>
        </div>

    </div>
    
    <div class="section-container">
    
        <div class="section-header">
        
            <div class="col1"><h3>SQUARE Steps</h3></div>
            <div class="col2">
                <%: Html.ActionLink<ProjectController>(a => a.ChangeStatus(Model.Project.Id), "Change Status", new { @class = "button"})%>
                <%: Html.ActionLink("Get Report", "GenerateReport", "Report", new {id=Model.Project.Id}, new {@class="button"})%>
            </div>
        </div>

        <div class="section-contents">
        
            <% foreach (var a in Model.SquareTypes) { %>

                <div class="steps-container">
            
                    <div class="ui-widget-header box-header ui-corner-all">
                        <%: a.Name %>
                    </div>

                    <% foreach (var b in Model.Project.ProjectSteps.Where(c => c.Step.SquareType.Id == a.Id).OrderBy(c => c.Step.Order)) { %>            
                
                        <a href="<%: Url.Action(b.Step.Action, b.Step.Controller, new {id=b.Id, projectId=b.Project.Id}) %>">

                            <% if (!b.DateStarted.HasValue) { %>
                                <span class="button pending" style="width: 270px; margin-top: .5em; text-align: left;">
                                <span class="icon ui-icon ui-icon-circle-minus" style="display: inline-block;"></span>
                            <% } else if (b.Complete) { %>
                                <span class="button complete" style="width: 270px; margin-top: .5em; text-align: left;">
                                <span class="icon ui-icon ui-icon-check" style="display: inline-block;"></span>
                            <% } else { %>
                                <span class="button working" style="width: 270px; margin-top: .5em; text-align: left;">
                                <span class="icon ui-icon ui-icon-play" style="display: inline-block;"></span>
                            <% } %>
                
                                <%: b.Step.Name %>
                            </span>
                        </a>
                    <% } %>

                </div>

            <% } %>

            <div style="clear:both;"></div>

        </div>

    </div>

    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.RiskAssessmentViewModel>" %>
<%@ Import Namespace="_2SQUARE.Models" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>
<%@ Import Namespace="_2SQUARE.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Privacy Risk Analysis for Ubiquitous Computing - Risk Assessment Module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink<PRAUCController>(a=>a.Add(Model.ProjectStep.Id, Model.Project.id), "Add Risk", new {@class="button ui-corner-all ui-state-default", style="float:right;"}) %>

    <h2>Identified Risks</h2>

    <% if (!Model.Risks.Any()) { %>
        <% Html.RenderPartial("Message", new MessageModel("No risks have been identified yet")); %>
    <% } else { %>
    
        <% foreach (var a in Model.Risks) { %>
        
            <fieldset>
            <div class="risk">
        
                <%: Html.ActionLink<PRAUCController>(b=>b.Edit(Model.ProjectStep.Id, Model.Project.id, a.id), "Edit", new {@class="button ui-corner-all ui-state-default", style="float:right; top: -1.5em;"}) %>

                <% if (a.RiskRecommendations.Any()) { %>
                    <%: Html.ActionLink<RiskRecommendationController>(b => b.Edit(a.RiskRecommendations.First().id, Model.ProjectStep.Id), "Edit Recommendation", new { @class = "button ui-corner-all ui-state-default", style = "float:right; top: -1.5em; margin-right: 5px;" })%>
                <% } else { %>
                    <%: Html.ActionLink<RiskRecommendationController>(b => b.Create(a.id, Model.ProjectStep.Id), "Create Recommendation", new { @class = "button ui-corner-all ui-state-default", style = "float:right; top: -1.5em; margin-right: 5px;" })%>
                <% } %>

                <ul class="entry-form">
                <li style="margin-top: 1em;"><strong>Description:</strong>
                    <div class="threat_source"><%: a.Description %></div>
                </li>

                <li style="margin-top: 1em;"><strong>Likelihood:</strong>
                    <%: a.Likelihood.Name %>
                </li>
                <li><strong>Damage:</strong>
                    <%: a.Damage.Name %>
                </li>
                <li><strong>Cost:</strong>
                    <%: a.Cost %>
                </li>
                <li><strong>Implement Protection:</strong>
                    <% if (a.RiskLevelId == ((char)RiskLevelsEnum.High).ToString()) { %>
                        Recommended
                    <% } else { %>
                        Not Necessary, Cost does not outweight Likelihood and Damage
                    <% } %>
                </li>

                
                <% if (!a.RiskRecommendations.Any()) { %>
                    <% Html.RenderPartial("Message", new MessageModel("No Risk Recommendation has been created for this risk.", true)); %>
                <% } else {
                       var rec = a.RiskRecommendations.First();
                       %>
                
                    <li><h4>Recommendation</h4></li>

                    <!-- Display out the risk recommendation information -->
                    <li style="margin-top: 1em;"><strong>Controls:</strong>
                        <%: rec.Controls %>
                    </li>

                    <li style="margin-top: 1em;"><strong>Impact:</strong>
                        <%: rec.Impact %>
                    </li>

                    <li style="margin-top: 1em;"><strong>Feasibility:</strong>
                        <%: rec.Feasibility %>
                    </li>

                <% } %>
                
                
                </ul>



            </div>
            </fieldset>
        <% } %>
        
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <style type="text/css">
        .risk { margin-top: 1.75em; margin-left: -2em; }
        fieldset { margin-bottom: 1em; }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

<%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>

</asp:Content>

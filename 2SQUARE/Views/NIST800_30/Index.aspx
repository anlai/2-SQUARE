<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.RiskAssessmentViewModel>" %>
<%@ Import Namespace="_2SQUARE.Models" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NIST 800-30 Risk Assessment Module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1"><h2>Identified Risks</h2></div>
            <div class="col2"><%: Html.ActionLink<NIST800_30Controller>(a=>a.Add(Model.ProjectStep.Id, Model.Project.Id), "Add Risk", new {@class="button"}) %></div>

        </div>    

        <div class="section-contents">
            <% if (!Model.Risks.Any()) { %>
                <% Html.RenderPartial("Message", new MessageModel("No risks have been identified yet")); %>
            <% } else { %>
    
                <% foreach (var a in Model.Risks) { %>
        
                    <div class="risk">
        
                        <%: Html.ActionLink<NIST800_30Controller>(b=>b.Edit(Model.ProjectStep.Id, Model.Project.Id, a.Id), "Edit", new {@class="button", style="float:right; top: -.5em;"}) %>

                        <% if (a.RiskRecommendations.Any()) { %>
                            <%: Html.ActionLink<RiskRecommendationController>(b => b.Edit(a.RiskRecommendations.First().Id, Model.ProjectStep.Id), "Edit Recommendation", new { @class = "button", style = "float:right; top: -.5em; margin-right: 5px;" })%>
                        <% } else { %>
                            <%: Html.ActionLink<RiskRecommendationController>(b => b.Create(a.Id, Model.ProjectStep.Id), "Create Recommendation", new { @class = "button", style = "float:right; top: -.5em; margin-right: 5px;" })%>
                        <% } %>

                        <h3><%: a.Name %></h3>

                        <ul class="entry-form">
                        <li style="margin-top: 1em;"><strong>Threat Source:</strong>
                            <div class="threat_source"><%: a.Source %></div>
                        </li>

                        <li style="margin-top: 1em;"><strong>Vulnerability:</strong>
                            <div class="vulnerability"><%: a.Vulnerability %></div>
                        </li>

                        <li style="margin-top: 1em;"><strong>Likelihood:</strong>
                            <%: a.Likelihood.Name %>
                        </li>
                        <li><strong>Impact:</strong>
                            <%: a.Impact.Name %>
                        </li>
                        <li><strong>Impact Magnitude:</strong>
                            <%: a.Magnitude.Name %>
                        </li>
                        <li><strong>Risk Level</strong> 
                            <%: a.RiskLevel.Name %>
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
        
                <% } %>
        
            <% } %>        
        </div>

    </div>

    

    



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <style type="text/css">
        .risk { margin-top: 3em; }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

<%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.Id), Model.Project.Name + " Home", new {@class="nav-button"}) %>

</asp:Content>

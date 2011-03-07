﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.NIST800_30ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Models" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NIST 800-30 Risk Assessment Module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink<NIST800_30Controller>(a=>a.Add(Model.ProjectStep.Id, Model.Project.id), "Add Risk", new {@class="button ui-corner-all ui-state-default", style="float:right;"}) %>

    <h2>Identified Risks</h2>

    <% if (!Model.Risks.Any()) { %>
        <% Html.RenderPartial("Message", new MessageModel("No risks have been identified yet")); %>
    <% } else { %>
    
        <% foreach (var a in Model.Risks) { %>
        
            <div class="risk">
        
                <%: Html.ActionLink<NIST800_30Controller>(b=>b.Edit(Model.ProjectStep.Id, Model.Project.id, a.id), "Edit", new {@class="button ui-corner-all ui-state-default", style="float:right; top: -5px;"}) %>
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
                </ul>
            </div>
        
        <% } %>
        
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <style type="text/css">
        .risk { margin-top: 3em; }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

<%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>

</asp:Content>

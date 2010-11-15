<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.SecurityStep1ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 1 - Agree on Definitions
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink<GuidanceController>(a=>a.SecurityStep1(), "Guidance", new {@class = "button ui-corner-all ui-state-default", style="float:right;"}) %>

    <h2>Security Step 1 - Agree on Definitions</h2>

    <% if (Model.ProjectManager || Model.Stakeholder || Model.RequirementsEngineer) { %>
        <h3>Responsiblities</h3>
    <% } %>

    <% if (Model.ProjectManager) { %>
        <div class="responsibility-box ui-state-highlight">
            <p>
                This is your guidance as a project manager.
            </p>
        </div>
    <% } %>
    <% if (Model.RequirementsEngineer) { %>
        <div class="responsibility-box ui-state-highlight">
            <p>
                This is your guidance as a requirements engineer.
            </p>
        </div>
    <% } %>    
    <% if (Model.Stakeholder) { %>
        <div class="responsibility-box ui-state-highlight">
            <p>
                This is your guidance as a stake holder.
            </p>
        </div>
    <% } %>

    <%: Html.ActionLink<SecurityController>(a=>a.Step1ViewPredefinedTerms(Model.Step.id, Model.Project.id), "View Predefined Terms", new {@class="button ui-corner-all ui-state-default", style="float:right; margin-top: 8px;"}) %>
    <%: Html.ActionLink<SecurityController>(a => a.Step1AddNewTerm(Model.Step.id, Model.Project.id), "Add New Term", new { @class = "button ui-corner-all ui-state-default", style = "float:right; margin-top: 8px; margin-right: 5px;" })%>

    <h3>Selected Definitions</h3>

    <% Html.RenderPartial("Step1ProjectTermsView"); %>


</asp:Content>


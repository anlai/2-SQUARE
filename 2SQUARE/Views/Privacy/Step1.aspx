<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step1ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Privacy Step 1 - Agree on Definitions
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.Id), Model.Project.Name + " Home", new {@class="nav-button"}) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Privacy Step 1 - Agree on Definitions</h2>

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1">
                <h3>Selected Definitions</h3>
            </div>
            <div class="col2">
                <%: Html.ActionLink<ProjectTermController>(a => a.ViewPredefinedTerms(Model.ProjectStep.Id, Model.Project.Id), "View Predefined Terms", new { @class = "button" })%>
                <%: Html.ActionLink<ProjectTermController>(a => a.AddNewTerm(Model.ProjectStep.Id, Model.Project.Id), "Add New Term", new { @class = "button"})%>
            </div>

        </div>    

        <div class="section-contents">
            <% Html.RenderPartial("ProjectTermsView"); %>
        </div>

    </div>

</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step5ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>
<%@ Import Namespace="_2SQUARE.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 5 - Select Elicitation Technique
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Security Step 5 - Select Elicitation Technique</h2>

    <% if (Model.Project.SecurityElicitationType != null) { %>
        <% Html.RenderPartial("Message", new MessageModel(string.Format("{0} is the selected elicitation technique.", Model.Project.SecurityElicitationType.Name))); %>
    <% } %>

    <%foreach (var a in Model.ElicitationTypes) { %>
    
        <fieldset>
            <legend><%: a.Name %></legend>

            <% if (a.id != Model.Project.SecurityElicitationId) { %>

                <% using (Html.BeginForm("SelectElicitationType", "Security")) { %>
                    <%: Html.Hidden("id", Model.ProjectStep.Id) %>
                    <%: Html.Hidden("projectId", Model.Project.id) %>
                    <%: Html.Hidden("elicitationId", a.id) %>

                    <%: Html.SubmitButton("Select", "Select", new {@class="button ui-corner-all ui-state-default", style="float:right; top: -.5em;"}) %>
                <% } %>
            <% } %>

            <h3>Description:</h3>
            <%: a.Description %>

            <h3>Strengths:</h3>
            <%: a.Strengths %>

            <h3>Weaknesses:</h3>
            <%: a.Weaknesses %>

        </fieldset>

    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>

</asp:Content>

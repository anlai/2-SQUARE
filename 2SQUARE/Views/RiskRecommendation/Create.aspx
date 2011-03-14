<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.RiskRecommendationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Risk Recommendation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create Risk Recommendation</h2>

    <% using (Html.BeginForm()) { %>

    <%: Html.ValidationSummary() %>

    <ul class="entry-form">
        <li><strong>Recommendated Controls:</strong>
            <%: Html.TextAreaFor(a=>a.RiskRecommendation.Controls) %>
        </li>
        <li><strong>Impact:</strong>
            <%: Html.TextAreaFor(a=>a.RiskRecommendation.Impact) %>
        </li>
        <li><strong>Feasibility:</strong>
            <%: Html.TextAreaFor(a=>a.RiskRecommendation.Feasibility) %>
        </li>
        <li><strong></strong>
            <%: Html.SubmitButton("Save", "Save", new { @class = "button ui-corner-all ui-state-default" })%>
            <%: Html.ActionLink("Cancel", "Index", Model.Risk.AssessmentType.Controller, new {id=Model.ProjectStepId, projectId=Model.Risk.ProjectId}, new {@class="button ui-state-default ui-corner-all"}) %>
        </li>
    </ul>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink("Back to " + Model.Risk.AssessmentType.Name, "Index", Model.Risk.AssessmentType.Controller, new {id=Model.ProjectStepId, projectId=Model.Risk.ProjectId}, new {@class="button ui-state-default"}) %>

</asp:Content>

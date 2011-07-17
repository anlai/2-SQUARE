<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.RiskRecommendationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Risk Recommendation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create Risk Recommendation</h2>

    <% Html.RenderPartial("RiskRecommendationView"); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink("Back to " + Model.Risk.AssessmentType.Name, "Index", Model.Risk.AssessmentType.Controller, new {id=Model.ProjectStepId, projectId=Model.Risk.Project.Id}, new {@class="button ui-state-default"}) %>

</asp:Content>

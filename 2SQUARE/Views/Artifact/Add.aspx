<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ArtifactViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add <%: Model.ProjectStep.Step.SquareType.Name %> Artifact
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add <%: Model.ProjectStep.Step.SquareType.Name %> Artifact</h2>

    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm("Add", "Artifact", FormMethod.Post, new { enctype = "multipart/form-data" })) { %>
        <% Html.RenderPartial("ArtifactForm"); %>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink("Back to Step 3", Model.ProjectStep.Step.Action, Model.ProjectStep.Step.Controller, new {id=Model.ProjectStep.Id, projectId=Model.ProjectStep.ProjectId}, new {@class="button ui-state-default"}) %>

</asp:Content>

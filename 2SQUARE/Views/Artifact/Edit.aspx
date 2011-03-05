<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ArtifactViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit <%: Model.ProjectStep.Step.SquareType.Name %> Artifact
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit <%: Model.ProjectStep.Step.SquareType.Name %> Artifact</h2>

    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm("Edit", "Artifact", FormMethod.Post, new { enctype = "multipart/form-data" })) { %>
        <%: Html.Hidden("artifactId", Model.Artifact.id) %>
        <% Html.RenderPartial("ArtifactForm"); %>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink("Back to Step 3", Model.ProjectStep.Step.Action, Model.ProjectStep.Step.Controller, new {id=Model.ProjectStep.Id, projectId=Model.ProjectStep.ProjectId}, new {@class="button ui-state-default"}) %>

</asp:Content>

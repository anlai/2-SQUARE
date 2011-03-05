<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ArtifactViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Artifact Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Model.Artifact.Name %></h2>

    <% if (Model.Artifact.Data != null) { %>
        <!-- We have some sort of file -->

        <% if (Model.Artifact.ContentType.StartsWith("image")) { %>
            <!-- Embed the image -->
            <img src="<%: Url.Action("GetFile", "Artifact", new {id=Model.Artifact.id}) %>" />
        <% } else { %>
            <!-- Not sure how to handle the file, so just give download link -->
            <a href="<%: Url.Action("GetFile", "Artifact", new {id=Model.Artifact.id}) %>">Download File</a>
        <% } %>


    <% } %>

    <h3>Details</h3>
    <ul class="entry-form">
        <li><strong>Artifact Type:</strong>
            <%: Model.Artifact.ArtifactType.Name %>
        </li>
        <li><strong>Description:</strong>
            <%: Model.Artifact.Description %>
        </li>
        <li><strong>Created By:</strong>
            <%: Model.Artifact.CreatedBy %>
        </li>
        <li><strong>Create Date:</strong>
            <%: Model.Artifact.DateCreated %>
        </li>
    </ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink("Back to Step 3", Model.ProjectStep.Step.Action, Model.ProjectStep.Step.Controller, new {id=Model.ProjectStep.Id, projectId=Model.ProjectStep.ProjectId}, new {@class="button ui-state-default"}) %>

</asp:Content>

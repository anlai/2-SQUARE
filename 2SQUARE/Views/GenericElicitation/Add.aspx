<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.RequirementViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Requirement
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Requirement</h2>

    <%: Html.ValidationSummary("Please correct the following errors.") %>

    <% using (Html.BeginForm()) { %>
        <ul class="entry-form">
            <li><strong>Requirement Id:</strong>
                <%: Html.TextBoxFor(a=>a.Requirement.RequirementId) %>
                <em>*Leaving blank will be filled with auto generated id.</em>
            </li>
            <li><strong>Name:</strong>
                <%: Html.TextBoxFor(a=>a.Requirement.Name) %>
            </li>
            <li><strong>Requirement:</strong>
                <%: Html.TextAreaFor(a=>a.Requirement.Requirement1) %>
            </li>
            <li><strong></strong>
                <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
                <%: Html.ActionLink("Cancel", "Step6", Model.ProjectStep.Step.SquareType.Name, new {id=Model.ProjectStep.Step.id, projectId=Model.Project.id}, new {@class="button ui-state-default ui-corner-all"}) %>
            </li>
        </ul>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink<GenericElicitationController>(a=>a.Index(Model.ProjectStep.Id, Model.Project.id), "Generic Elicitation Home", new {@class="button ui-state-default"}) %>
</asp:Content>

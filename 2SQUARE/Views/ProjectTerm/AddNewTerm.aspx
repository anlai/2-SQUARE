<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ProjectTermAddNewTermViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add New Term
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add New Term</h2>

    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) { %>

        <%: Html.ValidationSummary() %>

        <%: Html.HiddenFor(a=>a.ProjectTerm.ProjectId) %>
        <%: Html.HiddenFor(a=>a.ProjectTerm.SquareType) %>

        <ul class="entry-form">
            <li>
                <strong>Term:</strong>
                <%: Html.TextBoxFor(a=>a.ProjectTerm.Term) %>
            </li>
            <li>
                <strong>Definition:</strong>
                <%: Html.TextAreaFor(a=>a.ProjectTerm.Definition) %>
            </li>
            <li>
                <strong>Source:</strong>
                <%: Html.TextBoxFor(a=>a.ProjectTerm.Source) %>
            </li>
            <li>
                <strong></strong>
                <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
                <%: Html.ActionLink("Cancel", "Step1", Model.Step.SquareType.Name, new {id=Model.Step.id, projectId=Model.ProjectTerm.ProjectId}, new {@class="button ui-state-default"}) %>
            </li>
        </ul>

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink("Back to Step 1", "Step1", Model.Step.SquareType.Name, new {id=Model.Step.id, projectId=Model.ProjectTerm.ProjectId}, new {@class="button ui-state-default"}) %>
</asp:Content>

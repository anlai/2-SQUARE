<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.SecurityStep1AddNewTermViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Step1AddNewTerm
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Step1AddNewTerm</h2>

    <% using (Html.BeginForm("Step1AddNewTerm", "Security", FormMethod.Post, new {style="width:100%;"})) { %>

        <%: Html.HiddenFor(a=>a.ProjectTerm.ProjectId) %>
        <%: Html.HiddenFor(a=>a.ProjectTerm.SquareTypeId) %>

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
                <%: Html.ActionLink<SecurityController>(a=>a.Step1(Model.Step.id, Model.ProjectTerm.ProjectId), "Cancel", new {@class="button ui-corner-all ui-state-default"}) %>
            </li>
        </ul>

    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink<SecurityController>(a=>a.Step1(Model.Step.id, Model.ProjectTerm.ProjectId), "Back To Step 1", new {@class="button ui-state-default"}) %>
</asp:Content>

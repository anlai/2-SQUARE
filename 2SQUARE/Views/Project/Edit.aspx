<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Project>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Project
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Project</h2>

    <% using (Html.BeginForm()) { %>
    
        <%: Html.ValidationSummary() %>

        <ul class="entry-form">
            <li><strong>Name:</strong>
                <%: Html.TextBoxFor(a=>a.Name) %>
            </li>
            <li><strong>Description:</strong>
                <%: Html.TextAreaFor(a=>a.Description) %>
            </li>
            <li><strong>&nbsp;</strong>
                <%: Html.SubmitButton("Save", "Save", new { @class = "button ui-corner-all ui-state-default" })%>
                <%: Html.ActionLink("Cancel", "Details", "Project", new {id=Model.Id}, new {@class="button ui-state-default"}) %>
            </li>
        </ul>

    <% } %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
</asp:Content>

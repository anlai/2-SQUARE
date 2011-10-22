<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Controllers.AddPermissionViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Permission
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Permission</h2>

    <% using (Html.BeginForm()) { %>
    
        <%: Html.ValidationSummary() %>

        <ul class="entry-form">
            <li><strong>User:</strong>
                <%= this.Select("UserId").Options(Model.Users, x => x.UserId, x => x.Username).FirstOption("--Select User--") %>
            </li>
            <li><strong>Role:</strong>
                <%= this.Select("RoleId").Options(Model.Roles, x => x.Id, x => x.Name).FirstOption("--Select Role--") %>
            </li>
            <li><strong>&nbsp;</strong>
                <%: Html.SubmitButton("Save", "Save", new { @class = "button ui-corner-all ui-state-default" })%>
                <%: Html.ActionLink("Cancel", "Permissions", "Project", new {id=Model.ProjectId}, new {@class="button"}) %>
            </li>
        </ul>

    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
</asp:Content>

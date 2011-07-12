<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<Project>>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Projects List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink<ProjectController>(a => a.Create(), "Create New Project", new { @class = "button ui-corner-all ui-state-default", style = "float:right; margin-right: 5px;" })%>

    <h2>Projects</h2>

    <% if (Model.Count != 0) { %>
    <% foreach (var a in Model) { %>


        <div class="button object-list" style="text-align: left;">
            <h3><%: Html.ActionLink<ProjectController>(b=>b.Details(a.Id), a.Name) %></h3>
            <div><%= Html.ActionLink<ProjectController>(b=>b.Details(a.Id), a.Description ?? "n/a") %></div>
        </div>

        <div class="divider">&nbsp;</div>

    <% } %>
    <% } else { %>
    
        <p style="color: red;">You do not have access to a project.  Please create a new one or get added to an existing one.</p>

    <% } %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

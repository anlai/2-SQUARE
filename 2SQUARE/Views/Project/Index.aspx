<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<_2SQUARE.Models.Project>>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Projects List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Projects</h2>

    <% foreach (var a in Model) { %>


        <div class="button object-list" style="text-align: left;">
            <h3><%: Html.ActionLink<ProjectController>(b=>b.Details(a.id), a.Name) %></h3>
            <div><%= Html.ActionLink<ProjectController>(b=>b.Details(a.id), a.Description) %></div>
        </div>

        <div class="divider">&nbsp;</div>
        
        

    <% } %>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step4ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 4 - Risk Assessment
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Security Step 4 - Risk Assessment</h2>

    <table cellpadding="5px">
        <thead>
            <tr>
                <th></th>
                <th>Name</th>
                <th>Source</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var a in Model.AssessmentTypes) { %>
                <tr class="definition-row">
                    <td class="button-cell"><a href="<%: Url.Action("Index", a.Controller) %>" class="button ui-state-default ui-corner-all">Select</a></td>
                    <td><%: a.Name %></td>
                    <td><%: a.Source %></td>
                </tr>
            <%} %>
        </tbody>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.PRETResultViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PRET Results
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>PRET Results</h2>

    <dl>
    <% foreach (var a in Model.PretLaws) { %>
        <dt><%: a.Name %></dt>
        <dd><%: a.Description %></dd>
    <% } %>
    </dl>

    <h3>Requirements</h3>
    <table>
        <thead>
            <tr>
                <th>Requirement</th>
                <th>Law</th>
                <th>Source</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var a in Model.PretRequirements) { %>
                <tr>
                    <td><%: a.Requirement%></td>
                    <td><%: a.Law.Name %></td>
                    <td><%: a.Source %></td>
                </tr>
            <% } %>
        </tbody>
    </table>

    <div style="margin-top: 2em;">
    <% using (Html.BeginForm()) { %>
        <% foreach (var a in Model.PretLaws) { %>
            <%: Html.Hidden("lawIds", a.Id) %>
        <% } %>

        <%: Html.SubmitButton("Accept", "Accept Law(s)", new {@class="button ui-state-default ui-corner-all"}) %>
        <%: Html.ActionLink<PRETController>(a=>a.Index(Model.ProjectStep.Id, Model.Project.Id), "Cancel", new {@class="button ui-state-default ui-corner-all"}) %>
    <% } %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
</asp:Content>

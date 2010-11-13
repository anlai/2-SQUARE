<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.SecurityStep1ViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 1 - Agree on Definitions
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Security Step 1 - Agree on Definitions</h2>

    <h3>Selected Definitions</h3>
    <h3>Pending Terms</h3>

    <table cellpadding="5px">
        <thead>
            <tr>
                <th></th>
                <th>Source</th>
                <th>Definition</th>
            </tr>
        </thead>
        <tbody>
            <%foreach (var a in Model.PendingTerms) { %>
                <tr class="term">
                    <td colspan="3"><%: a.Name %></td>
                </tr>
                <% foreach (var b in a.Definitions) { %>
                    <tr class="definition-row">
                        <td><a href="#" class="button ui-corner-all ui-state-default">Select</a></td>
                        <td class="source-cell">[<%: b.Source %>]</td>
                        <td><%: b.Description %></td>
                    </tr>
                <% } %>

                <tr>
                    <td><a href="#" class="button ui-corner-all ui-state-default">Select</a></td>
                    <td class="source-cell"><%: Html.TextBox("Source") %></td>
                    <td><%: Html.TextBox("Definition", string.Empty, new {style="width:100%;"}) %></td>
                </tr>

            <% } %>

        </tbody>
    </table>

<%--
    <% foreach (var a in Model.PendingTerms) { %>
    
        <span class="term">
            <strong><%: a.Name %></strong>
        <ul>
            <% foreach (var b in a.Definitions) { %>

                <li class="definition"">
                    <span><a href="#" class="button ui-corner-all ui-state-default">Select</a></span>
                    <span class="source">[<%: b.Source %>]</span>
                    <span ><%: b.Description %></span>
                </li>    
            <% } %>
        </ul>
        </span>

    <% } %>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

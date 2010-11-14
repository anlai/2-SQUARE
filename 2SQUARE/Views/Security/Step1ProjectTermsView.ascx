<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.SecurityStep1ViewModel>" %>

    <table cellpadding="5px">
        <thead>
            <tr>
                <th></th>
                <th>Term</th>
                <th>Source</th>
                <th>Definition</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var a in Model.ProjectTerms) { %>
                <tr class="definition-row">
                    <td class="button-cell"><a href="#" class="button ui-corner-all ui-state-default">Edit</a></td>
                    <td class="term-cell"><%: a.Term %></td>
                    <td class="source-cell">[<%: a.Source %>]</td>
                    <td><%: a.Definition %></td>
                </tr>
            <% } %>
        </tbody>
    </table>
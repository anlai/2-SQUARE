<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.Step1ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Models" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

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
                    <td class="button-cell"><%: Html.ActionLink<ProjectTermController>(b=>b.EditTerm(a.id, Model.ProjectStep.Id), "Edit", new {@class="button ui-state-default ui-corner-all"}) %></td>
                    <td class="term-cell"><%: a.Term %></td>
                    <td class="source-cell">[<%: a.Source %>]</td>
                    <td><%: a.Definition %></td>
                </tr>
            <% } %>
        </tbody>
    </table>
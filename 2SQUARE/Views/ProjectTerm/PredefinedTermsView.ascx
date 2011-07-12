<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.ProjectTermPredefinedTermsViewModel>" %>

    <table cellpadding="5px">
        <thead>
            <tr>
                <th></th>
                <th>Source</th>
                <th>Definition</th>
            </tr>
        </thead>
        <tbody>
            <%foreach (var a in Model.PredefinedTerms) { %>
                <tr class="term">
                    <td colspan="3"><%: a.Name %></td>
                </tr>
                <% foreach (var b in a.Definitions) { %>
                    <tr class="definition-row">
                        <% using (Html.BeginForm()) { %>

                            <%: Html.Hidden("Id", Model.Step.id) %>
                            <%: Html.Hidden("projectId", Model.Project.id)%>
                            <%: Html.Hidden("SquareType", Model.Step.SquareType)%>
                            <%: Html.Hidden("termId", a.id) %>
                            <%: Html.Hidden("definitionId", b.id) %>

                            <td class="button-cell"><%: Html.SubmitButton("Submit", "Select", new { @class = "button ui-corner-all ui-state-default" })%></td>
                            <td class="source-cell">[<%: b.Source %>]</td>
                            <td><%: b.Description %></td>
                        <% } %>
                    </tr>
                <% } %>

                <tr>
                    <% using (Html.BeginForm()) { %>
                        
                        <%: Html.Hidden("Id", Model.Step.id) %>
                        <%: Html.Hidden("projectId", Model.Project.id) %>
                        <%: Html.Hidden("SquareType", Model.Step.SquareType) %>
                        <%: Html.Hidden("term", a.Name) %>
                        
                        <td><%: Html.SubmitButton("Submit", "Select", new { @class = "button ui-corner-all ui-state-default" })%></td>
                        <td class="source-cell"><%: Html.TextBox("source") %></td>
                        <td><%: Html.TextBox("definition", string.Empty, new {style="width:100%;"}) %></td>
                    <% }%>  
                </tr>

            <% } %>

        </tbody>
    </table>
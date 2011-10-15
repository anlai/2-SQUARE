<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ProjectTermEditViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Term
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ValidationSummary() %>

    <h2><%: Model.ProjectTerm.Term %></h2>

    <fieldset>
        <legend>Current Definition</legend>
        
        
        <ul class="entry-form">
            <%  using (Html.BeginForm()) { %>
            <li><strong>&nbsp;</strong><%: Html.TextArea("definition", Model.ProjectTerm.Definition, new {cols=95}) %></li>
            <li><strong>Source:</strong><%: Html.TextBox("source", Model.ProjectTerm.Source) %></li>
            <li style="margin-top: 10px;">
                <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
                <% } %>
                <% using (Html.BeginForm("RemoveTerm", "ProjectTerm", FormMethod.Post, new {style="display:inline;"})) { %>
                        
                    <%: Html.Hidden("id", Model.ProjectTerm.Id) %>
                    <%: Html.Hidden("projectStepId", Model.ProjectStepId)%>

                    <%: Html.SubmitButton("Delete", "Delete", new {@class="button ui-corner-all ui-state-default"}) %>
                <% } %>
            </li>
        </ul>
       

    </fieldset>

    <h2>Predefined Definitions</h2>

    <table cellpadding="5px">
        <thead>
            <tr>
                <th></th>
                <th>Source</th>
                <th>Definition</th>
            </tr>
        </thead>
        <tbody>
            <%foreach (var a in Model.Definitions) { %>
                <tr class="definition-row">
                    <% using (Html.BeginForm()) { %>

                        <%: Html.Hidden("definitionId", a.Id) %>

                        <td class="button-cell"><%: Html.SubmitButton("Submit", "Select", new { @class = "button ui-corner-all ui-state-default" })%></td>
                        <td class="source-cell">[<%: a.Source %>]</td>
                        <td><%: a.Description %></td>
                    <% } %>
                </tr>
            <% } %>

        </tbody>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink("Back to Step 1", "Step1", Model.ProjectTerm.SquareType.Name, new {id=Model.ProjectStepId, projectId=Model.ProjectTerm.Project.Id}, new {@class="nav-button"}) %>
</asp:Content>

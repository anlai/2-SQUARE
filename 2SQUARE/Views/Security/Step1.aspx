<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.SecurityStep1ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 1 - Agree on Definitions
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink<GuidanceController>(a=>a.SecurityStep1(), "Guidance", new {@class = "button ui-corner-all ui-state-default", style="float:right;"}) %>

    <h2>Security Step 1 - Agree on Definitions</h2>

    <h3>Selected Definitions</h3>

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
                        <% using (Html.BeginForm("AddTerm", "Security", FormMethod.Post)) { %>

                            <%: Html.Hidden("Id", Model.Step.id) %>
                            <%: Html.Hidden("projectId", Model.Project.id)%>
                            <%: Html.Hidden("squareTypeId", Model.Step.SquareTypeId)%>
                            <%: Html.Hidden("termId", a.id) %>
                            <%: Html.Hidden("definitionId", b.id) %>

                            <td class="button-cell"><%: Html.SubmitButton("Submit", "Select", new { @class = "button ui-corner-all ui-state-default" })%></td>
                            <td class="source-cell">[<%: b.Source %>]</td>
                            <td><%: b.Description %></td>
                        <% } %>
                    </tr>
                <% } %>

                <tr>
                    <% using (Html.BeginForm("AddNewTerm", "Security", FormMethod.Post)) { %>
                        
                        <%: Html.Hidden("Id", Model.Step.id) %>
                        <%: Html.Hidden("projectId", Model.Project.id) %>
                        <%: Html.Hidden("squareTypeId", Model.Step.SquareTypeId) %>
                        <%: Html.Hidden("term", a.Name) %>
                        
                        <td><%: Html.SubmitButton("Submit", "Select", new { @class = "button ui-corner-all ui-state-default" })%></td>
                        <td class="source-cell"><%: Html.TextBox("source") %></td>
                        <td><%: Html.TextBox("definition", string.Empty, new {style="width:100%;"}) %></td>
                    <% }%>  
                </tr>

            <% } %>

        </tbody>
    </table>

</asp:Content>


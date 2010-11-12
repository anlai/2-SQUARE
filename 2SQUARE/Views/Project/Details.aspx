<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ProjectDetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Model.Project.Name %></h2>

    <div id="description">
        <%: Model.Project.Description %>
    </div>

    <h2>SQUARE Steps</h2>

    <% foreach (var a in Model.SquareTypes) { %>

        <div class="steps-container">
            
            <%

           
           foreach (var b in Model.Project.ProjectSteps.Where(c => c.Step.SquareType.id == a.id).OrderBy(c => c.Step.Order))
           {
               %>
            
                <ul>
                <li><%: b.Step.Name %></li>
                <li><%: b.Complete %></li>
                <li><%: b.DateStarted %></li>
                </ul>
            <% } %>

        </div>

    <% } %>
    


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

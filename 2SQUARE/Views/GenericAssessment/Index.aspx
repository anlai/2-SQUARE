<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Controllers.GenericAssessmentViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Generic Risk Assessment Module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">
    
        <div class="section-header">
            <div class="col1"><h2>Generic Risk Assessment Module</h2></div>
            <div class="col2"><%: Html.ActionLink("Create New Risk", "Create", new {id=Model.ProjectStep.Id, projectId = Model.Project.Id}, new {@class="button"}) %></div>    
        </div>

        <div class="section-contents">
        
            <table>
                <thead>
                    <tr>
                        <th></th>
                        <th>Name</th>
                        <th>Vulnerability</th>
                        <th>Risk Level</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var risk in Model.Risks) { %>
                        <tr>
                            <td>
                                <%: Html.ActionLink("Edit", "Edit", new {id=risk.Id, projectStepid=Model.ProjectStep.Id, projectId=Model.Project.Id}, new {@class="button"}) %>
                                <%: Html.ActionLink("Delete", "Delete", new { id = risk.Id, projectStepid = Model.ProjectStep.Id }, new { @class = "button" })%>
                            </td>
                            <td><%: risk.Name %></td>
                            <td><%: risk.Vulnerability %></td>
                            <td><%: risk.RiskLevel.Name %></td>
                        </tr>
                    <% } %>
                </tbody>
            </table>

        </div>

    </div>

    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink("Back to Project", "Details", "Project", new {id=Model.Project.Id}, new {@class="nav-button"}) %>

</asp:Content>

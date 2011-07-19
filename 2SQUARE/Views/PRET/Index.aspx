<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.PRETViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Privacy Requirements Elicitation Technique (PRET) Module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>PRET Module</h2>

    <fieldset style="padding: 1em;">
        
        <legend>Actions</legend>

        <%: Html.ActionLink<PRETController>(a=>a.Run(Model.ProjectStep.Id, Model.Project.Id), "Run PRET", new {@class="button ui-corner-all ui-state-default"}) %>

        <%: Html.ActionLink<GenericElicitationController>(a => a.Index(Model.ProjectStep.Id, Model.Project.Id), "Edit Requirements Manually", new { @class = "button ui-corner-all ui-state-default" })%>
    </fieldset>

    <fieldset style="padding: 1em;">
        
        <legend>Requirements</legend>

        <table>
            
            <thead>
                <tr>
                    <td>Id</td>
                    <td>Requirement</td>
                </tr>
            </thead>

            <tbody>
                <% foreach (var a in Model.Project.Requirements) { %>
            
                    <tr>
                        <td><%: a.RequirementId %></td>
                        <td><%: a.RequirementText %></td>
                    </tr>

                <% } %>
            </tbody>
        </table>

    </fieldset>
    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

<%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.Id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step3ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 3 - Develop Artifacts
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink<ArtifactController>(a=>a.Add(Model.ProjectStep.Id), "Add Artifact", new {@class="button ui-corner-all ui-state-default", style="float:right;"}) %>

    <h2>Security Step 3 - Develop Artifacts</h2>
    
    <% if (Model.Artifacts.Count == 0)
       { %>
        <div class="ui-widget messages">
            <div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; margin-bottom: 10px; padding: 0pt 0.7em;">
                <p>
                    <span class="ui-icon ui-icon-info" style="float:left; margin-right: 0.3em;"></span>
                    <span >No artifacts have been entered.</span>
                </p>
            </div>
        </div>
    <% } else { %>
    <table cellpadding="5px">
        <thead>
            <tr>
                <th></th>
                <th>Name</th>
                <th>Artifact Type</th>
                <th>Created By</th>
                <th>Date Created</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var a in Model.Artifacts)
               { %>
                <tr class="definition-row">
                    <td class="button-cell" style="width: 135px;">
                        <%: Html.ActionLink<ArtifactController>(b=>b.Edit(Model.ProjectStep.Id, a.id), "Edit", new {@class="button ui-state-default ui-corner-all"}) %>
                        <%: Html.ActionLink<ArtifactController>(b=>b.Delete(Model.ProjectStep.Id, a.id), "Delete", new {@class="button ui-state-default ui-corner-all"}) %>
                    </td>
                    <td><%: a.Name%></td>
                    <td><%: a.ArtifactType.Name%></td>
                    <td><%: a.CreatedBy %></td>
                    <td><%: a.DateCreated %></td>
                </tr>
            <% } %>
        </tbody>
    </table>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.CategoryViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Requirement Categories
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1"><h2>Requirement Categories</h2></div>
            <div class="col2"><%= Html.ActionLink<CategoryController>(a=>a.Create(Model.ProjectStep.Id, Model.Project.Id), "Create Category", new {@class="button"}) %></div>

        </div>    

        <div class="section-contents">
            <table>
                <thead>
                    <tr>
                        <th></th>
                        <th>Name</th>
                        <th>Square Type</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var a in Model.Categories) { %>
                        <tr>
                            <td style="width: 150px;">
                                <%= Html.ActionLink<CategoryController>(b => b.Edit(Model.ProjectStep.Id, Model.Project.Id, a.Id), "Edit", new { @class = "button ui-corner-all ui-state-default" })%>
                                <% using(Html.BeginForm("Delete", "Category", new {id=Model.ProjectStep.Id, projectId=Model.Project.Id, categoryId=a.Id}, FormMethod.Post, new {style="display:inline-block;"})) { %>
                                    <%: Html.SubmitButton("Delete", "Delete", new {@class="button ui-corner-all ui-state-default"}) %>
                                <% } %>
                            </td>
                            <td><%: a.Name %></td>
                            <td><%: a.SquareType.Name %></td>
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

    <%: Html.ActionLink(string.Format("Back to {0} Step 7", Model.ProjectStep.Step.SquareType.Name), Model.ProjectStep.Step.Action, Model.ProjectStep.Step.Controller, new {id=Model.ProjectStep.Id, projectId=Model.Project.Id}, new {@class="nav-button"}) %>

</asp:Content>

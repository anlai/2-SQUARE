﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.CategoryViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Requirement Categories
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.ActionLink<CategoryController>(a=>a.Create(Model.ProjectStep.Id, Model.Project.id), "Create Category", new {@class="button ui-state-default ui-corner-all", style="float:right;"}) %>

    <h2>Requirement Categories</h2>

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
                        <%= Html.ActionLink<CategoryController>(b => b.Edit(Model.ProjectStep.Id, Model.Project.id, a.id), "Edit", new { @class = "button ui-corner-all ui-state-default" })%>
                        <% using(Html.BeginForm("Delete", "Category", new {id=Model.ProjectStep.Id, projectId=Model.Project.id, categoryId=a.id}, FormMethod.Post, new {style="display:inline-block;"})) { %>
                            <%: Html.SubmitButton("Delete", "Delete", new {@class="button ui-corner-all ui-state-default"}) %>
                        <% } %>
                    </td>
                    <td><%: a.Name %></td>
                    <td><%: a.SquareType.Name %></td>
                </tr>
            <% } %>
        </tbody>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink(string.Format("Back to {0} Step 7", Model.ProjectStep.Step.SquareType.Name), Model.ProjectStep.Step.Action, Model.ProjectStep.Step.Controller, new {id=Model.ProjectStep.Id, projectId=Model.Project.id}, new {@class="button ui-state-default"}) %>

</asp:Content>

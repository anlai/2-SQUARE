<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.GenericElicitationViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>
<%@ Import Namespace="_2SQUARE.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.ProjectStep.Step.SquareType.Name %> Step 6 - Elicit Requirements
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1"><h2><%: Model.ProjectStep.Step.SquareType.Name %> Step 6 - Elicit Requirements</h2></div>
            <div class="col2"><%: Html.ActionLink<GenericElicitationController>(a => a.Add(Model.ProjectStep.Id, Model.Project.Id), "Add Requirement", new { @class = "button" })%></div>

        </div>    

        <div class="section-contents">
            <% if (Model.Requirements.Count() == 0) { %>
                <% Html.RenderPartial("Message", new MessageModel("No requirements have been entered.")); %>
            <% } else { %>
            <table>
                <thead>
                    <tr>
                        <th></th>
                        <th>Id</th>
                        <th>Requirement</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var a in Model.Requirements) { %>
                        <tr>
                            <td style="width: 150px;">
                                <%: Html.ActionLink<GenericElicitationController>(b=>b.Edit(Model.ProjectStep.Id, Model.Project.Id, a.Id), "Edit", new {@class="button"}) %>
                                <%: Html.ActionLink<GenericElicitationController>(b=>b.Delete(Model.ProjectStep.Id, Model.Project.Id, a.Id), "Delete", new {@class="button"}) %>
                            </td>
                            <td><%: a.RequirementId ?? a.Id.ToString() %></td>
                            <td><%: a.RequirementText %></td>
                        </tr>
                    <% } %>
                </tbody>
            </table>
            <% } %>        
        </div>

    </div>

    

    



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.Id), Model.Project.Name + " Home", new {@class="nav-button"}) %>

</asp:Content>

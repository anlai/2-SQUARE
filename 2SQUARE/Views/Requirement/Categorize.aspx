<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.RequirementCategoryViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Categorize Requirement
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Categorize Requirement</h2>

    <ul class="entry-form">
        <li><strong>Id:</strong>
            <%: Model.Requirement.RequirementId %>
        </li>
        <li><strong>Name:</strong>
            <%: Model.Requirement.Name %>
        </li>
        <li><strong>Requirement:</strong>
            <%: Model.Requirement.Requirement1 %>
        </li>
    </ul>

    <%: Html.ValidationMessage("Please correct the following errors:") %>

    <% using (Html.BeginForm()) { %>
        <ul class="entry-form">
            <li><strong>Category:</strong>
                <%= this.Select("Requirement.CategoryId").Options(Model.Categories,x=>x.id,x=>x.Name).Selected(Model.Requirement.CategoryId).FirstOption("--Select a Category") %>
            </li>
            <li><strong>Essential:</strong>
                <%: Html.CheckBox("Requirement.Essential", Model.Requirement.Essential) %>
            </li>
            <li><strong></strong>
                <%: Html.SubmitButton("Svae", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
                <%: Html.ActionLink("Back to List", Model.ProjectStep.Step.Action, Model.ProjectStep.Step.Controller, new {id=Model.ProjectStep.Id,projectId=Model.Project.id}, new {@class="button ui-state-default ui-state-default"}) %>
            </li>
        </ul>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink("Back to List", Model.ProjectStep.Step.Action, Model.ProjectStep.Step.Controller, new {id=Model.ProjectStep.Id,projectId=Model.Project.id}, new {@class="button ui-state-default"}) %>

</asp:Content>

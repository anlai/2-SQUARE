<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.CategoryEditViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

    <%: Html.ValidationMessage("Please correct the following errors:") %>

    <% using (Html.BeginForm()) { %>
        <ul class="entry-form">
            <li><strong>Name:</strong>
                <%: Html.TextBoxFor(a=>a.Category.Name) %>
            </li>
            <li><strong></strong>
                <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
                <%= Html.ActionLink<CategoryController>(a=>a.Index(Model.ProjectStep.Id, Model.Project.id), "Cancel", new {@class="button ui-corner-all ui-state-default"}) %>
            </li>
        </ul>
    <% } %>
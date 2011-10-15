<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.RequirementViewModel>" %>

    <%: Html.ValidationSummary("Please correct the following errors.") %>

    <% using (Html.BeginForm())
       {%>
        <ul class="entry-form">
            <li><strong>Requirement Id:</strong>
                <%:Html.TextBoxFor(a => a.Requirement.RequirementId)%>
                <em>*Leaving blank will be filled with auto generated id.</em>
            </li>
            <li><strong>Name:</strong>
                <%:Html.TextBoxFor(a => a.Requirement.Name)%>
            </li>
            <li><strong>Requirement:</strong>
                <%:Html.TextAreaFor(a => a.Requirement.RequirementText)%>
            </li>

            <li><strong>Goal:</strong>
                <%: Html.TextAreaFor(a=>a.Requirement.Goal) %>
            </li>
            <li><strong>Recommendation:</strong>
                <%: Html.TextAreaFor(a => a.Requirement.Recommendation)%>
            </li>
            <li><strong>Implementation:</strong>
                <%: Html.TextAreaFor(a => a.Requirement.Implementation)%>
            </li>
            <li><strong>Source:</strong>
                <%: Html.TextBoxFor(a => a.Requirement.Source) %>
            </li>

            <li><strong></strong>
                <%:Html.SubmitButton("Save", "Save", new {@class = "button ui-corner-all ui-state-default"})%>
                <%:Html.ActionLink("Cancel", "Index", new {id=Model.ProjectStep.Id, projectId=Model.Project.Id}, new {@class="button"}) %>
            </li>
        </ul>
    <% } %>
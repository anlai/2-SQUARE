<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ChangeStatusViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ChangeStatus
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Change Project Status</h2>
    <% foreach (var a in Model.Project.ProjectSteps.Select(a=>a.Step.SquareType).Distinct()) { %>
        <div class="squaretype-container">
        <strong class="header"><%: a.Name %></strong>
        <ul class="editing-form">
            <% foreach (var b in Model.ChangeStatusProjectSteps.Where(b => b.SquareTypeId == a.id).OrderBy(b => b.Order)) { %>
                <li>
                    <strong>
                        <select id="<%: b.ProjectStepId %>" class="status" data-id="<%: b.ProjectStepId %>" <%: !b.CanEdit ? "disabled" : string.Empty %> data-origValue="<%: (int)b.CurrentStepStatus %>">
                            <% foreach (var s in Model.Status) { %>
                                <option value="<%: s.Key %>" <%: (int)b.CurrentStepStatus == s.Key ? "selected" : string.Empty %>><%: s.Value %></option>
                            <% } %>
                        </select>
                    </strong>
                    <%: b.Name %>
                </li>
            <% } %>
        </ul>
        </div>
    <% } %>

    <div style="clear:both;">&nbsp;</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

    <script type="text/javascript">

        var projectId = '<%: Model.Project.id %>';
        var updateUrl = '<%: Url.Action("UpdateStatus", "Project") %>';

        $(function () {

            $("select.status").change(function () {
                var stepId = $(this).data("id");
                var status = $(this).val();

                $.post(updateUrl, { id: projectId, stepId: stepId, projectStepStatus: status }, function (result) {
                    if (result.IsValid) {
                        $.each(result.ChangeSteps, function (index, item) {
                            var $select = $('select[data-id="' + item.Key + '"]');

                            if (item.Value) $select.attr("disabled", "");
                            else $select.attr("disabled", "disabled");

                        });
                    }
                    else {
                        $("#errormessage-container").html("");
                        $("#message-container").html("");

                        var errors = $("<ul>");
                        var messages = $("<ul>");

                        // go through the warnings
                        $.each(result.Warnings, function (index, item) {
                            messages.append($("<li>").html(item));
                        });

                        // go through the errors
                        $.each(result.Errors, function (index, item) {
                            errors.append($("<li>").html(item));
                        });

                        if (errors.children().size() > 0) {
                            $("#errormessage-container").append(errors);
                            $("#errormessage-container").parents("div.messages").show();
                        }
                        if (messages.children().size() > 0) {
                            $("#message-container").append(messages);
                            $("#message-container").parents("div.messages").show();
                        }

                        // set the value of the selected step back to the original value becuase of the failure
                        var $select = $("select#" + result.ProjectStepId);
                        $select.val($select.data("origValue"));
                    }



                });
            });

        });
    </script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>
</asp:Content>

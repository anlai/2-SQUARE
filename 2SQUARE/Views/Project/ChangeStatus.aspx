<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.ChangeStatusViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ChangeStatus
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="errormessage-container" style="display:none;" class="ui-widget messages">
        <div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; margin-bottom: 10px; padding: 0pt 0.7em;">
            <p>
                <span class="ui-icon ui-icon-alert" style="float:left; margin-right: 0.3em;"></span>
                       
                <span id="error-messages"></span>
            </p>
        </div>
    </div>

    <div id="message-container" style="display:none;" class="ui-widget messages">
        <div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; margin-bottom: 10px; padding: 0pt 0.7em;">
            <p>
                <span class="ui-icon ui-icon-info" style="float:left; margin-right: 0.3em;"></span>
                       
                <span id="messages"></span>
            </p>
        </div>
    </div>

    <h2>Change Project Status</h2>
    <% foreach (var a in Model.Project.ProjectSteps.Select(a=>a.Step.SquareType).Distinct()) { %>
        <div class="squaretype-container">
        <strong class="header"><%: a.Name %></strong>
        <ul class="editing-form">
            <% foreach (var b in Model.ChangeStatusProjectSteps.Where(b => b.SquareType == a.Id).OrderBy(b => b.Order)) { %>
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

        var projectId = '<%: Model.Project.Id %>';
        var updateUrl = '<%: Url.Action("UpdateStatus", "Project") %>';

        $(function () {

            $("select.status").change(function () {
                var stepId = $(this).data("id");
                var status = $(this).val();

                $.post(updateUrl, { id: projectId, stepId: stepId, projectStepStatus: status }, function (result) {

                    // reset the message containers
                    var errorContainer = $("#errormessage-container");
                    var messageContainer = $("#message-container");

                    var errortxt = $("#error-messages");
                    var messagetxt = $("#messages");

                    // blank out the txt
                    errortxt.html("");
                    messagetxt.html("");

                    // hide the containers
                    errorContainer.hide();
                    messageContainer.hide();

                    if (result.IsValid) {
                        $.each(result.ChangeSteps, function (index, item) {
                            var $select = $('select[data-id="' + item.Key + '"]');
                        });
                    }
                    else {

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
                            errortxt.append(errors);
                            errorContainer.show();
                        }
                        if (messages.children().size() > 0) {
                            messagetxt.append(messages);
                            messageContainer.show();
                        }

                        // set the value of the selected step back to the original value becuase of the failure
                        var $select = $("select#" + result.ProjectStepId);
                        $select.val($select.data("origValue"));
                    }



                });
            });

        });
    </script>

    <style type="text/css">
        select {min-width: 20px;}
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.Id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>
</asp:Content>

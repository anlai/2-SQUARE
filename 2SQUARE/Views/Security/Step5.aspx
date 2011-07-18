<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step5ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>
<%@ Import Namespace="_2SQUARE.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security Step 5 - Select Elicitation Technique
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Security Step 5 - Select Elicitation Technique</h2>

    <% if (Model.Project.SecurityElicitationType != null) { %>
        <% Html.RenderPartial("Message", new MessageModel(message: string.Format("{0} is the selected elicitation technique.<br/><br/><strong>Rationale:</strong><br/>{1}", Model.Project.SecurityElicitationType.Name, Model.Project.SecurityElicitationRationale), encode: false)); %>
    <% } %>

    <%foreach (var a in Model.ElicitationTypes) { %>
    
        <fieldset>
            <legend><%: a.Name %></legend>

            <% if (a.Id != (Model.Project.SecurityAssessmentType != null ? Model.Project.SecurityAssessmentType.Id : -1)) { %>

                <input type="button" class="select_elicitation button ui-corner-all ui-state-default" style="float:right; top: -.5em;" value="Save" data-id='<%: a.Id %>' data-name='<%: a.Name %>' />

            <% } %>

            <h3>Description:</h3>
            <%: a.Description %>

            <h3>Strengths:</h3>
            <%: a.Strengths %>

            <h3>Weaknesses:</h3>
            <%: a.Weaknesses %>

        </fieldset>

    <% } %>

    <div id="dialog" title="Select Elicitation Technique">
        <% using (Html.BeginForm()) { %>

        <%: Html.Hidden("id", Model.ProjectStep.Id) %>
        <%: Html.Hidden("projectId", Model.Project.Id) %>
        <%: Html.Hidden("elicitationId") %>

        <strong>Technique: </strong><span id="elicitation_name"></span>
        <br /><br />
        <strong>Rationale:</strong><br />
        <textarea id="rationale" name="rationale" style="width: 377px; height: 100px;"></textarea>
        <br /><br />
        <input type="submit" value="Save" class="button ui-corner-all ui-state-default" />
        <% } %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    
    <script type="text/javascript">
        $(function () {
            $("#dialog").dialog({
                autoOpen: false,
                modal: true, width: 400, height: 250
            });

            $(".select_elicitation").click(function () {
                $("#elicitation_name").html($(this).data("name"));
                $("#elicitationId").val($(this).data("id"));

                $("#dialog").dialog("open");
            });
        });
    </script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.Id), Model.Project.Name + " Home", new {@class="button ui-state-default"}) %>

</asp:Content>

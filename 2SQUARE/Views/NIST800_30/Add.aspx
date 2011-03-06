<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.NIST800_30EditViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NIST 800-30 Risk Assessment Module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Risk</h2>

    <% using (Html.BeginForm()) { %>

    <%: Html.ValidationSummary() %>

    <%: Html.HiddenFor(a=>a.Risk.ProjectId) %> 

    <h3>Threat Source</h3>

    <%: Html.TextAreaFor(a=>a.Risk.Source) %>

    <h3>Vulnerability</h3>

    <%: Html.TextAreaFor(a=>a.Risk.Vulnerability) %>

    <h3>Likelihood</h3>

    <%= this.Select("Risk.Likelihood").Class("risk-level-calc").Options(Model.RiskLevels, x=>x.id, x=>x.Name).FirstOption("--Select a Likelihood Level--") %>

    <h3>Impact</h3>

    <strong>Impact on Security Goal</strong>
    <%= this.Select("Risk.Impact").Options(Model.Impacts,x=>x.id, x=>x.Name).FirstOption("--Select an Impact--") %>
    <br />
    <strong>Magnitude of Impact</strong>
    <%= this.Select("Risk.Magnitude").Class("risk-level-calc").Options(Model.RiskLevels, x => x.id, x => x.Name).FirstOption("--Select a Magnitude of Impact--")%>

    <h3>Risk Level</h3>

    <span id="risk_level" style='<%: !string.IsNullOrEmpty(Model.RiskLevelColor) ? string.Format("color: {0};", Model.RiskLevelColor) : string.Empty  %>'>
    <%: Model.Risk.RiskLevel == null ? "n/a" : Model.Risk.RiskLevel.Name %>
    </span>

    <h3>&nbsp;</h3>
    <p>
    <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
    <%: Html.ActionLink<NIST800_30Controller>(a => a.Index(Model.ProjectStep.Id, Model.Project.id), "Cancel", new { @class = "button ui-state-default ui-corner-all" })%>
    </p>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

    <style type="text/css">
        textarea {width: 600px;}
        strong {display:inline-block; width:200px; margin-top: 1em;}
    </style>

    <script type="text/javascript">

        $(function () {
            $(".risk-level-calc").change(function () {

                var url = '<%: Url.Action("DetermineRiskLevel", "NIST800_30") %>';

                var likelihood = $("#Risk_Likelihood").val();
                var magnitude = $("#Risk_Magnitude").val();

                if (likelihood == "" || magnitude == "") return;

                $.getJSON(url, { likelihood: likelihood, magnitude: magnitude }, function (result) {
                    $("#risk_level").html(result.LevelName);
                    $("#risk_level").css("color", result.Color);
                });
            });
        });

    </script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<NIST800_30Controller>(a=>a.Index(Model.ProjectStep.Id,Model.Project.id), "NIST 800-30 Home", new {@class="button ui-state-default"}) %>

</asp:Content>

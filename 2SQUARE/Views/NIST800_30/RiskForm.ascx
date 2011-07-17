<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.NIST800_30EditViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

    <style type="text/css">
        textarea {width: 600px;}
        strong {display:inline-block; width:200px; margin-top: 1em;}
    </style>

    <script type="text/javascript">

        $(function () {
            $(".risk-level-calc").change(function () {

                var url = '<%: Url.Action("DetermineRiskLevel", "NIST800_30") %>';

                var likelihood = $("#likelihoodId").val();
                var magnitude = $("#magnitudeId").val();

                if (likelihood == "" || magnitude == "") return;

                $.getJSON(url, { likelihood: likelihood, magnitude: magnitude }, function (result) {
                    $("#risk_level").html(result.LevelName);
                    $("#risk_level").css("color", result.Color);
                });
            });
        });

    </script>


    <%: Html.ValidationSummary("Please correct the following errors.") %>

    <% using (Html.BeginForm()) { %>

    <h3>Name</h3>

    <%: Html.TextBoxFor(a=>a.Risk.Name) %>

    <h3>Threat Source</h3>

    <%: Html.TextAreaFor(a=>a.Risk.Source) %>

    <h3>Vulnerability</h3>

    <%: Html.TextAreaFor(a=>a.Risk.Vulnerability) %>

    <h3>Likelihood</h3>

    <%= this.Select("likelihoodId").Class("risk-level-calc").Options(Model.RiskLevels, x => x.Id, x => x.Name).FirstOption("--Select a Likelihood Level--").Selected(Model.Risk.Likelihood.Id)%>

    <h3>Impact</h3>

    <strong>Impact on Security Goal</strong>
    <%= this.Select("impactId").Options(Model.Impacts, x => x.Id, x => x.Name).FirstOption("-1", "--Select an Impact--").Selected(Model.Risk.Impact.Id.ToString()) %>
    <br />
    <strong>Magnitude of Impact</strong>
    <%= this.Select("magnitudeId").Class("risk-level-calc").Options(Model.RiskLevels, x => x.Id, x => x.Name).FirstOption("--Select a Magnitude of Impact--").Selected(Model.Risk.Magnitude.Id) %>

    <h3>Risk Level</h3>

    <span id="risk_level" style='<%: !string.IsNullOrEmpty(Model.RiskLevelColor) ? string.Format("color: {0};", Model.RiskLevelColor) : string.Empty  %>'>
    <%: Model.Risk.RiskLevel == null ? "n/a" : Model.Risk.RiskLevel.Name %>
    </span>

    <h3>&nbsp;</h3>
    <p>
    <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
    <%: Html.ActionLink<NIST800_30Controller>(a => a.Index(Model.ProjectStep.Id, Model.Project.Id), "Cancel", new { @class = "button ui-state-default ui-corner-all" })%>
    </p>
    <% } %>
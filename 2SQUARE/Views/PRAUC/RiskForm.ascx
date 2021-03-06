﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.PRAUCEditViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

    <%: Html.ValidationSummary("Please correct the following errors.") %>

    <% using (Html.BeginForm()) { %>
    
        <ul class="entry-form">
            <li><strong>Description:</strong>
                <%: Html.TextAreaFor(a=>a.Risk.Description) %>
            </li>
            <li><strong>Likelihood:</strong>
                <%= this.Select("LikelihoodId").Options(Model.RiskLevels, x=>x.Id, x=>x.Name).Selected(Model.Risk.Likelihood != null ? Model.Risk.Likelihood.Id : string.Empty).FirstOption("--Select Likelihood--") %>
            </li>
            <li><strong>Damage:</strong>
                <%= this.Select("DamageId").Options(Model.RiskLevels, x=>x.Id, x=>x.Name).Selected(Model.Risk.Damage != null ? Model.Risk.Damage.Id : string.Empty).FirstOption("--Select Damange--") %>
            </li>
            <li><strong>Cost:</strong>
                <%: Html.TextBoxFor(a=>a.Risk.Cost) %>
                <em>*Values should be between 1 and 9, where 1 is low and 9 is high</em>
            </li>
            <li><strong></strong>
                    <%: Html.SubmitButton("Save", "Save", new {@class="button ui-corner-all ui-state-default"}) %>
                    <%: Html.ActionLink<PRAUCController>(a => a.Index(Model.ProjectStep.Id, Model.Project.Id), "Cancel", new { @class = "button ui-state-default ui-corner-all" })%>
            </li>
        </ul>

    <% } %>
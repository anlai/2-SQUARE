<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.PRETViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Privacy Requirements Elicitation Technique (PRET) Module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>PRET Module</h2>

    <p>Please answer the following questions:</p>

    <%: Html.ValidationMessage("Please correct the following errors:") %>

    <% using (Html.BeginForm()) { %>
    <ol>
        <% foreach (var a in Model.PretQuestions.Where(b=>!b.Subquestion).OrderBy(b => b.Order)) { %>
            <% Html.RenderPartial("QuestionPartial", a); %>

            <% if (a.Children.Any()) { %>
                <ol style="list-style-type: lower-roman; ">
                    <% foreach (var b in a.Children) { %>
                
                        <% Html.RenderPartial("QuestionPartial", b); %>    

                    <% } %>
                </ol>
            <% } %>
        <% } %>
    </ol>
    
    <%: Html.SubmitButton("Run", "Run", new {@class="button ui-corner-all ui-state-default"}) %>
    <%: Html.ActionLink<PRETController>(a=>a.Index(Model.ProjectStep.Id, Model.Project.id), "Cancel", new {@class="button ui-state-default ui-corner-all"}) %>

    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

    <style type="text/css">
        ul {list-style:none;}
    </style>

    <script type="text/javascript">
        $(function () {
            $.each($(".question"), function (index, item) {

                var questionId = $(item).find("#QuestionId");
                questionId.attr("id", "pretQuestionAnswers[" + index + "]_QuestionId");
                questionId.attr("name", "pretQuestionAnswers[" + index + "].QuestionId");

                var subQuestion = $(item).find("#IsSubQuestion");
                subQuestion.attr("id", "pretQuestionAnswers[" + index + "]_IsSubQuestion");
                subQuestion.attr("name", "pretQuestionAnswers[" + index + "].IsSubQuestion");

                $.each($(item).find("#AnswerId"), function (index2, item2) {
                    $(item2).attr("id", "pretQuestionAnswers[" + index2 + "]_AnswerId");
                    $(item2).attr("name", "pretQuestionAnswers[" + index + "].AnswerId");
                });

            });
        });
    </script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

<%: Html.ActionLink<PRETController>(a=>a.Index(Model.ProjectStep.Id, Model.Project.id), "Back to PRET Home", new {@class="button ui-state-default"}) %>

</asp:Content>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Core.PRET.PRETQuestion>" %>

<li class="question">
    <%: Model.Question %>
    <%: Html.Hidden("QuestionId", Model.Id) %>
    <%: Html.Hidden("IsSubQuestion", Model.SubQuestion)%>
    <ul>
        <% foreach (var answer in Model.PretAnswers) { %>
            <li>
                <%: Html.RadioButton("AnswerId", answer.Id) %>
                <%: answer.Answer %>
            </li>
        <% } %>
    </ul>

</li>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.PRETQuestion>" %>

<li class="question">
    <%: Model.Question %>
    <%: Html.Hidden("QuestionId", Model.Id) %>
    <%: Html.Hidden("IsSubQuestion", Model.Subquestion)%>
    <ul>
        <% foreach (var answer in Model.PRETAnswers) { %>
            <li>
                <%: Html.RadioButton("AnswerId", answer.Id) %>
                <%: answer.Answer %>
            </li>
        <% } %>
    </ul>

</li>

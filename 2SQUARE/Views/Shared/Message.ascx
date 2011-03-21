<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.MessageModel>" %>

<div class="ui-widget messages">
    <div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; margin-bottom: 10px; padding: 0pt 0.7em;">
        <p>
            <!-- Figure out whether or not to show an icon -->
            <% if (Model.ShowErrorIcon) { %>
                <span class="ui-icon ui-icon-alert" style="float:left; margin-right: 0.3em;"></span>
            <% } else if (Model.ShowWarningIcon) { %>
                <span class="ui-icon ui-icon-info" style="float:left; margin-right: 0.3em;"></span>
            <% } %>
            
            <% if (Model.Encode) { %>
            <span id="errormessage-container"><%: Model.Message %></span>
            <% } else { %>
                <span id="errormessage-container"><%= Model.Message %></span>
            <% } %>           
        </p>
    </div>
</div>
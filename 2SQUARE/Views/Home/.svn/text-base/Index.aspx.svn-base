<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.LogOnModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>2Square</title>
    <link href="<%: Url.Content("~/Content/Site.css") %>" rel="stylesheet" type="text/css" />
</head>

<body>
    <div id="header">
        <span class=logos>
            <img src="<%= Url.Content("~/Images/logo.png") %>" alt="UC Davis" />
        </span>

    <div class=page>

        <div class="main_box corners" style="height: 300px">
        <div class="box_header corners">
            Welcome!
        </div>
        <div class="box_content">
            <p>
                blah blha blha
            </p>
        </div>
        </div>
        <div class="right_box corners" style="height: 200px;">
            <div class="box_header corners">Account Information</div>
            <div class="box_content">
            <%--<% using (Html.BeginForm("LogOn", "Account", FormMethod.Post)) { %>                        --%>
            <% using (Html.BeginForm("LogOn", "Account", FormMethod.Post, new {style="width: 200px;"})) { %>
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.UserName) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.UserName) %>
                        <%: Html.ValidationMessageFor(m => m.UserName) %>
                    </div>
                
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Password) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.PasswordFor(m => m.Password) %>
                        <%: Html.ValidationMessageFor(m => m.Password) %>
                    </div>
                
                    <div class="editor-label">
                        <%: Html.CheckBoxFor(m => m.RememberMe) %>
                        <%: Html.LabelFor(m => m.RememberMe) %>
                    </div>
                
                    <div>
                        
                    </div>

                        <input type="submit" class="button corners" style="display:inline;" value="Log On" />
                        <%: Html.ActionLink("Register", "Register", "Account", null, new { @class = "corners button", style="position:relative;"})%>
                </div>
            
            <% } %>
            </div>    
        </div>

        <div class="right_box">
            some more stuff
        </div>
    </div>
</body>
</html>


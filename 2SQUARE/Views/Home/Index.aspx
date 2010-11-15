<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.LogOnModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>2Square</title>
    <link href="<%: Url.Content("~/Content/Site.css") %>" rel="stylesheet" type="text/css" />
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/smoothness/jquery-ui.css" rel="Stylesheet" type="text/css" />
</head>

<body>
    <div id="header">
        <span class=logos>
            <img src="<%= Url.Content("~/Images/logo.png") %>" alt="UC Davis" />
        </span>
    </div>

    <div class=page>

        <div class="large-left-box ui-corner-all" style="height: 300px">
        <div class="box-header ui-corner-all ui-widget-header">
            Welcome!
        </div>
        <div class="box-content">
            <p>
                blah blha blha
            </p>
            <p>
                <%: Html.ActionLink<ProjectController>(a=>a.Index(), "Projects") %>
            </p>
        </div>
        </div>
        <div class="small-right-box ui-corner-all" style="height: 200px;">
            <div class="box-header ui-widget-header ui-corner-all">Account Information</div>
            <div class="box-content">
            <% if (Request.IsAuthenticated) { %>
                Welcome <%: Page.User.Identity.Name %>

                <%: Html.ActionLink<AccountController>(a=>a.LogOff(), "Logout", new {@class="ui-button ui-corner-all"}) %>
            <% } else { %>            
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

                            <input type="submit" class="ui-corner-all button ui-state-default" style="display:inline;" value="Log On" />
                            <%: Html.ActionLink("Register", "Register", "Account", null, new { @class = "ui-corner-all button ui-state-default", style="position:relative;"})%>
                    </div>
            
                <% } %>
            <% } %>
            </div>    
        </div>
    
</body>
</html>


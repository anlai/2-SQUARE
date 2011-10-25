<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.LogOnModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>2Square</title>
    
    <link href="<%: Url.Content("~/Content/Site.css") %>" rel="stylesheet" type="text/css" />
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/smoothness/jquery-ui.css" rel="Stylesheet" type="text/css" />

    <style type="text/css">
        input[type='text'], input[type='password'] {width: 150px;}
    </style>

    <script type="text/javascript" src='<%: Url.Content("~/Scripts/jquery-1.5.1.min.js") %>'></script>
    <script type="text/javascript" src='<%: Url.Content("~/Scripts/jquery-ui-1.8.13.min.js") %>'></script>

    <script type="text/javascript">
        $(function () {
            $(".button").button();
        });
    </script>
</head>

<body>
    <div id="header">
        <span class=logos>
            <img src="<%= Url.Content("~/Images/logo.png") %>" alt="2Square" />
        </span>
    </div>

    <div class=page>

        <div class="large-left-box ui-corner-all" style="height: 300px">
        <div class="box-header ui-corner-all ui-widget-header">
            Welcome!
        </div>
        <div class="box-content">
            
            <p>
            Welcome to 2-SQUARE!  2-SQUARE is a tool to support Security Quality Requirements Eliciation (SQUARE) Method developed by
            the Software Engineering Institute (SEI) at Carnegie Mellon University.  This tool supports requirements engineering in
            the security and privacy fields.
            </p>

            <p>
            More information on SQUARE can be found <a href="#">here.</a>
            </p>

            <p>
                <%: Html.ActionLink<ProjectController>(a=>a.Index(), "Projects") %>
            </p>
        </div>
        </div>
        <div class="small-right-box ui-corner-all" style="height: 225px;">
            <div class="box-header ui-widget-header ui-corner-all">Account Information</div>
            <div class="box-content">
            <% if (Request.IsAuthenticated) { %>
                Welcome <%: Page.User.Identity.Name %>

                <%: Html.ActionLink<AccountController>(a=>a.LogOff(), "Logout", new {@class="button"}) %>
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

                            <input type="submit" class="button" style="display:inline;" value="Log On" />
                            <%: Html.ActionLink("Register", "Register", "Account", null, new { @class = "button", style="position:relative;"})%>
                    </div>
            
                <% } %>
            <% } %>
            </div>    
        </div>
    
</body>
</html>


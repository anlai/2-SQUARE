<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">
    
        <div class="section-header"><h2>Reinitialize Database?</h2></div>

        <div class="section-contents">
            
            <p>Are you sure you want to reinitialize the database values?  This will wipe all existing information.</p>
        
            <p>
                <% using(Html.BeginForm()) { %>
                
                    <%: Html.ValidationSummary() %>

                    <ul class="entry-form">
                        <li><strong>Password:</strong><input type="text" id="password" name="password"/></li>
                        <li><strong>&nbsp;</strong><input type="submit" value="Confirm" class="button"/><%: Html.ActionLink("Cancel", "Index", "Home", new {}, new {@class="button"}) %></li>
                    </ul>

                <% } %>

                
            </p>

        </div>

    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

    <style type="text/css">
        form {display: inline-block;}
    </style>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
</asp:Content>

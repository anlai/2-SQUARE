<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Project>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Project Permissions
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">
    
        <div class="section-header">
            <div class="col1"><h2>Project Permissions</h2></div>
            <div class="col2">
                <%: Html.ActionLink("Add Permission", "AddPermission", new{id=Model.Id}, new {@class="button"}) %>
            </div>
        </div>

        <div class="section-contents">

            <table>
    
                <thead>
                    <tr>
                        <th></th>
                        <th>Login</th>
                        <th>Role</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
        
                    <% foreach (var worker in Model.ProjectWorkers) { %>
            
                        <tr>
                            <td>
                                <% using (Html.BeginForm("RemovePermission", "Project")) { %>
                        
                                    <%: Html.Hidden("id", worker.Id) %>
                                    <input type="submit" class="button" value="Remove"/>

                                <% } %>
                            </td>
                            <td><%: worker.User.Username %></td>
                            <td><%: worker.Role.Name %></td>
                            <td></td>
                        </tr>

                    <% } %>

                </tbody>

            </table>
        
        </div>

    </div>

    


        

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink("Back to Project", "Details", "Project", new {id=Model.Id}, new {@class="nav-button"}) %>

</asp:Content>

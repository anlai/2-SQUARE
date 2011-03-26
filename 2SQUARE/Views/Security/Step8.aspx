<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step8ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.ProjectStep.Step.SquareType.Name %> Step 8 - Prioritize Requirements
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Model.ProjectStep.Step.SquareType.Name %> Step 8 - Prioritize Requirements</h2>

    <table id="requirements">
        <thead>
            <tr>
                <th>Id</th>
                <th>Requirement</th>
                <th>Essential</th>
                <th>Cateogry</th>
                <th>Priority</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var a in Model.Requirements.OrderBy(b=>b.Order)) { %>
                <tr data-id='<%: a.id %>'>
                    <td><%: a.RequirementId %></td>
                    <td><%: a.Requirement1 %></td>
                    <td><%: a.Essential %></td>
                    <td><%: a.Category.Name %></td>
                    <td>
                        <input type="text" class="priority" data-id="<%: a.id %>" value="<%: a.Priority %>" />
                    </td>
                </tr>
            <% } %>
        </tbody>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.tablednd_0_5.js") %>"></script>

    <script type="text/javascript">
        $(function () {
            $("#requirements").tableDnD({ onDrop: function (table, row) {

                var rows = table.tBodies[0].rows;
                var updateOrder = "";
                for (var i = 0; i < rows.length; i++) {
                    updateOrder += "," + $(rows[i]).data("id");
                }

                // strip out the first ","
                updateOrder = updateOrder.substring(1, updateOrder.length);

                var url = '<%: Url.Action("UpdateRequirementOrder") %>';
                $.post(url, {projectId:<%: Model.Project.id %>,squareTypeId: <%: Model.ProjectStep.Step.SquareTypeId %>,requirementIds: updateOrder }
                    , function (result) { 
                        if(result) { alert("save successful"); } else {alert("failed to save");}
                    });
            }
            });
        });
    </script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    
    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.id), string.Format("{0} Home", Model.Project.Name), new {@class="button ui-state-default"}) %>

</asp:Content>

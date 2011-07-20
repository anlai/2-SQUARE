<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<_2SQUARE.Models.Step8ViewModel>" %>

<style type="text/css">
    input[type="text"] {width: 30px;}
</style>

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

            var url = '<%: Url.Action("UpdateRequirementOrder", "Requirement") %>';
            $.post(url, {projectId:<%: Model.Project.Id %>,squareTypeId: <%: Model.ProjectStep.Step.SquareType.Id %>, requirements: updateOrder }
                , function (result) { 
                    if(result) { alert("save successful"); } else {alert("failed to save");}
                });
            }
        });

        $(".priority").blur(function(){
            var url = '<%: Url.Action("UpdatePriority", "Requirement") %>';

            var reqId = $(this).data("id");
            var priority = $(this).val();

            $.post(url, {requirementId: reqId, priority: priority }, function(result){
                if(result) { alert("save successful"); } else {alert("failed to save");}
            });
        });
    });
</script>


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
            <tr data-id='<%: a.Id %>'>
                <td><%: a.RequirementId %></td>
                <td><%: a.RequirementText %></td>
                <td><%: a.Essential %></td>
                <td><%: a.Category.Name %></td>
                <td>
                    <input type="text" class="priority" data-id="<%: a.Id %>" value="<%: a.Priority %>" />
                </td>
            </tr>
        <% } %>
    </tbody>
</table>
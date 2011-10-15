<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectStep>" %>

<div class="section-container">

    <div class="section-header">
        
        <div class="col1">
            <h3>Project Step Files</h3>
        </div>
        <div class="col2">
            <input type="button" class="button" id="ps-add-file" value="Add File"/>
        </div>

    </div>    

    <div class="section-contents">
        <table id="ps-files">
            <thead>
                <tr>
                    <td></td>
                    <td>File Name</td>
                    <td>Notes</td>
                    <td>Date Created</td>
                    <td></td>
                </tr>
            </thead>
            <tbody>
                <% foreach (var file in Model.ProjectStepFiles) { %>
                    <tr>    
                        <td>View</td>
                        <td><%: file.FileName %></td>
                        <td><%: file.Notes %></td>
                        <td><%: file.DateCreated.ToString("d") %></td>
                        <td><button class="button delete-file" data-id="<%: file.Id %>">Delete</button></td>
                    </tr>
                <% } %>
            </tbody>
        </table>
    </div>

</div>

<div id="ps-file-dialog" title="Upload Project Step File">
<%--    <form action="<%: Url.Action("SaveFile", "ProjectStepFile") %>" method="POST" enctype="multipart/form-data">
    Notes:<br/>
    <textarea id="ps-file-notes"></textarea><br/>
    <input type="file" id="ps-file" name="file" />
    </form>--%>

    <textarea id="ps-file-notes"></textarea>

    <div id="ps-file-uploader"></div>

</div>

<link href="<%: Url.Content("~/Content/fileuploader.css") %>" type="text/css" rel="stylesheet"/>
<%--http://valums.com/ajax-upload/--%>
<script type="text/javascript" src="<%: Url.Content("~/Scripts/fileuploader.js") %>"></script>
<script type="text/javascript" src="https://raw.github.com/douglascrockford/JSON-js/master/json_parse.js"></script>

<script type="text/javascript">

    $(function () {

        $("#ps-file-dialog").dialog({
            modal: true,
            autoOpen: false,
            width: 500,
            buttons: {
                "Close": function () { $(this).dialog("close"); }
            }
        });

        $("#ps-add-file").click(function () { $("#ps-file-notes").val(""); $("#ps-file-dialog").dialog("open"); });

        var uploader = new qq.FileUploader({
            element: $("#ps-file-uploader")[0],
            action: '<%: Url.Action("SaveFile", "ProjectStepFile", new {id=Model.Id}) %>',
            //params: { notes: $("#ps-file-notes").val() },
            onSubmit: function (id, fileName) {

                var note = $("#ps-file-notes").val();
                uploader.setParams({ notes:note });

            },
            onComplete: function (id, fileName, responseJSON) {
                $("#ps-file-dialog").dialog("close");

                var parsed = responseJSON;
                var file = [{ id: parsed.id, fileName: parsed.fileName, notes: parsed.notes, dateCreated: parsed.dateCreated}];

                $("#psfile-template").tmpl(file).appendTo("#ps-files tbody");
                $("#ps-file-dialog").dialog("close");

                $(".button").button();

            }
        });

        $(".delete-file").live("click", function () {

            var $that = $(this);

            var url = '<%: Url.Action("DeleteFile", "ProjectStepFile") %>';
            $.post(url, { id: '<%: Model.Id %>', fileId: $that.data("id") }, function (result) {

                if (result == true) {
                    $that.parents("tr").remove();
                }


            });

        });

    });

</script>

<script type="text/x-jquery-tmpl" id="psfile-template">
    <tr>    
        <td>View</td>
        <td>${fileName}</td>
        <td>${notes}</td>
        <td>${dateCreated}</td>
        <td><button class="button delete-file" data-id="${id}">Delete</button></td>
    </tr>
</script>
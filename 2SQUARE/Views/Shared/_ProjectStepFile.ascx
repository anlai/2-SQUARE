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
    <form action="<%: Url.Action("SaveFile", "ProjectStepFile") %>" method="POST" enctype="multipart/form-data">
    Notes:<br/>
    <textarea id="ps-file-notes"></textarea><br/>
    <input type="file" id="ps-file" name="file" />
    </form>
</div>

<link href="<%: Url.Content("~/Scripts/uploadify-v2.1.4/uploadify.css") %>" type="text/css" rel="stylesheet"/>
<script type="text/javascript" src="<%: Url.Content("~/Scripts/uploadify-v2.1.4/swfobject.js") %>"></script>
<script type="text/javascript" src="<%: Url.Content("~/Scripts/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js") %>"></script>
<script type="text/javascript" src="https://raw.github.com/douglascrockford/JSON-js/master/json_parse.js"></script>

<script type="text/javascript">

    $(function () {

        $("#ps-file-dialog").dialog({
                modal: true,
                autoOpen: false,
                width: 500,
                buttons: {
                    "Save": function() {
                    
                        $("#ps-file").uploadifySettings('scriptData', { 'id': <%: Model.Id %> , 'notes': $("#ps-file-notes").val() });
                        $("#ps-file").uploadifyUpload();
                        
                    },
                    "Cancel": function() { $(this).dialog("close"); }
                }
            });
        
        $("#ps-add-file").click(function() { $("#ps-file-notes").val(""); $("#ps-file-dialog").dialog("open");});
        
        $("#ps-file").uploadify({

            'uploader': '<%: Url.Content("~/Scripts/uploadify-v2.1.4/uploadify.swf") %>',
            'script': '<%: Url.Action("SaveFile", "ProjectStepFile") %>',
            'cancelImg': '<%: Url.Content("~/Scripts/uploadify-v2.1.4/cancel.png") %>',
            'auto': false,
            'multi' : false,
            onError: function (a, b, c, d) {
                 if (d.status == 404)  
                   alert("Could not find upload script. Use a path relative to: "+"<?= getcwd() ?>");  
                 else if (d.type === "HTTP")  
                   alert("error "+d.type+": "+d.status);  
                 else if (d.type ==="File Size")  
                   alert(c.name+" "+d.type+" Limit: "+Math.round(d.sizeLimit/1024)+"KB");  
                 else  
                   alert("error "+d.type+": "+d.text);  
                 },
            onComplete: function(event, id, fileObj, response, data) {
                var parsed = JSON.parse(response);
                var file = [{ id: parsed.id, fileName: parsed.fileName, notes: parsed.notes, dateCreated: parsed.dateCreated }];
                
                $("#psfile-template").tmpl(file).appendTo("#ps-files tbody");
                $("#ps-file-dialog").dialog("close");

                $(".button").button();
            }
        });

        $(".delete-file").live("click", function() {

            var $that = $(this);
            
            var url = '<%: Url.Action("DeleteFile", "ProjectStepFile") %>';
            $.post(url, { id: '<%: Model.Id %>', fileId: $that.data("id") }, function(result) {
                
                if (result == true) {
                    $that.parents("tr").remove();
                }
                else {
                    alert("there was an error deleting the file.");
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
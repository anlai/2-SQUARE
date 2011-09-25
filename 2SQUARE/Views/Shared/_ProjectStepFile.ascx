<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectStep>" %>

<input type="button" class="button" id="ps-add-file" value="Add File" style="float:right;"/>

<h3>Project Step Files</h3>

<div id="ps-file-dialog" title="Upload Project Step File">
    <form action="<%: Url.Action("SaveFile", "ProjectStepFile") %>" method="POST" enctype="multipart/form-data">
    <textarea id="ps-file-notes"></textarea>
    <input type="file" id="ps-file" name="file" />
    <%--<a href="javascript:$('#ps-file').uploadifyUpload();">Upload File</a>--%>
    </form>
</div>


<link href="<%: Url.Content("~/Scripts/uploadify-v2.1.4/uploadify.css") %>" type="text/css" rel="stylesheet"/>
<script type="text/javascript" src="<%: Url.Content("~/Scripts/uploadify-v2.1.4/swfobject.js") %>"></script>
<script type="text/javascript" src="<%: Url.Content("~/Scripts/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js") %>"></script>

<script type="text/javascript">

    $(function () {

        $("#ps-file-dialog").dialog({
                autoOpen: false,
                width: 500,
                buttons: {
                    "Save": function() {

                        //$("#ps-file").uploadifySettings({ 'scriptData': { 'id': <%: Model.Id %> , 'notes': 'blah blah blah' } });
                        $("#ps-file").uploadifySettings('scriptData', { 'id': <%: Model.Id %> , 'notes': $("#ps-file-notes").val() });
                        $("#ps-file").uploadifyUpload();
                        
                    },
                    "Cancel": function() { $(this).dialog("close"); }
                }
            });
        
        $("#ps-add-file").click(function() { $("#ps-file-dialog").dialog("open");});
        
        $("#ps-file").uploadify({

            'uploader': '<%: Url.Content("~/Scripts/uploadify-v2.1.4/uploadify.swf") %>',
            'script': '<%: Url.Action("SaveFile", "ProjectStepFile") %>',
            'cancelImg': '<%: Url.Content("~/Scripts/uploadify-v2.1.4/cancel.png") %>',
            'auto': false,
            'multi' : false
            });

    });

</script>
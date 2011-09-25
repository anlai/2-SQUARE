<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectStep>" %>

<h3>Project Step Files</h3>

<form action="<%: Url.Action("SaveFile", "ProjectStepFile") %>" method="POST" enctype="multipart/form-data">
<input type="file" id="ps-file" name="file" />
<a href="javascript:$('#ps-file').uploadifyUpload();">Upload File</a>
</form>


<link href="<%: Url.Content("~/Scripts/uploadify-v2.1.4/uploadify.css") %>" type="text/css" rel="stylesheet"/>
<script type="text/javascript" src="<%: Url.Content("~/Scripts/uploadify-v2.1.4/swfobject.js") %>"></script>
<script type="text/javascript" src="<%: Url.Content("~/Scripts/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js") %>"></script>

<script type="text/javascript">

    $(function () {

        $("#ps-file").uploadify({

            'uploader': '<%: Url.Content("~/Scripts/uploadify-v2.1.4/uploadify.swf") %>',
            'script': '<%: Url.Action("SaveFile", "ProjectStepFile") %>',
            'cancelImg': '<%: Url.Content("~/Scripts/uploadify-v2.1.4/cancel.png") %>',
            'auto': false,
            'multi' : false,
            'scriptData': {id: <%: Model.Id %>},
            onError: function (a, b, c, d) {

                debugger;
                
                if (d.status == 404)
                    alert("Could not find upload script. Use a path relative to: " + "<?= getcwd() ?>");
                else if (d.type === "HTTP")
                    alert("error " + d.type + ": " + d.status);
                else if (d.type === "File Size")
                    alert(c.name + " " + d.type + " Limit: " + Math.round(d.sizeLimit / 1024) + "KB");
                else
                    alert("error " + d.type + ": " + d.text);
            }  
        });

    });

</script>


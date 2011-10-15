<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectStep>" %>

<div id="file-trigger" class="ui-widget-header">Project Step Files</div>

<div id="file-dialog" title="Project Step Files">

    <% Html.RenderPartial("_ProjectStepFile"); %>

</div>

<div id="note-trigger" class="ui-widget-header">Project Step Notes</div>

<div id="note-dialog" title="Project Step Notes">

    <% Html.RenderPartial("_ProjectStepNotes"); %>

</div>

<script type="text/javascript">

    $(function () {

        $("#file-dialog").dialog({ autoOpen: false, width: 800 });

        $("#note-dialog").dialog({ autoOpen: false, width: 800 });

        $("#file-trigger").click(function () {
            $("#file-dialog").dialog("open");
        });

        $("#note-trigger").click(function () {
            $("#note-dialog").dialog("open");
        });

    });

</script>

<style type="text/css">

#file-trigger, #note-trigger {position: fixed; bottom: 0; border: 1px solid #CCCCCC; padding: .5em; cursor: pointer;}
#file-trigger:hover, #note-trigger:hover {background-color: lightgray; background-image: none;}
#file-trigger {right: 50px;}
#note-trigger {right: 200px;}

</style>
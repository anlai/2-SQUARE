<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectStep>" %>

<%--<%: Html.ActionLink<ProjectTermController>(a=>a.ViewPredefinedTerms(Model.ProjectStep.Id, Model.Project.Id), "View Predefined Terms", new {@class="button ui-corner-all ui-state-default", style="float:right;"}) %>--%>

<input type="button" class="button" id="ps-add-note" value="Add Note" style="float:right;"/>

<h3>Project Step Notes</h3>

<table id="project-step-notes">
    <thead>
        <tr>
            <td></td>
            <td>Note</td>
            <td>Date</td>
            <td>User</td>
        </tr>
    </thead>
    <tbody>
    
        <% foreach (var note in Model.ProjectStepNotes) { %>
            <tr>
                <td><input type="button" class="button delete-note" value="Delete" data-id=<%: note.Id %> /></td>
                <td><%: note.Description %></td>
                <td><%: note.DateCreated.ToString("d") %></td>
                <td><%: note.UserId %></td>
            </tr>
        <% } %>

    </tbody>
</table>

<div id="ps-note-dialog" title="Add Note">
<textarea id="ps-note"></textarea>
</div>

<script type="text/javascript">

    $(function () {

        $("#ps-note-dialog").dialog({
            autoOpen: false,
            buttons: {
                Save: function () {

                    var note = $("#ps-note").val();
                    var url = '<%: Url.Action("SaveNotes", "ProjectStepNote") %>';

                    var that = $(this);
                    
                    $.post(url, { id: <%: Model.Id %>, notes: note }, function(result) {
                        
                        if (result != false) {

                            var note = [{NoteId: result.id, NoteDescription: result.note, NoteDateCreated: result.dateCreated, NoteUser: result.user}];

                            $("#note-template").tmpl(note).appendTo("#project-step-notes tbody");

                            that.dialog("close");
                        }
                        else {
                            alert("There was an error saving the notes.");
                        }
                    });

                },
                Cancel: function () { $(this).dialog("close"); }
            }
        });

        $("#ps-add-note").click(function () {

            // blank out the notes field
            $("#ps-note").val("");

            // open the dialog
            $("#ps-note-dialog").dialog("open");

        });

        $(".delete-note").click(function() {

            var url = '<%: Url.Action("DeleteNotes", "ProjectStepNote") %>';
            var noteId = $(this).data("id");

            var $that = $(this);
            
            $.post(url, { id: <%: Model.Id %> , noteId: noteId }, function(result) {
                
                if (result == true) {
                    $that.parents("tr").remove();
                }
                else {
                    alert("There was an error deleting the notes.");
                }
                
            });
            
        });
        
    });

</script>

<script type="text/x-jquery-tmpl" id="note-template">

    <tr>
        <td><input type="button" class="button delete-note" value="Delete" data-id=${NoteId} /></td>
        <td>${NoteDescription}</td>
        <td>${NoteDateCreated}</td>
        <td>${NoteUser}</td>
    </tr>

</script>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Models.Step4ViewModel>" %>
<%@ Import Namespace="_2SQUARE.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Privacy Step 4 - Risk Assessment
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1"><h2>Privacy Step 4 - Risk Assessment</h2></div>
            <div class="col2"></div>

        </div>    

        <div class="section-contents">
             <table cellpadding="5px">
                <thead>
                    <tr>
                        <th></th>
                        <th>Name</th>
                        <th>Source</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var a in Model.AssessmentTypes) { %>
                        <tr class="definition-row">
                            <td class="button-cell">
                                <%: Html.ActionLink<SecurityController>(b=>b.SelectAssessmentType(Model.ProjectStep.Id, Model.Project.Id, a.Id), "Select", new {@class="button"}) %>
                            </td>
                            <td><%: a.Name %></td>
                            <td><%: a.Source %></td>
                        </tr>
                    <%} %>
                </tbody>
            </table>       
        </div>

    </div>

    



    <div class="pstep-container">
    <% Html.RenderPartial("_ProjectStepNotes", Model.ProjectStep); %>

    <% Html.RenderPartial("_ProjectStepFile", Model.ProjectStep); %>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">

    <%: Html.ActionLink<ProjectController>(a=>a.Details(Model.Project.Id), Model.Project.Name + " Home", new {@class="nav-button"}) %>

</asp:Content>

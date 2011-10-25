<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<_2SQUARE.Controllers.GenericRiskViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Risk
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">
    
        <div class="section-header"><h2>Create Risk</h2></div>

        <div class="section-contents">
        
            <% using (Html.BeginForm()) { %>
            
                <%: Html.ValidationSummary() %>

                <ul class="entry-form">
                    <li><strong>Name:</strong>
                        <%: Html.TextBoxFor(a=>a.Risk.Name) %>
                    </li>
                    <li><strong>Threat Source:</strong>
                        <%: Html.TextAreaFor(a=>a.Risk.Source) %>
                    </li>
                    <li><strong>Vulnerability:</strong>
                        <%: Html.TextAreaFor(a=>a.Risk.Vulnerability) %>
                    </li>
                    <li><strong>Risk Level:</strong>
                        <%= this.Select("RiskLevelId").Options(Model.RiskLevels,x=>x.Id,x=>x.Name).FirstOption("--Select Risk Level--").Selected(Model.Risk.RiskLevel != null ? Model.Risk.RiskLevel.Id : string.Empty) %>
                    </li>
                    <li><strong>&nbsp;</strong>
                        <input type="submit" value="Save" class="button"/>
                        <%: Html.ActionLink("Cancel", "Index", new {id=Model.ProjectStep.Id, projectId = Model.Project.Id}, new {@class="button"}) %>
                    </li>
                </ul>

            <% } %>

        </div>

    </div>

    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
</asp:Content>

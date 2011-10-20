<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PrivacyStep7
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1">
                <h2>Security Step 7 - Categorize Requirements</h2>
            </div>
            <div class="col2">
            </div>

        </div>    

        <div class="section-contents">
        
            <p>The aim of this step is to systematically categorize requirements to help in the next step of the process, requirements prioritization. This step also facilitates team discussion of the requirements and separates requirements from the constraints for the project.</p>

            <p>In this step, the requirements engineering team guides the stakeholders to categorize requirements through discussion. The requirements engineering team provides the stakeholders with a set of basic categories and explains the process of categorization. The stakeholders may use the given set or add new categories to the set.</p>

            <table>
                <tr>
                    <td></td>
                    <td>System Level</td>
                    <td>Software Level</td>
                    <td>Architectural Constraint</td>
                </tr>
                <tr>
                    <td>Essential</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Non-essential</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>

            <p>The above matrix provides a generic way of categorizing requirements. However, because a num-ber of privacy-related requirements have legal implications, the team may want to use a categori-zation that suits privacy requirements. One method for prioritizing legal requirements uses the following categories [Massey 2009]:</p>

            <ul>
                <li>nonlegal requirements</li>
                <li>legal requirements needing further refinement</li>
                <li>implementation-ready legal requirements</li>
            </ul>

        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

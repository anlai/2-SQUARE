<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PrivacyStep5
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
                <h2>Security Step 5 - Select Elicitation Technique</h2>

        </div>    

        <div class="section-contents">
            
            <p>In this step, the requirements engineering team selects one elicitation technique that is suitable for the project and the clients and that elicit all the requirements from the stakeholders [Mead 2005]. Some of the techniques that they consider are</p>

            <ul>
                <li>structured/unstructured interviews</li>
                <li>use/misuse cases</li>
                <li>facilitated meeting sessions, such as joint application development and the accelerated re-quirements method</li>
                <li>soft systems methodology</li>
                <li>issue-based information systems</li>
                <li>quality function deployment</li>
                <li>feature-oriented domain analysis</li>
            </ul>

            <p>To adapt SQUARE for privacy requirements elicitation, we suggest that the requirements engi-neering team use the PRET, a computer-aided technique that helps the requirements engineering team elicit and prioritize privacy requirements more efficiently [Miyazaki 2008]. This technique uses a database of privacy requirements based on privacy laws and regulations such as the OECD and PIPA. Using a questionnaire and a decision process, the tools create a list of privacy require-ments and their priorities. The PRET makes it faster and easier to elicit requirements and prevent leaks when the team is not familiar with the laws and regulations. However, the PRET currently does not contain all the privacy laws, and the database needs to be updated as the laws change. Also, because the PRET is a generic tool, the requirements are general, and the requirements en-gineering team needs to verify and tailor them to the specific needs of the project.</p>

        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

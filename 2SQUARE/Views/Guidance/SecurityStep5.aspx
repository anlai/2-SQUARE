<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SecurityStep5
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1">
                <h2>Security Step 5 - Select Elicitation Technique</h2>
            </div>
            <div class="col2">
            </div>

        </div>    

        <div class="section-contents">
        
            <p>
                The requirements engineering team must select an elicitation technique that is suitable for the
                client organization and project. Although this task may appear to be straightforward, it is often
                the case that multiple techniques will likely work for the same project. The difficulty is in
                choosing a technique that can adapt to the number and expertise of stakeholders, size and
                scope of the client project, and expertise of the requirements engineering team. It is extremely
                unlikely that any single technique will work for all projects under all circumstances,
                though previous experience has shown that the Accelerated Requirements Method (ARM)
                has been successful in eliciting security requirements.
            </p>

            <p>
                The following is a sample of elicitation techniques that may be appropriate:
            </p>

            <ul>
                <li>Structured/unstructured interviews</li>
                <li>Use/misuse cases</li>
                <li>Facilitated meeting sessions, such as Joint Application Development and the Accelerated Requirements Method</li>
                <li>Soft Systems Methodology</li>
                <li>Issue-Based Information Systems</li>
                <li>Quality Function Deployment</li>
                <li>Feature-Oriented Domain Analysis</li>
                <li>Controlled Requirements Expression</li>
                <li>Critical Discourse Analysis</li>
            </ul>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Responsibilities</h3></div>

        <div class="section-contents">
        
            <h3>Requirements Engineering Team</h3>

            <ol>
                <li>
                    Select an elicitation technique that is appropriate for the number and expertise of stakeholders,
                    size and scope of the project, and expertise of the requirements engineering
                    team.                
                </li>
                <li>
                    Document the rationale for the choice and make necessary preparations to execute the technique.
                </li>
            </ol>
            
        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Exit Criteria</h3></div>

        <div class="section-contents">
            
            <p>
                The requirements engineering team has selected an appropriate elicitation technique and
                documented the rationale for their choice.
            </p>

        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SecurityStep6
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1">
                <h2>Security Step 6 - Elicit Security Requirements</h2>
            </div>
            <div class="col2">
            </div>

        </div>    

        <div class="section-contents">
            
            <p>
                This step is the heart of the SQUARE process: the elicitation of security requirements. To the
                benefit of the requirements engineering team, most elicitation techniques provide detailed
                guidance on how to perform the elicitation, so this step is simply a matter of executing the
                technique. However, even if the stakeholders are very knowledgeable about the project and
                communicate effectively, it can be challenging for the requirements engineering team to elicit
                correct requirements.            
            </p>
            <p>
                 Perhaps the largest mistake that the requirements engineering team can make in this step is to
                elicit non-verifiable or vague, ambiguous requirements. Each requirement must be stated in a
                manner that will allow relatively easy verification once the project has been implemented.
                For instance, the requirement “The system shall improve the availability of the existing customer
                service center” is impossible to measure objectively. Instead, the requirements engineering
                team should encourage the production of requirements that are clearly verifiable and,
                where appropriate, quantifiable. A better version of the previously stated requirement would
                thus be “The system shall handle at least 300 simultaneous connections to the customer service
                center.”           
            </p>
            <p>
                 A second mistake that the requirements engineering team can make in this step is to elicit
                implementations or architectural constraints instead of requirements. Requirements are concerned
                with what the system should do, not how it should be done.           
            </p>
            <p>
                All elicitation techniques will involve face-to-face interaction with the stakeholders, so it is
                also the responsibility of the requirements engineering team to make logistical arrangements
                with the stakeholders and inform them of the time they can expect to spend in this part of the
                SQUARE process.            
            </p>
            
        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Responsibilities</h3></div>

        <div class="section-contents">
        
            <h3>Requirements Engineering Team</h3>

            <ol>
            
                <li>
                    Execute the elicitation technique chosen in Step 5. This may entail a large amount of
                    logistical preparation and orientation for the stakeholders. Stakeholders should be informed
                    of the amount of time they can be expected to spend during this step of the process.                
                </li>
                <li>
                    Document the requirements as they are collected.
                </li>

            </ol>

            <h3>Stakeholders</h3>

            <p>Follow the instructions given by the requirements engineering team during the elicitation process.</p>

            <h3>Joint</h3>

            <p>Encourage the generation of verifiable, preferably quantifiable security requirements.</p>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Exit Criteria</h3></div>

        <div class="section-contents"><p>An initial set of security requirements for the system has been elicited and documented. It is not necessary that the set be considered final or completely correct.</p></div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SecurityStep9
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1">
                <h2>Security Step 9 - Requirements Inspection</h2>
            </div>
            <div class="col2">
            </div>

        </div>    

        <div class="section-contents">
        
            <p>
                The last step of the SQUARE process, requirements inspection, is one of the most important
                elements in creating a set of accurate and verifiable security requirements. Inspection can be
                done at varying levels of formality, from Fagan Inspections to peer reviews [Fagan 86,
                Wiegers 02]. The goal of any inspection method, however, is to find any defects in the requirements
                such as ambiguities, inconsistencies, or mistaken assumptions.            
            </p>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Responsibilities</h3></div>

        <div class="section-contents">
        
            <h3>Requirements Engineering Team</h3>

            <p>
                Facilitate the inspection process by providing any orientation to the structured inspection
                technique or informal inspection guides such as checklists.            
            </p>

            <h3>Stakeholders</h3>

            <p>
                Come to a consensus on the validity of each security requirement. Verify that each requirement
                is verifiable, in scope, within financial means, and feasible to implement. Requirements
                that do not fit these criteria should have been identified in earlier stages of SQUARE, but the
                stakeholders should use this opportunity as a last chance to remove any requirements from
                the working set.            
            </p>

            <h3>Joint</h3>

            <p>
                Verify that each requirement is directly applicable to one or more of the security goals of the
                project or in support of a higher level requirement.            
            </p>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Exit Criteria</h3></div>

        <div class="section-contents">
        
            <p>
                All security requirements have been verified both by the requirements engineering team and
                the stakeholders. At this point the SQUARE process is complete, and the requirements engineering
                team can produce the final security requirements document for the stakeholders.            
            </p>

        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

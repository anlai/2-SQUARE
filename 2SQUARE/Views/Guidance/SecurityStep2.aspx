<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SecurityStep2
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1">
                <h2>Security Step 2 - Identify Security Goals</h2>
            </div>
            <div class="col2">
            </div>

        </div>    

        <div class="section-contents">
        
            <p>
                The purpose of Step 2 in SQUARE is for the stakeholders to formally agree on a set of prioritized
                security goals for the project. Without overall security goals for the project, it is impossible
                to identify the priority and relevance of any security requirements that are generated. In
                addition, the establishment of security goals scopes the rest of the SQUARE process.
            </p>
            <p>
                Initially, different stakeholders will likely have different security goals. For example, a member
                of human resources may be concerned about maintaining the confidentiality of personnel
                records, whereas a stakeholder in finance may be concerned with ensuring that financial data
                is not modified without authorization. The security goals of the stakeholders may also conflict
                with one another. A security-conscious stakeholder may place high importance on strong
                security controls for the system, which in turn may hamper overall system performance. Decreased
                performance might likely be at odds with the goals of the marketing department. Step
                2 in the SQUARE process serves to eliminate such conflicts and align all of the stakeholders’
                interests.            
            </p>
            <p>
                The security goals of the project must be in clear support of the project’s overall business
                goal, which also must be identified and enumerated in this step. On average, stakeholders
                should attempt to brainstorm to come up with approximately half a dozen security goals for
                the project, with more or less depending on the scale of the project. More sophisticated techniques
                for mapping high-level business requirements to low-level requirements can be found
                in Core Security Requirements Artefacts [Moffett 04] and “Mapping Mission-Level Availability
                Requirements to System Architectures and Policy Abstractions” [Watro 01].            
            </p>
            <p>
                Once the goals of the various stakeholders have been identified, they must be prioritized. In
                the absence of consensus, an executive decision may be needed to prioritize the goals.            
            </p>
            <p>
                Finally, the requirements engineering team must encourage the stakeholders to generate security
                goals as opposed to requirements or recommendations. There is a fine line between a security
                goal such as “The system shall be available for use when needed,” a requirement such
                as “The system must have a continuity of operations plan in place to ensure appropriate system
                availability,” and a recommendation such as “Invest in backup information technology
                hardware to ensure business continuity.” The requirements engineering team must act as the
                experts in this situation, providing assistance to the stakeholders so that they may generate an
                appropriately scoped set of security requirements.
            </p>
        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Responsibilities</h3></div>

        <div class="section-contents">
        
        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Exit Criteria</h3></div>

        <div class="section-contents"></div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

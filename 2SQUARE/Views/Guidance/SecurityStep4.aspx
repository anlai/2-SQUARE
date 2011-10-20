<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SecurityStep4
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1">
                <h2>Security Step 4 - Risk Assessment</h2>
            </div>
            <div class="col2">
            </div>

        </div>    

        <div class="section-contents">
        
            <p>
                The purpose of this step in the SQUARE process is to identify the vulnerabilities and threats
                that face the system, the likelihood that the threats will materialize as real attacks, and any
                potential consequences of an attack. Without a risk assessment, organizations can be tempted
                to implement security requirements or countermeasures without a logical rationale. For instance,
                the stakeholders may decide that encryption is a necessary component of their system
                without fully understanding the nature of the problem that encryption can solve. The risk assessment
                also serves to prioritize the security requirements at a later stage in the process.            
            </p>
            <p>
                There are a growing number of risk assessment methods from which to choose (see the list of
                examples in Section 4.5.1). Some of the methods are very structured and may require the assistance
                of an external risk expert. Ideally, this expert would already be a part of the requirements
                engineering team.            
            </p>
            <p>
                After the threats have been identified by the risk assessment method, they must be classified
                according to likelihoods. Again, this will aid in prioritizing the security requirements that are
                generated at a later stage. For each threat identified, a corresponding security requirement can
                identify a quantifiable, verifiable response. For instance, a requirement may describe speed of
                containment, cost of recovery, or limit to the damage that can be done to the system’s functionality.            
            </p>
        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Responsibilities</h3></div>

        <div class="section-contents">
        
            <h3>Requirements Engineering Team</h3>

            <ol>
                <li>
                    Facilitate the completion of a structured risk assessment, likely performed by an external risk expert.
                </li>
                <li>
                    Review the results of the risk assessment and share them with stakeholders.
                </li>
            </ol>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Exit Criteria</h3></div>

        <div class="section-contents">
        
            <p>
                All vulnerabilities and threats have been identified and classified according to their likelihoods.
                Potential consequences of attacks are identified. The results are well documented and
                shared with the stakeholders.            
            </p>

        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PrivacyStep3
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
                <h2>Security Step 3 - Collect Artifacts</h2>

        </div>    

        <div class="section-contents">
        
            <p>
                In this step, participants collect the relevant artifacts for the system being developed. These arti-facts may clarify an existing system or clarify the purpose and environment for the proposed sys-tem.
            </p>
            <p>
                With respect to privacy, some of the relevant artifacts include
            </p>

            <ul>
                <li>system architecture diagrams</li>
                <li>use case scenarios and diagrams</li>
                <li>misuse case scenarios and diagrams</li>
                <li>attack trees</li>
                <li>user-role hierarchies</li>
            </ul>
        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header">System Architecture Diagrams</div>

        <div class="section-contents">
        
            <p>System architecture diagrams provide an overview of the system as it exists. A dynamic perspec-tive of a system can show how data flows among the different components. Because privacy is concerned with vulnerabilities with respect to data, the architecture diagrams can determine data- flow connections that could be vulnerable to attack as well as connections between components and their data-flow dependencies.</p>

            <p>A system architecture diagram can also help determine how the system stores data and how secure those data stores are.</p>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header">Use Case Scenarios/Diagrams</div>

        <div class="section-contents">
        
            <p>Privacy use cases will mostly be related to how the system handles user data and how the system components interact with each other. They help the stakeholders and the requirements engineering team gain a better understanding of the system and its requirements.</p>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header">Misuse Case Scenarios/Diagrams</div>

        <div class="section-contents">
        
            <p>Misuse cases identify the vulnerabilities of the system and can be used to make the system more resistant to such attacks. They also identify the risks that the system faces.</p>

            <p>Some of the requirements that can be derived from the above misuse case include the following:</p>

            <ul>
                <li>The system network communications must be protected from unauthorized information ga-thering and eavesdropping.</li>
                <li>The system shall provide a data backup mechanism.</li>
                <li>The system shall have functional audit logs and usage reports that do not disclose identity information.</li>
                <li>The system shall have strong authentication measures in place at all system gateways and entrance points.</li>
            </ul>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header">Attack Trees</div>

        <div class="section-contents">
        
            <p>The purpose of an attack tree is to model threats to the system by focusing on the attackers and the ways they may attack the system [Schneier 2000]. The goal of the attack is represented as the root node, and leaf nodes describe the different ways in which that goal may be achieved. Figure 2 shows an example attack tree.</p>

            <p>Using this knowledge, the stakeholders and the requirements engineering team can determine the ways the system can be protected from potential attacks.</p>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header">User-Role Hierarchies</div>

        <div class="section-contents">Privacy-related systems are required to implement a role-based access control mechanism. Be-cause data is the central point for privacy, it is critical to determine who can access which data. For this purpose, a role-based hierarchy for the system can determine the access control require-ments.</div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

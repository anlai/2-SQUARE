<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PrivacyStep2
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
                <h2>Security Step 2 - Identify Assets and Privacy Goals</h2>

        </div>    

        <div class="section-contents">

            <p>
                The second step in the SQUARE process is to identify assets and security goals. For privacy re-quirements engineering, the basic idea of this step is the same, only the requirements engineering team and the stakeholders agree on a set of assets and prioritized privacy goals instead of security goals. The purpose of this step is to initiate a discussion among the stakeholders regarding their assets and overall privacy goals for the project.    
            </p>        
            <p>
                Because privacy policy is driven by laws and regulations, a number of privacy goals are derived from laws like the HIPAA, Public Law 104-191, the OECD Guidelines on the Protection of Pri-vacy and Trans-border Flows of Personal Data, and the Personal Information Protection Act (PIPA).            
            </p>
            <p>
                The following are some examples goals for privacy:
            </p>

            <ul>
            
                <li>Ensure that personal data is collected with the user‟s permission.</li>
                <li>Ensure that the data collected for a specific purpose is not used for other purposes without appropriate authorization.</li>
                <li>Ensure that the user is aware of the purpose for which personal data is collected.</li>
            
            </ul>

        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

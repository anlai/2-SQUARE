<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PrivacyStep4
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
        
            <p>Risk assessment for privacy and security requirements identifies the vulnerabilities and threats that the system faces, the likelihood that the threats will materialize as real attacks, and the poten-tial consequences of an attack, if any. Risk assessment establishes a rationale for choosing and implementing the privacy requirements. Identification and prioritization of risks also help to pri-oritize privacy requirements later in the elicitation process [Mead 2005].</p>

            <p>Privacy risk assessment identifies vulnerabilities with respect to data and how it can be compro-mised. As such, it takes into account the policies, regulations, and laws for privacy. Because secu-rity risk assessment does not necessarily consider laws and regulations, the goals of privacy risk assessment tend to be different from the goals of security risk assessment.</p>

            <p>A number of different privacy laws govern different industries and domains. Some of these laws and regulations provide certain guidelines that can be used to assess privacy risks. For example, the HIPAA addresses privacy concerns of health information systems by enforcing data exchange standards. Privacy Impact Assessment (PIA) is a comprehensive process for determining the pri-vacy, confidentiality, and security risks associated with the collection, use, and disclosure of per-sonal information [Abu-Nimeh 2009].</p>

            <p>The privacy risk assessment focuses on the following [Abu-Nimeh 2009]:</p>

            <ul>
                <li>nature of data collected</li>
                <li>purpose of data collection</li>
                <li>procedures for obtaining an individual‟s consent</li>
                <li>compliance to regulations</li>
                <li>necessity and accuracy of data</li>
            </ul>

            <p>Furthermore, the privacy risk assessment checks and analyzes the following [Abu-Nimeh 2009]:</p>

            <ul>
                <li>authorization and authentication requirements</li>
                <li>risk of theft</li>
                <li>third-party vulnerabilities</li>
            </ul>

            <p>According to Boehm, any risk assessment should take into account these three steps [Boehm 1991]:</p>

            <ol>
                <li>risk identification. A list of potential risks should be generated using available project infor-mation and requirements.</li>
                <li>risk analysis. After risks have been identified, they need to be analyzed with respect to their probability of occurrence and their potential impact on the project or system.</li>
                <li>risk prioritization. The risks then need to be ranked by importance based on the probability of occurrence and degree of impact.</li>
            </ol>

            <p>Currently, there exists a body of privacy literature and laws that focuses on some privacy areas of interest. Requirements engineers, risk experts, and stakeholders can classify these works by ana-lyzing what steps of risk assessment process they address [Panusuwan 2009]. Using this classifi-cation, participants can select the risk assessment methods that suit their requirements.</p>

            <p>A report on privacy risk assessment published by the CERT Program illustrates case studies of two projects that used different risk assessment techniques to identify, analyze, and prioritize risks [Panusuwan 2009]. Another paper describes how PIA [Flaherty 2000] and the HIPAA can be used to assess privacy risks in conjunction with security risk assessment techniques that are used in the SQUARE methodology [Abu-Nimeh 2009].</p>

        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

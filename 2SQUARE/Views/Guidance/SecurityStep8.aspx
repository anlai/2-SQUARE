<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SecurityStep8
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1">
                <h2>Security Step 8 - Prioritize Requirements</h2>
            </div>
            <div class="col2">
            </div>

        </div>    

        <div class="section-contents">
        
            <p>
                In most cases, the client organization will be unable to implement all of the security requirements
                due to lack of time, resources, or developing changes in the goals of the project. Thus,
                the purpose of this step in the SQUARE process is to prioritize the security requirements so
                that the stakeholders can choose which requirements to implement and in what order. The
                results of Step 4, the risk assessment, and Step 7, categorization, are crucial inputs to this
                step.
            </p>
            <p>
                The available prioritization methods are flexible and can be as simple as unstructured deliberation
                between the stakeholders. There are several structured prioritization techniques that
                exist, such as Triage [Davis 03], Win-Win [Boehm 01], and the Analytical Hierarchy Process
                (AHP); the latter has been reported to be quite effective [Karlsson 97, Saaty 80]. AHP is discussed
                in detail in Section 4.9 of this report. Ideally, the requirements engineering team
                should also produce a cost-benefit analysis to aid the stakeholders’ decisions.
            </p>
            <p>
                During prioritization, some of the requirements may be deemed to be entirely unfeasible to
                implement. In such cases, the requirements engineering team has a choice: completely dismiss
                the requirement from further consideration, or document the requirement as “future
                work” and remove it from the working set of project requirements. This decision should be
                made after consulting with the stakeholders.
            </p>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Responsibilities</h3></div>

        <div class="section-contents">
        
            <h3>Requirements Engineering Team</h3>

            <p>
                Facilitate the prioritization process with the stakeholders. If a structured prioritization process
                is selected, teach the stakeholders how to perform the process.
            </p>

            <h3>Stakeholders</h3>

            <p>
                Prioritize the security requirements using the risk assessment and categorization results as a
                basis for decision making.            
            </p>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Exit Criteria</h3></div>

        <div class="section-contents">
            
            <p>All security requirements have been prioritized.</p>

        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

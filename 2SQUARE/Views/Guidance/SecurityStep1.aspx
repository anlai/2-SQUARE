<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SecurityStep1
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1">
                <h2>Security Step 1 - Agree on Definitions</h2>
            </div>
            <div class="col2">
            </div>

        </div>    

        <div class="section-contents">
            <p>
                In order to guarantee effective and clear communication throughout the requirements engineering
                process, the requirements engineering team and stakeholders must first agree on a
                common set of terminology and definitions. Given the differences in expertise, knowledge,
                and experience, an arbitrary term may have multiple meanings between the participants of
                SQUARE. In addition, there may be ambiguity in the level of detail that is assumed for a
                given term. For instance, one stakeholder may view “access controls” as a set of policies that
                governs which users may be granted access to which resources. Another stakeholder may
                view access controls as the software elements in the system that actually implement this functionality.
                These differences in perspective must be resolved before the process can continue.
            </p>
            <p>
                The initial list of terms should also include suggested definitions for each term and their corresponding
                sources. This allows the stakeholders to get a general understanding and scope of
                each term, and in the common case, select one of the suggested definitions as final. See Table
                3 for an example of the information that should be provided with each term. In this example,
                the stakeholders could place a checkmark next to the definition that suits them best.
            </p>
        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Responsibilities</h3></div>

        <div class="section-contents">
        
            <h3>Requirements Engineering Team</h3>

            <ol>
                <li>
                    Provide an initial set of terms with corresponding suggested definitions. All external
                    sources should be cited. The set of terms should be as comprehensive as possible, even
                    if some terms appear to be irrelevant to the project. 
                </li>
                <li>
                    Provide a means for stakeholders to review and select a desired definition for each term.
                    This process could take place by way of a Web-based tool, email exchanges, or paper
                    surveys. The chosen means must allow the stakeholders to freely add new terms and
                    definitions to the set.
                </li>
                <li>
                    Document and share the finalized set of terms and definitions.
                </li>
            </ol>

            <h3>Stakeholders</h3>

                <p>
                    Select an existing or create a custom definition for each term provided by the requirements
                    engineering team. All stakeholders must come to a consensus on each term’s definition in a
                    timely manner and present their results to the requirements engineering team.
                </p>

            <h3>Joint</h3>

                <p>
                    Establish a single point of contact (POC) for interaction between the requirements engineering team and the stakeholders.
                </p>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Exit Criteria</h3></div>

        <div class="section-contents">
        
            <p>
                A well-documented, agreed-on set of definitions has been established and is available to all
                stakeholders and the requirements engineering team. The definitions document will be used
                as a reference throughout the rest of the SQUARE process.
            </p>

        </div>

    </div>

    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

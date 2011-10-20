<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SecurityStep7
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="section-container">

        <div class="section-header">
        
            <div class="col1">
                <h2>Security Step 7 - Categorize Requirements</h2>
            </div>
            <div class="col2">
            </div>

        </div>    

        <div class="section-contents">

            <p>
                The purpose of this step is to allow the requirements engineer and stakeholders to classify the
                requirements as essential, non-essential, system level, software level, or as architectural constraints.
                The requirements engineering team can provide to the stakeholders a matrix such as
                the one in the following table to assist in this process.            
            </p>

            <table>
                <tr>
                    <td></td>
                    <td>System Level</td>
                    <td>Software Level</td>
                    <td>Architectural Constraint</td>
                </tr>
                <tr>
                    <td>Essential</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Non-essential</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>

            <p>
                The categories in the above table are not fixed; each iteration of SQUARE will likely produce a
                much larger set of categories that are customized to the project at hand. These categories are
                instead suggested as a minimal set.            
            </p>
            <p>
                 Since the goal of SQUARE is to produce security requirements, the requirements engineering
                team and stakeholders should avoid producing architectural constraints. Architectural constraints
                are provided as a category here to serve as an outlet for “requirements” that, upon
                categorization, are considered to be constraints. Ideally, such anomalies would be identified
                and corrected in the previous steps of the process.           
            </p>
            <p>
                 Once the requirements are categorized, the requirements engineering team and stakeholders
                will be able to prioritize them more efficiently.           
            </p>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Responsibilities</h3></div>

        <div class="section-contents">
        
            <h3>Requirements Engineering Team</h3>

            <ol>
                <li>
                    Provide a baseline set of categories such as those in the table above. The team may have to suggest
                    alternative categories, depending on the client project.
                </li>
                <li>
                    Facilitate the stakeholders’ categorization process.
                </li>
            </ol>

            <h3>Stakeholders</h3>

            <p>
            Come to a consensus on the categorization for each requirement.
            </p>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Exit Criteria</h3></div>

        <div class="section-contents">
        
            <p>The initial set of requirements has been organized into stakeholder-defined categories, and any remaining architectural constraints are identified as such.</p>

        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

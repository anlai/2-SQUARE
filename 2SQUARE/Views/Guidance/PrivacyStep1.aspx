<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PrivacyStep1
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
            In this step, participants create a comprehensive list of terms that will aid effective communica-tion and reduce ambiguity. Differences in perspective within a team can produce two kinds of problems [Mead 2005]:
            </p>

            <ul>
                <li>Certain terms may have multiple meanings among the participants</li>
                <li>Ambiguity may exist in the level of detail assumed for a particular term.</li>
            </ul>

        </div>

    </div>

    <div class="section-container">
    
        <div class="section-header"><h3>Suggested Privacy Terms</h3></div>

        <div class="section-contents">

            <table>

                <tr>
                    <td>
                        access<br/>
                        aggregation<br/>
                        anonymity<br/>
                        anonymous<br/>
                        application of denial of service<br/>
                        application modification<br/>
                        appropriation<br/>
                        authentication<br/>
                        authorization<br/>
                        blackmail<br/>
                        client-side profiles<br/>
                        collection limitation<br/>
                        contact<br/>
                    </td>
                    <td>
                        confidentiality<br/>
                        cookie<br/>
                        credential theft<br/>
                        data breach<br/>
                        data controller<br/>
                        data exposure<br/>
                        data privacy<br/>
                        data quality<br/>
                        disclosure<br/>
                        distortion<br/>
                        exclusion<br/>
                        exposure<br/>
                        fair information practice<br/>
                    </td>
                    <td>
                        functional manipulation<br/>
                        identification<br/>
                        identity fraud<br/>
                        increased accessibility<br/>
                        information aggregation<br/>
                        information collection<br/>
                        information monitoring<br/>
                        information personaliza-tion<br/>
                        information storage<br/>
                        information transfer<br/>
                        insecurity<br/>
                        interrogation<br/>
                        intrusion<br/>   
                    </td>
                    <td>
                        network credential theft<br/>
                        network denial of service<br/>
                        network exposure<br/>
                        openness<br/>
                        privacy<br/>
                        privacy act<br/>
                        privacy policy<br/>
                        privacy protection<br/>
                        right to privacy<br/>
                        pseudonymity<br/>
                        pseudonymous profile<br/>
                        secondary use<br/>
                        surveillance      <br/>      
                    </td>
                </tr>

            </table>        

        </div>

    </div>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
<a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

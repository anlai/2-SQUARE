<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SQUARE Guidance
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>SQUARE Guidance</h2>

    <p>All of the following documentation is from the original SQUARE documentation developed by the Software Engineering Institute at Carnegie Mellon University.</p>

    <h3>Guidance</h3>
    <ul>
        <li><%: Html.ActionLink("Security", "Security") %></li>
        <li><%: Html.ActionLink("Privacy", "Privacy") %></li>
    </ul>

    <h3>Sources</h3>
    <ul>
        <li><a href="http://www.cert.org/sse/square/">Square Website</a></li>
        <li><a href="http://www.sei.cmu.edu/library/abstracts/reports/05tr009.cfm">Security Quality Requirements Engineering Report</a></li>
        <li><a href="http://www.sei.cmu.edu/library/abstracts/reports/10tn022.cfm">Adapting the SQUARE Process for Privacy Requirements Engineering</a></li>
    </ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
</asp:Content>

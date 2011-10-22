<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NoAccessToStep
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>NoAccessToStep</h2>

    <p>You do not have access to the requested step.</p>

    <p><a href="#" onClick="history.go(-1)" class="nav-button">Click here to go Back</a> </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContents" runat="server">
    <a href="#" onClick="history.go(-1)" class="nav-button">Back</a> 
</asp:Content>

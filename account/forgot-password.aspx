<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="forgot-password.aspx.cs" Inherits="account_forgot_password" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentLeft" Runat="Server">
    <ul id="left-nav">
		<li><a href="/account">My Account</a></li>
	</ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" Runat="Server">
    <fieldset class="account-form">
        <legend>Forgot your password?</legend>
        <asp:Literal ID="litMsg" runat="server" />
        <asp:Panel ID="pnlForm" runat="server" DefaultButton="btnSubmit" >
        <p>Just enter your email below and we'll send a new one to your registered email address:</p>
        Email: <asp:TextBox ID="txtEmail" runat="server" />
        <br /><br />
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn-default" Text="Submit" 
            onclick="btnSubmit_Click" />
        </asp:Panel>
   </fieldset>

</asp:Content>
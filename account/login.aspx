<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="account_login" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentLeft" Runat="Server">
    <ul id="left-nav">
		<li><a href="/account">My Account</a></li>
	</ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" Runat="Server">
<asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnLogin">
    <asp:Literal ID="litMsg" runat="server" />
    <fieldset class="account-form">
        <legend>Log in</legend>
        <table>
            <tr>
                <td align="right">
                    <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUsername">Email</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsername" runat="server" ValidationGroup="login" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword">Password</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" ValidationGroup="login" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnLogin" runat="server" Text="Submit" onclick="btnLogin_Click"  class="btn-default"/>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Panel>
</asp:Content>


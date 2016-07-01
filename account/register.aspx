<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="account_register" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentLeft" Runat="Server">
    <ul id="left-nav">
		<li><a href="/account">My Account</a></li>
	</ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" Runat="Server">
<asp:Panel ID="pnlRegister" runat="server" DefaultButton="btnRegister">
    <asp:Literal ID="litMsg" runat="server" />
    <fieldset class="account-form">
        <legend>Registration</legend>
        <p>You know the drill.</p>
        <table>
            <tr>
                <td align="right">
                    <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUsername">Choose a Username</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsername" runat="server" ValidationGroup="register" MaxLength="255" />
                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" ControlToValidate="txtUsername"
                         ErrorMessage="(required)" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtPassword">Password</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" ValidationGroup="register" MaxLength="255" />
                    <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword"
                         ErrorMessage="(required)" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="txtPassword2">Confirm Password</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword2" TextMode="Password" runat="server" ValidationGroup="register" MaxLength="255" />
                    <asp:RequiredFieldValidator ID="reqPassword2" runat="server" ControlToValidate="txtPassword2"
                         ErrorMessage="(required)" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="txtEmail">Email</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" ValidationGroup="register" MaxLength="255"  />
                    <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail"
                         ErrorMessage="(required)" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn-default" 
                        onclick="btnRegister_Click"/>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Panel>
</asp:Content>


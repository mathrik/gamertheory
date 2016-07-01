<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="submit-game.aspx.cs" Inherits="games_submit_game" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:GamesLeftNav ID="myGameLeftNav" name="gmrLeftNav" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">

<asp:Literal ID="litMsg" runat="server"></asp:Literal>
<asp:Panel ID="pnlAddGame" runat="server" DefaultButton="btnAdd">
<table class="account-form">
    <tr>
        <td align="right"><asp:Label ID="lblTitle" AssociatedControlID="txtAddTitle" runat="server">Title:</asp:Label></td>
        <td><asp:TextBox ID="txtAddTitle" runat="server" /></td>
    </tr>
    <tr>
        <td align="right"><asp:Label ID="Label5" AssociatedControlID="txtAddReleaseDate" runat="server">Release Date</asp:Label></td>
        <td><asp:TextBox ID="txtAddReleaseDate" runat="server" /></td>
    </tr>
    <tr>
        <td align="right"><asp:Label ID="Label6" AssociatedControlID="fileCover" runat="server">Cover Image</asp:Label></td>
        <td><asp:FileUpload ID="fileCover" runat="server" /></td>
    </tr>
	<tr>
		<td align="right" valign="top"><asp:Label ID="Label3" AssociatedControlID="chk360" runat="server">Platforms:</asp:Label></td>
		<td>
			<asp:CheckBox ID="chk360" runat="server" Text="360" />
			<asp:CheckBox ID="chk3ds" runat="server" Text="3DS" />
			<asp:CheckBox ID="chkpc" runat="server" Text="PC" />
			<asp:CheckBox ID="chkps3" runat="server" Text="PS3" />
			<asp:CheckBox ID="chkwii" runat="server" Text="Wii" />
			
		</td>
	</tr>
</table>
<table class="account-form">
    <tr>
        <td colspan="2">
        <asp:Button ID="btnAdd" runat="server" CssClass="btn-default" Text="Add Game" onclick="btnAdd_Click" />
        </td>
    </tr>
</table>
</asp:Panel>
</asp:Content>


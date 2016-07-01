<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="account_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AccountSidebar ID="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" Runat="Server">
<script type="text/javascript">
	function toggleChangeEmail(emailstate) {
		if (emailstate == 1) {
			$("#details").hide();
			$("#change-password").hide();
			$("#<%=pnlChangeAvatar.ClientID %>").hide();
			$("#<%=pnlChangeEmail.ClientID %>").show();
		} else {
			$("#details").show();
			$("#<%=pnlChangeEmail.ClientID %>").hide();
		}
	}

	$(document).ready(function () {
		toggleChangeEmail(0);
	});

	function togglePassword(passState) {
		if (passState == 1) {
			$("#details").hide();
			$("#<%=pnlChangeEmail.ClientID %>").hide();
			$("#<%=pnlChangeAvatar.ClientID %>").hide();
			$("#change-password").show();
		} else {
			$("#details").show();
			$("#change-password").hide();
		}
	}

	function toggleAvatar(avatarState) {
		if (avatarState == 1) {
			$("#details").hide();
			$("#change-password").hide();
			$("#<%=pnlChangeEmail.ClientID %>").hide();
			$("#<%=pnlChangeAvatar.ClientID %>").show();
		} else {
			$("#details").show();
			$("#<%=pnlChangeAvatar.ClientID %>").hide();
		}
	}
</script>

<h2>Account Information for <asp:Literal ID="litUsername" runat="server" /></h2>
<asp:Literal ID="litMsg" runat="server" />
<br /><br />
<div class="subpage-container">
	<div class="subpage-legend">Details</div>
	<div class="subpage-inner-container">
		<ul class="tab-list">
			<li><a href="#" onclick="toggleChangeEmail(1); return false;">Change Email</a></li>
			<li><a href="#" onclick="toggleAvatar(1); return false;">Change Avatar</a></li>
			<li><a href="#" onclick="togglePassword(1); return false;">Change Password</a></li>
		</ul>
		<div class="clearance"></div>
		<div id="details" style="padding: 10px;">
		<asp:Image ID="imgAvatar" runat="server" /><br />
		Email: <asp:Literal ID="litDisplayEmail" runat="server" />

		</div>
		<asp:Panel ID="pnlChangeAvatar" runat="server" DefaultButton="btnChangeAvatar" style="display: none;">
		<asp:FileUpload ID="fileAvatar" runat="server" />
		<br /><br />
		<asp:Button ID="btnChangeAvatar" runat="server" OnClick="btnChangeAvatar_clicked" Text="Update Avatar" class="btn-default" />
		</asp:Panel>

		<asp:Panel ID="pnlChangeEmail" runat="server" DefaultButton="btnChangeEmail">
		Email: <asp:Textbox ID="txtEmail" runat="server" />
		<br /><br />
		<asp:Button ID="btnChangeEmail" runat="server" OnClick="btnChangeEmail_clicked" CssClass="btn-default" Text="Update Email" />
		&nbsp;&nbsp;
		<input type="button" value="Cancel" onclick="toggleChangeEmail(0); return false;" class="btn-default" />
		</asp:Panel>
		<br /><br />
		<div id="change-password" style="display: none;">
		<asp:Panel ID="pnlPassword" runat="server" DefaultButton="btnChangePassword">
			<table>
				<tr>
					<td align="right">Password</td>
					<td><asp:TextBox ID="txtPass1" runat="server" TextMode="Password" /></td>
				</tr>
				<tr>
					<td align="right">Confirm Password</td>
					<td><asp:TextBox ID="txtPass2" runat="server" TextMode="Password" /></td>
				</tr>
				<tr>
					<td></td>
					<td><asp:Button ID="btnChangePassword" runat="server" Text="Update Password" OnClick="btnChangePassword_clicked" CssClass="btn-default" />
					&nbsp;&nbsp;
					<input type="button" value="Cancel" onclick="togglePassword(0); return false;" class="btn-default" />
					</td>
				</tr>
			</table>
		</asp:Panel>
		</div>
	</div>
</div>

<table>
	<tr>
		<td valign="top" width="50%">
<div class="subpage-container">
	<div class="subpage-legend">Comments</div>
	<div class="subpage-inner-container">
		<div id="account-comments">
		<asp:Repeater ID="rptRecentPosts" runat="server" OnItemDataBound="rptRecentPosts_databound">
		<ItemTemplate>
			<div class="post-copy2">
				<asp:Literal ID="litUsername" runat="server" />
				<div class="title"><%#Eval("body") %></div>
				<asp:Literal ID="litReleaseDate" runat="server" /> | <asp:Literal ID="litReadMore" runat="server" />
			</div>
			<div class="clearance" style="height: 10px;"></div>
		</ItemTemplate>
		</asp:Repeater>
		</div>
	</div>
</div>
		</td>
		<td>&nbsp;</td>
		<td valign="top">
<div class="subpage-container">
	<div class="subpage-legend">Reviews</div>
	<div class="subpage-inner-container">
	<asp:Repeater ID="rptGames" runat="server" OnItemDataBound="rptGames_DataBound" 
        onitemcommand="rptGames_ItemCommand">
        <ItemTemplate>
        <div style="padding: 3px 5px 5px;">
			<%#Eval("title") %>&nbsp;&nbsp;
			<a class="read-more" href="<%#Eval("permalink") %>">Read Review</a>
		</div>
        </ItemTemplate>
    </asp:Repeater>
	</div>
</div>
		</td>
	</tr>
</table>
</asp:Content>


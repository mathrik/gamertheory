<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountSidebar.ascx.cs" Inherits="controls_AccountSidebar" %>
<asp:Panel ID="pnlLoggedIn" runat="server">
<ul id="left-nav">
	<li><a href="/account" class="account-home">My Account</a></li>
	<li><a href="/account/collection.aspx" class="collection">My Collection</a></li>
	<li><a href="/account/friends-list.aspx" class="friends">My Friends</a></li>
	<li><a href="/account/manage-stories.aspx" class="stories">Manage Stories</a></li>
</ul>
</asp:Panel>
<asp:Panel ID="pnlLoggedOut" runat="server">
<ul id="left-nav">
	<li><a href="/account/login.aspx" class="login">Log In</a></li>
	<li><a href="/account/register.aspx" class="register">Register</a></li>
</ul>
</asp:Panel>
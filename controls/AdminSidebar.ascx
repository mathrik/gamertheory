<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminSidebar.ascx.cs" Inherits="AdminSidebar" %>
<ul id="left-nav">
	<li><a href="/account" class="account-home">My Account</a></li>
	<li><a href="/admin/manage-stories.aspx" class="stories">Manage Stories</a></li>
	<asp:Literal ID="litNavItems" runat="server" />
</ul>
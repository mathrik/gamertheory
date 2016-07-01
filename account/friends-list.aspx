<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="friends-list.aspx.cs" Inherits="account_friends_list" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
	 <gamer:AccountSidebar ID="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
	<h2>Friends List</h2>
	<table class="row-box-container" id="friends-list">
		<asp:Repeater ID="rptFriends" runat="server" OnItemDataBound="rptFriends_databound">
		<ItemTemplate>
		<tr>
			<td><asp:Literal ID="litThumbnail" runat="server" /></td>
			<td>
				<span class="title"><a href="/account/view-user.aspx?id=<%#Eval("id") %>"><%#Eval("username") %></a></span>
			</td>
		</tr>
		</ItemTemplate>
		<AlternatingItemTemplate>
		<tr class="odd">
			<td><asp:Literal ID="litThumbnail" runat="server" /></td>
			<td>
				<span class="title"><a href="/account/view-user.aspx?id=<%#Eval("id") %>"><%#Eval("username") %></a></span>
			</td>
		</tr>
		</AlternatingItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>


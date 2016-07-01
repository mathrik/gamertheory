<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="user.aspx.cs" Inherits="user" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AccountSidebar ID="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
<h2>Account Information for <asp:Literal ID="litUsername" runat="server" /></h2>

	<asp:Image ID="imgAvatar" runat="server" />



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


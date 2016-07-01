<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="users_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
<gamer:GamesLeftNav ID="myGameLeftNav" name="gmrLeftNav" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
<h2>Gamer Theory Breakthroughs</h2>
<table class="account-form">
	<tr>
		<td valign="top">
<div class="subpage-container">
	<div class="subpage-legend">Largest Collections</div>
	<div class="subpage-inner-container">
		<div id="largest-collections">
		<asp:Repeater ID="rptLargestCollections" runat="server" OnItemDataBound="rptLargestCollections_databound">
		<ItemTemplate>
		<div class="post-item">
			<div class="post-thumb">
				<div style="max-height: 70px; overflow: hidden;"><a href="<%#Eval("permalink") %>"><asp:Literal ID="litThumbnail" runat="server" /></a></div>
				<a href="<%#Eval("permalink") %>" style="color:#000;"><asp:Literal ID="litUsername" runat="server" /></a>
			</div>
			<div class="post-copy">
				<span class="title"><asp:Literal ID="litNumber" runat="server" /></span>
			</div>
			<div class="clearance"></div>
		</div>
		</ItemTemplate>
		</asp:Repeater>
		</div>
	</div>
</div>
		</td>
		<td valign="top">
<div class="subpage-container">
	<div class="subpage-legend">Most Games Rated</div>
	<div class="subpage-inner-container">
		<div id="most-games-rated">
		<asp:Repeater ID="rptGamesRated" runat="server" OnItemDataBound="rptGamesRated_databound">
		<ItemTemplate>
		<div class="post-item">
			<div class="post-thumb">
				<div style="max-height: 70px; overflow: hidden;"><a href="<%#Eval("permalink") %>"><asp:Literal ID="litThumbnail" runat="server" /></a></div>
				<a href="<%#Eval("permalink") %>" style="color:#000;"><asp:Literal ID="litUsername" runat="server" /></a>
			</div>
			<div class="post-copy">
				<span class="title"><asp:Literal ID="litNumber" runat="server" /></span>
			</div>
			<div class="clearance"></div>
		</div>
		</ItemTemplate>
		</asp:Repeater>
		</div>
	</div>
</div>
		</td>
	</tr>
	<tr>
		<td valign="top">
<div class="subpage-container">
	<div class="subpage-legend">Most Comments</div>
	<div class="subpage-inner-container">
		<div id="most-comments-made">
		<asp:Repeater ID="rptCommentsMade" runat="server" OnItemDataBound="rptCommentsMade_databound">
		<ItemTemplate>
		<div class="post-item">
			<div class="post-thumb">
				<div style="max-height: 70px; overflow: hidden;"><a href="<%#Eval("permalink") %>"><asp:Literal ID="litThumbnail" runat="server" /></a></div>
				<a href="<%#Eval("permalink") %>" style="color:#000;"><asp:Literal ID="litUsername" runat="server" /></a>
			</div>
			<div class="post-copy">
				<span class="title"><asp:Literal ID="litNumber" runat="server" /></span>
			</div>
			<div class="clearance"></div>
		</div>
		</ItemTemplate>
		</asp:Repeater>
		</div>
	</div>
</div>
		</td>
		<td></td>
	</tr>
</table>
</asp:Content>


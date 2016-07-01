<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="reviews.aspx.cs" Inherits="games_reviews" %>
<%@ MasterType VirtualPath="~/Masterpage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
<gamer:GamesLeftNav ID="myGameLeftNav" name="gmrLeftNav" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
	<h2>Reviews</h2>
		<asp:Repeater ID="rptUpdates" runat="server" OnItemDataBound="rptUpdates_databound">
        <ItemTemplate>
        <div class="home-updates-item">
			<div class="update-thumb"><asp:Literal ID="litThumbnail" runat="server" /></div>
			<div class="update-copy">
				<div class="title"><%#Eval("Title") %></div>
				<%#Eval("Preview") %>
				<a class="read-more" href="<%#Eval("permalink") %>">READ MORE</a>
			</div>
			<div class="clearance"></div>
		</div>
        </ItemTemplate>
        </asp:Repeater>
	<asp:Panel class="story-paging-container paging-links" ID="pnlPagingContainer" runat="server">
		<div class="story-paging-left"><asp:Literal ID="litLeftPaging" runat="server" /></div>
		<div class="story-paging-right"><asp:Literal ID="litRightPaging" runat="server" /></div>
		<div class="clearance"></div>
	</asp:Panel>
</asp:Content>


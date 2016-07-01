<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" Runat="Server">
	<div id="home-featured">
		<asp:Literal ID="litHomeBillboard" runat="server" />
		
	</div>
	
	<div id="home-updates">
		<h2>Latest Updates</h2>
		<ul id="systems">
			<li><a href="Default.aspx?p=1">PC</a></li>
			<li><a href="Default.aspx?p=2">Microsoft</a></li>
			<li><a href="Default.aspx?p=3">Sony</a></li>
			<li><a href="Default.aspx?p=4">Nintendo</a></li>
			<li><a href="Default.aspx?p=5">Mobile</a></li>
		</ul>
		<div class="hr"></div>

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
       
		<a class="archives-link" href="/archives/">Story Archives</a>
	</div>
	
	<img src="images/home-section-shadow.gif" alt="" />
	
	<div id="home-recent-posts">
		<h2>Recent Posts</h2>
		<div class="hr"></div>
		<asp:Repeater ID="rptRecentPosts" runat="server" OnItemDataBound="rptRecentPosts_databound">
		<ItemTemplate>
		<div class="post-item">
			<div class="post-thumb"><asp:Literal ID="litThumbnail" runat="server" /></div>
			<div class="post-copy">
				<asp:Literal ID="litUsername" runat="server" />
				<div class="title"><asp:Literal ID="litBody" runat="server" /></div>
				<asp:Literal ID="litReleaseDate" runat="server" /> | <asp:Literal ID="litReadMore" runat="server" />
			</div>
			<div class="clearance"></div>
		</div>
		</ItemTemplate>
		</asp:Repeater>
	</div>
	
	<div id="home-podcasts">
		<h2>Latest Podcasts</h2>
		<div class="hr"></div>
		&nbsp;<br />
        <asp:Repeater ID="rptPodcasts" runat="server" OnItemDataBound="rptPodcasts_databound">
            <ItemTemplate>
        <div class="podcasts-item">
			<div class="podcasts-thumb"><asp:Literal ID="litThumbnail" runat="server" /></div>
			<div class="podcasts-copy">
				<div class="title"><%#Eval("title") %></div>
				<%#Eval("preview") %>
				<br />
				<b><asp:Literal ID="litReleaseDate" runat="server" /> | <a class="read-more" href="/podcast/?id=<%#Eval("id") %>">Listen</a></b>
			</div>
			<div class="clearance"></div>
		</div>
            </ItemTemplate>
        </asp:Repeater>
	</div>
</asp:Content>
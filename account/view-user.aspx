<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="view-user.aspx.cs" Inherits="account_view_user" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
	<gamer:GameRateCollect ID="myGameRateCollect" name="GameRateCollect" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AccountSidebar ID="gmrSidebar" runat="server" />
	<script type="text/javascript">
		function toggle_friend(friendID) {
			$.post("/ajax/toggle-friend.aspx",
				{ friend: friendID },
				function (data) {
					if (data == "success") {
						$("#<%=btnFriend.ClientID %>").hide();
						$("#friendMsg").html("Friend setting updated.");
					}
				});
		}
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
<h2><asp:Literal ID="litVUsername" runat="server" /></h2>
<table>
	<tr>
		<td><asp:Image ID="imgAvatar" runat="server" /></td>
		<td style="padding-left: 45px;">
			<asp:Button ID="btnFriend" runat="server" CssClass="btn-default" />
			<div id="friendMsg"></div>
		</td>
	</tr>
</table>
<br />
<table>
	<tr>
		<td valign="top" width="50%">
<div class="subpage-container">
	<div class="subpage-legend">Comments</div>
	<div class="subpage-inner-container">
		<div id="account-comments">
		<asp:Repeater ID="rptRecentPosts" runat="server" OnItemDataBound="rptRecentPosts_databound">
		<ItemTemplate>
		<div class="post-item">
			<div class="post-thumb"><asp:Literal ID="litThumbnail" runat="server" /></div>
			<div class="post-copy">
				<asp:Literal ID="litUsername" runat="server" />
				<div class="title"><%#Eval("body") %></div>
				<asp:Literal ID="litReleaseDate" runat="server" /> | <asp:Literal ID="litReadMore" runat="server" />
			</div>
			<div class="clearance"></div>
		</div>
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
	No reviews written.
	</div>
</div>
		</td>
	</tr>
</table>
<div class="clearance" style="height: 25px;"></div>
<h2>Collection</h2>
<table width="100%" class="row-box-container">
<asp:Repeater ID="rptGames" runat="server" OnItemDataBound="rptGames_databound">
<ItemTemplate>
    <tr>
        <td align="center" valign="middle" height="90">
<a href="/games/details.aspx?id=<%#Eval("gameID") %>"><asp:Literal ID="litGameImage" runat="server" /></a>
        </td>
        <td align="center">
<asp:Literal ID="litPlatforms" runat="server" />
        </td>
		<td align="center">
			<asp:Literal ID="litRanking" runat="server" />
			<div id="rating<%#Eval("id") %>" class="rating-stars">
	<table>
		<tr>
			<td><a href="javascript:void(0)" onclick="addRating(1,<%#Eval("id") %>)" onmouseover="previewRating(1,<%#Eval("id") %>)" onmouseout="setRating(<%#Eval("id") %>)"><img src="/images/spacer.gif" width="8" height="9" alt="" /></a></td>
			<td><a href="javascript:void(0)" onclick="addRating(2,<%#Eval("id") %>)" onmouseover="previewRating(2,<%#Eval("id") %>)" onmouseout="setRating(<%#Eval("id") %>)"><img src="/images/spacer.gif" width="8" height="9" alt="" /></a></td>
			<td><a href="javascript:void(0)" onclick="addRating(3,<%#Eval("id") %>)" onmouseover="previewRating(3,<%#Eval("id") %>)" onmouseout="setRating(<%#Eval("id") %>)"><img src="/images/spacer.gif" width="8" height="9" alt="" /></a></td>
			<td><a href="javascript:void(0)" onclick="addRating(4,<%#Eval("id") %>)" onmouseover="previewRating(4,<%#Eval("id") %>)" onmouseout="setRating(<%#Eval("id") %>)"><img src="/images/spacer.gif" width="8" height="9" alt="" /></a></td>
			<td><a href="javascript:void(0)" onclick="addRating(5,<%#Eval("id") %>)" onmouseover="previewRating(5,<%#Eval("id") %>)" onmouseout="setRating(<%#Eval("id") %>)"><img src="/images/spacer.gif" width="8" height="9" alt="" /></a></td>
		</tr>
	</table>
			</div>
			</div>
		</td>
	</tr>
</ItemTemplate>
<AlternatingItemTemplate>
	<tr class="odd">
		<td align="center" valign="middle" height="90">
<a href="/games/details.aspx?id=<%#Eval("gameID") %>"><asp:Literal ID="litGameImage" runat="server" /></a>
		</td>
		<td align="center">
<asp:Literal ID="litPlatforms" runat="server" />
		</td>
		<td align="center">
			<asp:Literal ID="litRanking" runat="server" />
			<div id="rating<%#Eval("id") %>" class="rating-stars">
	<table>
		<tr>
			<td><a href="javascript:void(0)" onclick="addRating(1,<%#Eval("id") %>)" onmouseover="previewRating(1,<%#Eval("id") %>)" onmouseout="setRating(<%#Eval("id") %>)"><img src="/images/spacer.gif" width="8" height="9" alt="" /></a></td>
			<td><a href="javascript:void(0)" onclick="addRating(2,<%#Eval("id") %>)" onmouseover="previewRating(2,<%#Eval("id") %>)" onmouseout="setRating(<%#Eval("id") %>)"><img src="/images/spacer.gif" width="8" height="9" alt="" /></a></td>
			<td><a href="javascript:void(0)" onclick="addRating(3,<%#Eval("id") %>)" onmouseover="previewRating(3,<%#Eval("id") %>)" onmouseout="setRating(<%#Eval("id") %>)"><img src="/images/spacer.gif" width="8" height="9" alt="" /></a></td>
			<td><a href="javascript:void(0)" onclick="addRating(4,<%#Eval("id") %>)" onmouseover="previewRating(4,<%#Eval("id") %>)" onmouseout="setRating(<%#Eval("id") %>)"><img src="/images/spacer.gif" width="8" height="9" alt="" /></a></td>
			<td><a href="javascript:void(0)" onclick="addRating(5,<%#Eval("id") %>)" onmouseover="previewRating(5,<%#Eval("id") %>)" onmouseout="setRating(<%#Eval("id") %>)"><img src="/images/spacer.gif" width="8" height="9" alt="" /></a></td>
		</tr>
	</table>
			</div>
			</div>
		</td>
		<td align="center">
		<asp:Literal ID="litRemoveFromCollection" runat="server" />
		</td>
	</tr>
</AlternatingItemTemplate>
</asp:Repeater>
</table>
<div class="clearance"></div>
</asp:Content>


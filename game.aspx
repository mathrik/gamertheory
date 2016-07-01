<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="game.aspx.cs" Inherits="game" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content3" ContentPlaceHolderID="headerContent" Runat="Server">
	<gamer:GameRateCollect ID="myGameRateCollect" name="GameRateCollect" runat="server" />
    <gamer:FancyBox ID="fancybox" name="gmrfancybox" runat="server" />
<script type="text/javascript">
	$(document).ready(function () {
		$("a.fancybox-image").fancybox({
			'transitionIn': 'elastic',
			'transitionOut': 'elastic',
			'speedIn': 300,
			'speedOut': 500,
			'overlayShow': false,
			'type': 'image'
		});

	});
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:GamesLeftNav ID="myGameLeftNav" name="gmrLeftNav" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" Runat="Server">
    <h2><asp:Literal ID="litGameTitle" runat="server" /></h2>
    <asp:Literal ID="litGameBody" runat="server" />
	<br />
<table width="100%" class="row-box-container">

<asp:Repeater ID="rptGames" runat="server" OnItemDataBound="rptGames_databound">
<ItemTemplate>
    <tr>
        <td align="center" valign="middle" height="90">
<asp:Literal ID="litGameImage" runat="server" />
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
		<asp:Literal ID="litCollection" runat="server" />
		</td>
	</tr>
</ItemTemplate>
<AlternatingItemTemplate>
	<tr class="odd">
		<td align="center" valign="middle" height="90">
<asp:Literal ID="litGameImage" runat="server" />
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
		<asp:Literal ID="litCollection" runat="server" />
		</td>
	</tr>
</AlternatingItemTemplate>
</asp:Repeater>
</table>
	<br />
	<div class="subpage-container">
	<asp:Literal ID="litStoriesList" runat="server" />
	</div>

    <div class="image-gallery">
    <asp:Repeater ID="rptImages" runat="server" OnItemDataBound="rptImages_databound">
    <ItemTemplate>
    <div class="screenshot-container">
    <asp:Literal ID="litImageTag" runat="server" />
    </div>
    </ItemTemplate>
    </asp:Repeater>
    </div>
<div class="clearance"></div>
<div class="subpage-container">
	<div class="subpage-legend">User Reviews</div>
	<asp:Literal ID="litUserReviewMsg" runat="server" />
<asp:Panel ID="pnlUserReview" runat="server">
	<script type="text/javascript">
		function showReviewForm() {
			$("#user-review-container").show();
		}

		function hideReviewForm() {
			$("#user-review-container").hide();
		}
	</script>
	<p style="padding: 5px 15px;">
	<input type="button" class="btn-default" onclick="showReviewForm(); return false;" value="Write a Review" />
	</p>
	<div id="user-review-container" style="display: none;">
	<table class="account-form">
		<tr>
			<td>
				Title<br />
				<asp:TextBox ID="txtUserReviewTitle" runat="server" />
			</td>
			<td>
				Rating<br />
				<asp:DropDownList ID="ddUserReviewRating" runat="server">
					<asp:ListItem Text="1 Star" Value="1" />
					<asp:ListItem Text="2 Stars" Value="2" />
					<asp:ListItem Text="3 Stars" Value="3" />
					<asp:ListItem Text="4 Stars" Value="4" />
					<asp:ListItem Text="5 Stars" Value="5" />
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				Review<br />
				<asp:TextBox ID="txtUserReviewBody" runat="server" TextMode="MultiLine" Width="475" Height="180" />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:Button ID="btnUserReviewSubmit" runat="server" CssClass="btn-default" Text="Submit" OnClick="btnUserReviewSubmit_Click" />
				&nbsp;&nbsp;
				<input type="button" value="Cancel" class="btn-default" onclick="hideReviewForm(); return false;" />
			</td>
		</tr>
	</table>
	</div>
</asp:Panel>
<asp:Panel ID="pnlUserReviewsList" runat="server">
	<ul class="user-reviews">
	<asp:Repeater ID="rptUserReviews" runat="server">
		<ItemTemplate>
			<li><a href="<%#Eval("permalink") %>"><%#Eval("Title") %></a><br /><%#Eval("Preview") %></li>
		</ItemTemplate>
	</asp:Repeater>
	</ul>
</asp:Panel>
</div>
<gamer:Comments ID="gamerComments" runat="server" />
</asp:Content>



<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="show-all.aspx.cs" Inherits="rate_this_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
	<gamer:GameRateCollect ID="myGameRateCollect" name="GameRateCollect" runat="server" />
	<script type="text/javascript">
		function showAllPlatforms() {
			$("#most-common").toggle();
			$("#all-platforms").toggle();
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
<gamer:GamesLeftNav ID="myGameLeftNav" name="gmrLeftNav" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
	<h2>Top Rated Games</h2>
	
<table width="100%" class="row-box-container">
	<tr>
		<td colspan="4" align="right" class="account-form">
			Filter By Platform<br />
			<div id="most-common"><asp:Literal ID="litCommonPlatformFilter" runat="server" /><a href="#" class="misc-btn-link" onclick="showAllPlatforms();return false;">Show All</a></div> 
			<div id="all-platforms" style="display: none;"><asp:Literal ID="litAllPlatformFilter" runat="server" /></div>
		</td>
	</tr>
<asp:Repeater ID="rptGames" runat="server" OnItemDataBound="rptGames_databound">
<ItemTemplate>
    <tr>
        <td align="center" valign="middle" height="90">
<a href="/games/details.aspx?id=<%#Eval("gameID") %>" class="gamecover"><asp:Literal ID="litGameImage" runat="server" /></a>
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
			<asp:Literal ID="litNumRankings" runat="server" />
		</td>
		<td align="center">
		<asp:Literal ID="litCollection" runat="server" />
		</td>
	</tr>
</ItemTemplate>
<AlternatingItemTemplate>
	<tr class="odd">
		<td align="center" valign="middle" height="90">
<a href="/games/details.aspx?id=<%#Eval("gameID") %>" class="gamecover"><asp:Literal ID="litGameImage" runat="server" /></a>
<asp:Literal ID="litGameTitle" runat="server" />
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
			<asp:Literal ID="litNumRankings" runat="server" />
		</td>
		<td align="center">
		<asp:Literal ID="litCollection" runat="server" />
		</td>
	</tr>
</AlternatingItemTemplate>
</asp:Repeater>
</table>
</asp:Content>


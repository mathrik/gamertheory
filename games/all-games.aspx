<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="all-games.aspx.cs" Inherits="games_all_games" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content3" ContentPlaceHolderID="headerContent" Runat="Server">
	<gamer:GameRateCollect ID="myGameRateCollect" name="GameRateCollect" runat="server" />
	<script type="text/javascript">
		function toggleSearchBox() {
			$("#search-box-container").toggle();
			//$("#btnOpenSearchBox").toggle($("#btnOpenSearchBox").val("Show Search Options"), $("#btnOpenSearchBox").val("Hide Search Options"));
		}

		function filterGames() {
			var genre, platform, publisher, year, title;
			genre = document.getElementById("<%=ddFilterGenre.ClientID %>").value;
			platform = document.getElementById("<%=ddFilterPlatform.ClientID %>").value;
			publisher = document.getElementById("<%=ddFilterPublisher.ClientID %>").value;
			year = document.getElementById("<%=ddFilterYear.ClientID %>").value;
			title = document.getElementById("<%=txtFilterTitle.ClientID %>").value;
			$.post("/ajax/filtered-games.aspx",
				{ genre: genre, platform: platform, publisher: publisher, year: year, title: title }, 
				function (data) {
					$("#games-page-container").html(data);
			});
		}


		function pageFilteredGames(pg) {
			var genre, platform, publisher, year, title;
			genre = document.getElementById("<%=ddFilterGenre.ClientID %>").value;
			platform = document.getElementById("<%=ddFilterPlatform.ClientID %>").value;
			publisher = document.getElementById("<%=ddFilterPublisher.ClientID %>").value;
			year = document.getElementById("<%=ddFilterYear.ClientID %>").value;
			title = document.getElementById("<%=txtFilterTitle.ClientID %>").value;
			$.post("/ajax/filtered-games.aspx",
				{ genre: genre, platform: platform, publisher: publisher, year: year, title: title, pg: pg },
				function (data) {
					$("#games-page-container").html(data);
			});
		}
	</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:GamesLeftNav ID="myGameLeftNav" name="gmrLeftNav" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" Runat="Server">
<h2>All Games</h2>
<input type="button" class="btn-default" value="Search Options" onclick="toggleSearchBox()" id="btnOpenSearchBox" />
<div style="display: none;" id="search-box-container">
<table class="row-box-container search-container">
	<tr>
		<td align="right">Platform</td>
		<td><asp:DropDownList ID="ddFilterPlatform" runat="server" /></td>
		<td align="right">Publisher</td>
		<td><asp:DropDownList ID="ddFilterPublisher" runat="server" /></td>
	</tr>
	<tr>
		<td align="right">Release Year</td>
		<td><asp:DropDownList ID="ddFilterYear" runat="server" /></td>
		<td align="right">Genre</td>
		<td><asp:DropDownList ID="ddFilterGenre" runat="server" /></td>
	</tr>
	<tr>
		<td align="right">Title</td>
		<td colspan="3"><asp:TextBox ID="txtFilterTitle" runat="server" Width="360" /></td>
	</tr>
	<tr>
		<td align="right"></td>
		<td colspan="3"><input type="button" onclick="filterGames()" value="Search" class="btn-default" /></td>
	</tr>
</table>
</div>
<br />&nbsp;<br />
<div id="games-page-container">
    <table width="100%" class="row-box-container">
<asp:Repeater ID="rptGames" runat="server" OnItemDataBound="rptGames_databound">
<ItemTemplate>
        <tr>
            <td align="center" height="90" valign="middle">
    <a href="/games/details.aspx?id=<%#Eval("id") %>" class="gamecover"><asp:Literal ID="litGameImage" runat="server" /></a>
            </td>
            <td align="left">	
				<table class="row-box-container games-ranking-row" width="100%">	
	<asp:Repeater ID="rptGameDetails" runat="server" OnItemDataBound="rptGamesDetails_databound">
	<ItemTemplate>
			<tr class="even">
				<td align="right" style="padding-right: 10px;">
				<asp:Literal ID="litPlatforms" runat="server" />
				</td>
				<td width="45">
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
				<td align="center" width="<%=collectionTDwidth %>">
				<asp:Literal ID="litAddToCollection" runat="server" />
				</td>
				<td align="center" width="105"><asp:Literal ID="litDateAdded" runat="server" /></td>
			</tr>
	</ItemTemplate>
	<AlternatingItemTemplate>
			<tr class="odd">
				<td align="right" style="padding-right: 10px;">
				<asp:Literal ID="litPlatforms" runat="server" />
				</td>
				<td width="45">
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
				<td align="center" width="<%=collectionTDwidth %>">
				<asp:Literal ID="litAddToCollection" runat="server" />
				</td>
				<td align="center" width="105"><asp:Literal ID="litDateAdded" runat="server" /></td>
			</tr>
	</AlternatingItemTemplate>
	</asp:Repeater>
				</table>
<div class="clearance"></div>
            </td>
        </tr>
    <asp:Literal ID="litClearance" runat="server" />
</ItemTemplate>
<AlternatingItemTemplate>
        <tr class="odd">
            <td align="center" height="90" valign="middle">
    <a href="/games/details.aspx?id=<%#Eval("id") %>" class="gamecover"><asp:Literal ID="litGameImage" runat="server" /></a>
            </td>
            <td align="left">	
				<table class="row-box-container games-ranking-row" width="100%">	
	<asp:Repeater ID="rptGameDetails" runat="server" OnItemDataBound="rptGamesDetails_databound">
	<ItemTemplate>
			<tr class="odd">
				<td align="right" style="padding-right: 10px;">
				<asp:Literal ID="litPlatforms" runat="server" />
				</td>
				<td width="45">
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
				<td align="center" width="<%=collectionTDwidth %>">
				<asp:Literal ID="litAddToCollection" runat="server" />
				</td>
				<td align="center" width="105"><asp:Literal ID="litDateAdded" runat="server" /></td>
			</tr>
	</ItemTemplate>
	<AlternatingItemTemplate>
			<tr class="even">
				<td align="right" style="padding-right: 10px;">
				<asp:Literal ID="litPlatforms" runat="server" />
				</td>
				<td width="45">
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
				<td align="center width="<%=collectionTDwidth %>">
				<asp:Literal ID="litAddToCollection" runat="server" />
				</td>
				<td align="center" width="105"><asp:Literal ID="litDateAdded" runat="server" /></td>
			</tr>
	</AlternatingItemTemplate>
	</asp:Repeater>
				</table>
<div class="clearance"></div>
            </td>
        </tr>
</AlternatingItemTemplate>
</asp:Repeater>
     </table>
<div class="clearance"></div>
<asp:Panel ID="pnlPagingLinks" runat="server" class="paging-links">
<table width="100%">
	<tr>
		<td align="left"><asp:Literal ID="litPreviousLink" runat="server" /></td>
		<td align="right"><asp:Literal ID="litNextLink" runat="server" /></td>
	</tr>
</table>
</asp:Panel>
</div>
</asp:Content>


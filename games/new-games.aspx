<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="new-games.aspx.cs" Inherits="games_new_games" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content3" ContentPlaceHolderID="headerContent" Runat="Server">
	<gamer:GameRateCollect ID="myGameRateCollect" name="GameRateCollect" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:GamesLeftNav ID="myGameLeftNav" name="gmrLeftNav" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" Runat="Server">
<h2>New Games</h2>
<div class="row-box-container">
<asp:Repeater ID="rptGames" runat="server" OnItemDataBound="rptGames_databound">
<ItemTemplate>
    <div class="row-box">
        <table width="100%">
            <tr>
                <td align="center" height="90" valign="bottom">
        <a href="/games/details.aspx?id=<%#Eval("id") %>"><asp:Literal ID="litGameImage" runat="server" /></a>
                </td>
            </tr>
            <tr>
                <td align="center">	
					<table>	
		<asp:Repeater ID="rptGameDetails" runat="server" OnItemDataBound="rptGamesDetails_databound">
		<ItemTemplate>
				<tr>
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
				</tr>
				<tr>
					<td align="center" colspan="2">
					<asp:Literal ID="litAddToCollection" runat="server" />
					</td>
				</tr>
		</ItemTemplate>
		</asp:Repeater>
					</table>
                </td>
            </tr>
            <tr>
                <td align="center"><asp:Literal ID="litDateAdded" runat="server" /></td>
            </tr>
        </table>
    </div>
    <asp:Literal ID="litClearance" runat="server" />
</ItemTemplate>
</asp:Repeater>
</div>
<div class="clearance"></div>
</asp:Content>

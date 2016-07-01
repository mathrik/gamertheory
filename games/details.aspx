<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="details.aspx.cs" Inherits="games_details" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content3" ContentPlaceHolderID="headerContent" Runat="Server">
	<gamer:GameRateCollect ID="myGameRateCollect" name="GameRateCollect" runat="server" />
    <gamer:FancyBox ID="fancybox" name="gmrfancybox" runat="server" />
<script type="text/javascript">
    $(document).ready(function() {
        $("a.fancybox-image").fancybox({
		    'transitionIn'	:	'elastic',
		    'transitionOut'	:	'elastic',
		    'speedIn'		:	300, 
		    'speedOut'		:	500, 
		    'overlayShow'	:	false,
		    'type'          :   'image'
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
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="manage-games.aspx.cs" Inherits="admin_manage_games" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
<script type="text/javascript">
    function removeGame(gameid) {
        if (confirm("Are you sure you wish to delete this game?")) {
            $.post('remove-game.aspx', { id: gameid},
                function(data) {
                    if (data == "success") {
                        //reload page
                        document.getElementById("li" + gameid).style.display = "none";
                    } else {
                        alert("Failed to delete game.");
                    }
            });
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AdminSidebar ID="AdminSidebar1" name="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
    <h2 class="sansam-left">Manage Games</h2>
    <div style="float:right;  padding: 15px;">
        <table>
            <tr>
                <td><input type="button" class="btn-default" onclick="document.location.href = '/admin/ae-game.aspx'" 
                    value="Add New Game" ID="btnAddGame" /></td>
            </tr>
        </table>
    </div>
    <div class="clearance"></div>
    <asp:Literal ID="litMsg" runat="server" />
    <asp:Repeater ID="rptGames" runat="server" OnItemDataBound="rptGames_DataBound" 
        onitemcommand="rptGames_ItemCommand">
        <HeaderTemplate><ul class="story-list"></HeaderTemplate>
        <ItemTemplate>
        <li class="odd" id="li<%#Eval("id") %>"><a class="game-title" name='#anchor<%#Eval("id") %>' href='/admin/ae-game.aspx?id=<%#Eval("id") %>'><%#Eval("title") %></a>
        &nbsp;&nbsp;<a href="#anchor<%#Eval("id") %>" onclick='removeGame(<%#Eval("id") %>)' title="Delete"><input type="button" class="btn-default" value="Delete" /></a>
		&nbsp;&nbsp;<a href="ae-story.aspx?t=2&g=<%#Eval("id") %>"><input type="button" class="btn-default" value="Add Review" /></a>
		</li>
        </ItemTemplate>
        <AlternatingItemTemplate>
        <li class="even" id="li<%#Eval("id") %>"><a class="game-title" name='#anchor<%#Eval("id") %>' href='/admin/ae-game.aspx?id=<%#Eval("id") %>'><%#Eval("title") %></a>
        &nbsp;&nbsp;<a href="#anchor<%#Eval("id") %>" onclick='removeGame(<%#Eval("id") %>)' title="Delete"><input type="button" class="btn-default" value="Delete" /></a>
		&nbsp;&nbsp;<a href="ae-story.aspx?t=2&g=<%#Eval("id") %>"><input type="button" class="btn-default" value="Add Review" /></a></li>
        </AlternatingItemTemplate>
        <FooterTemplate></ul></FooterTemplate>
    </asp:Repeater>
</asp:Content>


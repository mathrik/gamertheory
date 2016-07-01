<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="manage-stories.aspx.cs" Inherits="account_manage_stories" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">

<script type="text/javascript">
	function removeStory(storyid) {
		if (confirm("Are you sure you wish to delete this review?")) {
			$.post('/admin/remove-story.aspx', { id: storyid },
                function (data) {
                	if (data == "success") {
                		//reload page
                		document.getElementById("li" + storyid).style.display = "none";
                	} else {
                		alert("Failed to delete story.");
                	}
                });
		}
	}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
	<gamer:AccountSidebar ID="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
	<h2>User Reviews</h2>
	<asp:Literal ID="litMsg" runat="server" />
    <div class="clearance"></div>
    <asp:Repeater ID="rptGames" runat="server" OnItemDataBound="rptGames_DataBound" 
        onitemcommand="rptGames_ItemCommand">
        <HeaderTemplate><ul class="story-list"></HeaderTemplate>
        <ItemTemplate>
        <li class="odd" id='li<%#Eval("id") %>'><a name='anchor<%#Eval("id") %>' href="<%#Eval("permalink") %>"><%#Eval("title") %></a>
        &nbsp;&nbsp;<a href="#anchor<%#Eval("id") %>" onclick='removeStory(<%#Eval("id") %>)' title="Delete"><img src="/images/icons/delete.png" alt="Delete" title="Delete" style="display: inline;" /></a>
        </li>
        </ItemTemplate>
        <AlternatingItemTemplate>
        <li class="even" id='li<%#Eval("id") %>'><a name='anchor<%#Eval("id") %>' href="<%#Eval("permalink") %>"><%#Eval("title") %></a>
        &nbsp;&nbsp;<a href="#anchor<%#Eval("id") %>" onclick='removeStory(<%#Eval("id") %>)' title="Delete"><img src="/images/icons/delete.png" alt="Delete" title="Delete" style="display: inline;" /></a>
        </li>
        </AlternatingItemTemplate>
        <FooterTemplate></ul></FooterTemplate>
    </asp:Repeater>
    <div class="clearance"></div>
</asp:Content>


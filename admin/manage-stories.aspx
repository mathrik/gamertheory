<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="manage-stories.aspx.cs" Inherits="admin_manage_stories" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content3" ContentPlaceHolderID="headerContent" Runat="Server">
<script type="text/javascript">
    function removeStory(storyid) {
        if (confirm("Are you sure you wish to delete this story?")) {
            $.post('remove-story.aspx', { id: storyid},
                function(data) {
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
<asp:Content ID="Content1" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AdminSidebar ID="AdminSidebar1" name="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" Runat="Server">
    <ul class="tab-list">
        <li><a href="manage-stories.aspx?t=0">All</a></li>
        <asp:Repeater ID="rptTabs" runat="server">
        <ItemTemplate>
            <li><a href='manage-stories.aspx?t=<%#Eval("value") %>'><%#Eval("text") %></a></li>
        </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="clearance"></div>
    <div style="float:right;  padding: 15px;">
        <table>
            <tr>
                <td><input type="button" class="btn-default" onclick="showDropDown()" value="Add New Story" ID="btnShowOptions" /></td>
                <td><asp:DropDownList ID="ddStoryType" runat="server" /></td>
                <td><input type="button" class="btn-default" onclick="addStory()" value="Go" ID="btnAddStory" /></td>
            </tr>
        </table>
    </div>
    <div class="clearance"></div>
    <asp:Literal ID="litMsg" runat="server" />
    <div class="clearance"></div>
    <asp:Repeater ID="rptGames" runat="server" OnItemDataBound="rptGames_DataBound" 
        onitemcommand="rptGames_ItemCommand">
        <HeaderTemplate><ul class="story-list"></HeaderTemplate>
        <ItemTemplate>
        <li class="odd" id='li<%#Eval("id") %>'><a name='anchor<%#Eval("id") %>' href='/admin/ae-story.aspx?id=<%#Eval("id") %>&t=<%#Eval("typeID") %>'><%#Eval("title") %></a>
        &nbsp;&nbsp;<a href="#anchor<%#Eval("id") %>" onclick='removeStory(<%#Eval("id") %>)' title="Delete">(-)</a>
        </li>
        </ItemTemplate>
        <AlternatingItemTemplate>
        <li class="even" id='li<%#Eval("id") %>'><a name='anchor<%#Eval("id") %>' href='/admin/ae-story.aspx?id=<%#Eval("id") %>&t=<%#Eval("typeID") %>'><%#Eval("title") %></a>
        &nbsp;&nbsp;<a href="#anchor<%#Eval("id") %>" onclick='removeStory(<%#Eval("id") %>)' title="Delete">(-)</a>
        </li>
        </AlternatingItemTemplate>
        <FooterTemplate></ul></FooterTemplate>
    </asp:Repeater>
    <div class="clearance"></div>
    <script type="text/javascript">
         $(document).ready(function() {
            $('#<%=ddStoryType.ClientID %>').hide();
            $('#btnAddStory').hide();
        });
    
        function showDropDown() {
            $('#<%=ddStoryType.ClientID %>').show();
            $('#btnAddStory').show();
        }
        
        function addStory() {
            document.location.href = "/admin/ae-story.aspx?t=" + $('#<%=ddStoryType.ClientID %>').val();
        }
    </script>
</asp:Content>


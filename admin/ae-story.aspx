<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ae-story.aspx.cs" Inherits="admin_ae_story" Title="Untitled Page" ValidateRequest="false" %>
<asp:Content ID="hdrContent" ContentPlaceHolderID="headerContent" runat="server">
    <script type="text/javascript" src="/js/jquery.autocomplete.js"></script>
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var GLOBAL_STORY = <asp:Literal ID="litStoryID" runat="server" />;
        function removeItem(itemID, itemtype) {
            $.post('remove-story-item.aspx', { item: itemID, type: itemtype, story: GLOBAL_STORY},
                function(data) {
                    if (data == "success") {
                        $.post('story-attachments.aspx', {story: GLOBAL_STORY, type: itemtype},
                            function(data) {
                                switch (itemtype) {
                                    case "developer":
                                        document.getElementById("developers-container").innerHTML = data;
                                        break;     
                                    case "game":
                                        document.getElementById("games-container").innerHTML = data;
                                        break;    
                                    case "genre":
                                        document.getElementById("genres-container").innerHTML = data;
                                        break;    
                                    case "platform":
                                        document.getElementById("platforms-container").innerHTML = data;
                                        break;    
                                    case "publisher":
                                        document.getElementById("publishers-container").innerHTML = data;
                                        break; 
                                }
                        });
                    } else {
                        alert("Failed to remove item from this story.");
                    }
            });
        }
        
        function addItem(textBox, itemtype) {
            var mytitle = document.getElementById(textBox).value;
            if (mytitle.length > 0) {
                $.post('attach-story-item.aspx', { title: mytitle, type: itemtype, story: GLOBAL_STORY},
                    function(data) {
                        if (data == "success") {
                            $.post('story-attachments.aspx', {story: GLOBAL_STORY, type: itemtype},
                                function(data) {
                                    switch (itemtype) {
                                        case "developer":
                                            document.getElementById("developers-container").innerHTML = data;
                                            break;    
                                        case "game":
                                            document.getElementById("games-container").innerHTML = data;
                                            break;    
                                        case "genre":
                                            document.getElementById("genres-container").innerHTML = data;
                                            break;    
                                        case "platform":
                                            document.getElementById("platforms-container").innerHTML = data;
                                            break;    
                                        case "publisher":
                                            document.getElementById("publishers-container").innerHTML = data;
                                            break;    
                                    }
                            });
                        } else {
                            alert("Failed to add item to this story.");
                        }
                });
            } else {
                alert("No data found.");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AdminSidebar ID="myAdminSidebar" name="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" Runat="Server">
<script type="text/javascript">
    tinyMCE.init({
        mode : "textareas",
        theme : "advanced",
        editor_selector : "mceEditor",
       	plugins: "fileLibrary",
       	theme_advanced_buttons3: "fileLibrary",
        theme_advanced_toolbar_location : "top",
		relative_urls : false,
       	pageID: <%=storyID %>
    });
    
    $(document).ready(function() {
        $('#<%=txtDev.ClientID %>').autocompleteArray([<asp:Literal ID="litDev" runat="server" />]);
        $('#<%=txtGame.ClientID %>').autocomplete("/admin/search-games.aspx");
        $('#<%=txtGenre.ClientID %>').autocompleteArray([<asp:Literal ID="litGenre" runat="server" />]);
        $('#<%=txtPlatform.ClientID %>').autocompleteArray([<asp:Literal ID="litPlatform" runat="server" />]);
        $('#<%=txtPublisher.ClientID %>').autocompleteArray([<asp:Literal ID="litPublisher" runat="server" />]);
    });
</script>
<asp:Panel ID="pnlStory" runat="server" DefaultButton="btnSubmit" >
<table class="account-form">
    <tr>
        <td align="right"><asp:Label ID="lblTitle" AssociatedControlID="txtTitle" runat="server">Title:</asp:Label></td>
        <td><asp:TextBox ID="txtTitle" runat="server" /></td>
        <td><asp:DropDownList ID="ddPublish" runat="server">
			<asp:ListItem Text="Draft" Value="0" />
			<asp:ListItem Text="Publish" Value="1" />
		</asp:DropDownList></td>
    </tr>
    <tr>
        <td align="right"><asp:Label ID="lblFeatured" AssociatedControlID="chkFeatured" runat="server">Featured?</asp:Label></td>
        <td colspan="2"><asp:CheckBox ID="chkFeatured" runat="server" /></td>
    </tr>
    <tr>
        <td align="right"><asp:Label ID="Label6" AssociatedControlID="fileStoryThumbnail" runat="server">Thumbnail:</asp:Label></td>
        <td colspan="2"><asp:FileUpload ID="fileStoryThumbnail" runat="server" /> <asp:Literal ID="litStoryThumbnail" runat="server" /></td>
    </tr>
</table>
<asp:Panel ID="pnlItemAttachments" runat="server">
<table class="account-form">
    <tr>
        <td>
            <asp:Label ID="label2" AssociatedControlID="txtDev" runat="server">Related Developers</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
        <div id="developers-container">
        <asp:Repeater ID="rptStoryDevs" runat="server">
            <HeaderTemplate><ul class="relation-list"></HeaderTemplate>
            <ItemTemplate>
                <li><%#Eval("title") %> &nbsp; <input type="button" value="Remove" class="btn-default" onclick='removeItem(<%#Eval("id") %>, "developer");'/></li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
        </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtDev" runat="server" /> &nbsp; <input type="button" value="Add" class="btn-default" onclick='addItem("<%=txtDev.ClientID %>", "developer");'/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="label3" AssociatedControlID="txtGame" runat="server">Related Games</asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2"><div id="games-container"><asp:Repeater ID="rptStoryGames" runat="server">
            <HeaderTemplate><ul class="relation-list"></HeaderTemplate>
            <ItemTemplate>
                <li><%#Eval("title") %> &nbsp; <input type="button" value="Remove" class="btn-default" onclick='removeItem(<%#Eval("id") %>, "game");'/></li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater></div></td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtGame" runat="server" /> &nbsp; <input type="button" value="Add" class="btn-default" onclick='addItem("<%=txtGame.ClientID %>", "game");'/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="labelGenre" AssociatedControlID="txtGenre" runat="server">Related Genres</asp:Label>
        </td>
    </tr>
    <tr>
        <td><div id="genres-container"><asp:Repeater ID="rptStoryGenres" runat="server">
            <HeaderTemplate><ul class="relation-list"></HeaderTemplate>
            <ItemTemplate>
                <li><%#Eval("title") %> &nbsp; <input type="button" value="Remove" class="btn-default" onclick='removeItem(<%#Eval("id") %>, "genre");'/></li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater></div></td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtGenre" runat="server" /> &nbsp; <input type="button" value="Add" class="btn-default" onclick='addItem("<%=txtGenre.ClientID %>", "genre");'/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="label4" AssociatedControlID="txtPlatform" runat="server">Related Platforms</asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2"><div id="platforms-container"><asp:Repeater ID="rptStoryPlatforms" runat="server">
            <HeaderTemplate><ul class="relation-list"></HeaderTemplate>
            <ItemTemplate>
                <li><%#Eval("title") %> &nbsp; <input type="button" value="Remove" class="btn-default" onclick='removeItem(<%#Eval("id") %>, "platform");'/></li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater></div></td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtPlatform" runat="server" /> &nbsp; <input type="button" value="Add" class="btn-default" onclick='addItem("<%=txtPlatform.ClientID %>", "platform");'/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="label5" AssociatedControlID="txtPublisher" runat="server">Related Publishers</asp:Label></td>
    </tr>
    <tr>
        <td colspan="2"><div id="publishers-container"><asp:Repeater ID="rptStoryPublishers" runat="server">
            <HeaderTemplate><ul class="relation-list"></HeaderTemplate>
            <ItemTemplate>
                <li><%#Eval("title") %> &nbsp; <input type="button" value="Remove" class="btn-default" onclick='removeItem(<%#Eval("id") %>, "publisher");'/></li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater></div></td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtPublisher" runat="server" /> &nbsp; <input type="button" value="Add" class="btn-default" onclick='addItem("<%=txtPublisher.ClientID %>", "publisher");'/>
        </td>
    </tr>
</table>
</asp:Panel>
<table class="account-form">
    <tr>
        <td>
           <asp:Label ID="lblPreview" AssociatedControlID="txtPreview" runat="server">Preview</asp:Label>
<asp:TextBox ID="txtPreview" runat="server" TextMode="MultiLine" Rows="8" cols="120" Width="500" CssClass="mceEditor" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
           <asp:Label ID="Label1" AssociatedControlID="txtStory" runat="server">Full Story</asp:Label>
<asp:TextBox ID="txtStory" runat="server" TextMode="MultiLine" Rows="34" cols="120" Width="500" CssClass="mceEditor" ></asp:TextBox>
        </td>
    </tr>
	<tr>
		<td><asp:Label ID="lblPodcastFile" AssociatedControlID="filePodcast" runat="server" Text="Podcast File" />
		<asp:FileUpload ID="filePodcast" runat="server" />
		</td>
	</tr>
    <tr>
        <td>
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn-default" Text="Submit" onclick="btnSubmit_Click" />
        &nbsp;&nbsp;
        <input type="button" value="Cancel" class="btn-default" onclick="window.history.go(-1)" />
        </td>
    </tr>
</table>
</asp:Panel>
</asp:Content>


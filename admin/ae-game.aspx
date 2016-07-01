<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ae-game.aspx.cs" Inherits="admin_ae_game" Title="Untitled Page" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
    <script type="text/javascript" src="/js/jquery.autocomplete.js"></script>
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <gamer:FancyBox ID="fancybox" name="gmrfancybox" runat="server" />
    <script type="text/javascript">
        function removeGameInstance(instanceid) {
            if (confirm("Are you sure you wish to delete this game instance?")) {
                $.post('remove-game-instance.aspx', { id: instanceid},
                    function(data) {
                        if (data == "success") {
                            //reload page
                            document.getElementById("divHideMeLink" + instanceid).style.display = "none";
                            document.getElementById("divHideMeDate" + instanceid).style.display = "none";
                            document.getElementById("divHideMeAnchor" + instanceid).style.display = "none";
                        } else {
                            alert("Failed to delete game.");
                        }
                });
            }
        }
        
        function removeGameScreenshot(imageID) {
            if (confirm("Are you sure you wish to delete this game image?")) {
                $.post('remove-game-image.aspx', { id: imageID},
                    function(data) {
                        if (data == "success") {
                            //reload page
                            document.getElementById("sscontainer" + imageID).style.display = "none";
                        } else {
                            alert("Failed to delete game image.");
                        }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AdminSidebar ID="myAdminSidebar" name="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
<script type="text/javascript">
    tinyMCE.init({
        mode : "textareas",
        theme : "advanced",
        editor_selector : "mceEditor",
        theme_advanced_toolbar_location : "top"
    });
</script>
<asp:Literal ID="litMsg" runat="server"></asp:Literal>
<asp:Panel ID="pnlAddGame" runat="server" DefaultButton="btnAdd">
<table class="account-form">
    <tr>
        <td align="right"><asp:Label ID="lblTitle" AssociatedControlID="txtAddTitle" runat="server">Title:</asp:Label></td>
        <td><asp:TextBox ID="txtAddTitle" runat="server" /></td>
    </tr>
    <tr>
        <td align="right"><asp:Label ID="lblchkApproved" AssociatedControlID="chkAddApproved" runat="server">Show on site?</asp:Label></td>
        <td><asp:CheckBox ID="chkAddApproved" runat="server" /></td>
    </tr>
    <tr>
        <td align="right"><asp:Label ID="Label5" AssociatedControlID="txtAddReleaseDate" runat="server">Release Date</asp:Label></td>
        <td><asp:TextBox ID="txtAddReleaseDate" runat="server" /></td>
    </tr>
    <tr>
        <td align="right"><asp:Label ID="Label6" AssociatedControlID="fileCover" runat="server">Cover Image</asp:Label></td>
        <td><asp:FileUpload ID="fileCover" runat="server" /></td>
    </tr>
	<tr>
		<td align="right" valign="top"><asp:Label ID="Label3" AssociatedControlID="chk360" runat="server">Platforms:</asp:Label></td>
		<td>
			<asp:CheckBox ID="chk360" runat="server" Text="360" />
			<asp:CheckBox ID="chk3ds" runat="server" Text="3DS" />
			<asp:CheckBox ID="chkpc" runat="server" Text="PC" />
			<asp:CheckBox ID="chkps3" runat="server" Text="PS3" />
			<asp:CheckBox ID="chkwii" runat="server" Text="Wii" />
			
		</td>
	</tr>
</table>
<table class="account-form">
    <tr>
        <td colspan="2">
           <asp:Label ID="Label1" AssociatedControlID="txtAddNotes" runat="server">Notes</asp:Label>
<asp:TextBox ID="txtAddNotes" runat="server" TextMode="MultiLine" Rows="34" cols="120" Width="500" CssClass="mceEditor" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
        <asp:Button ID="btnAdd" runat="server" CssClass="btn-default" Text="Add Game" onclick="btnAdd_Click" />
        </td>
    </tr>
</table>
</asp:Panel>
<asp:Panel ID="pnlEditGame" runat="server" DefaultButton="btnUpdateGame">
<table class="account-form">
    <tr>
        <td align="right"><asp:Label ID="Label2" AssociatedControlID="txtTitle" runat="server">Title:</asp:Label></td>
        <td><asp:TextBox ID="txtTitle" runat="server" /></td>
    </tr>
    <tr>
        <td align="right"><asp:Label ID="Label4" AssociatedControlID="chkApproved" runat="server">Show on site?</asp:Label></td>
        <td><asp:CheckBox ID="chkApproved" runat="server" /></td>
    </tr>
</table>
<table class="account-form">
    <tr>
        <td colspan="2">
           <asp:Label ID="Label7" AssociatedControlID="txtNotes" runat="server">Notes</asp:Label>
<asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="34" cols="120" Width="500" CssClass="mceEditor" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
        <asp:Button ID="btnUpdateGame" runat="server" CssClass="btn-default" Text="Update" onclick="btnUpdateGame_Click" />
        </td>
    </tr>
</table>
</asp:Panel>
<asp:Panel ID="pnlInstances" runat="server">
<h2 class="sansam-left">Available on these Platforms</h2>
<div style="float:right; padding-top: 4px;">
    <table>
        <tr>
            <td><asp:Literal ID="litAddInstance" runat="server" /></td>
        </tr>
    </table>
</div>
<div class="clearance"></div>
<table class="account-form">
<asp:Repeater ID="rptInstances" runat="server" OnItemDataBound="rptInstances_DataBound">
    <ItemTemplate>
    <tr>
        <td><div id="divHideMeLink<%#Eval("id") %>"><asp:Literal ID="litEditLink" runat="server" /></div></td>
        <td><div id="divHideMeDate<%#Eval("id") %>"><asp:Literal ID="litReleaseDate" runat="server" /></div></td>
        <td><div id="divHideMeAnchor<%#Eval("id") %>"><a name="anchor<%#Eval("id") %>">&nbsp;</a><a href="#anchor<%#Eval("id") %>" onclick='removeGameInstance(<%#Eval("id") %>)' title="Delete">(-)</a></div></td>
    </tr>
    </ItemTemplate>
</asp:Repeater>
</table>
<h2 class="sansam-left">Images</h2>
<div style="float:right; padding-top: 4px;">
    <table>
        <tr>
            <td><asp:Literal ID="litAddImage" runat="server" /></td>
        </tr>
    </table>
</div>
<div class="clearance"></div>
<asp:Repeater ID="rptImages" runat="server">
    <ItemTemplate>
    <div class="screenshot-container" id="sscontainer<%#Eval("id") %>">
        <img src="/uploads/games/thumbs/75_<%#Eval("filename") %>" alt="Screenshot <%#Eval("id") %>" />
        <a name="ss_anchor<%#Eval("id") %>">&nbsp;</a>
        <a href="#ss_anchor<%#Eval("id") %>" onclick='removeGameScreenshot(<%#Eval("id") %>)' title="Delete">(-)</a>
    </div>
    </ItemTemplate>
</asp:Repeater>
<script type="text/javascript">
	$(document).ready(function () {
		$("a.fancy-box-iframe").fancybox({
			'transitionIn': 'elastic',
			'transitionOut': 'elastic',
			'speedIn': 300,
			'speedOut': 500,
			'overlayShow': false,
			'type': 'iframe',
			'width': 620,
			'height': 450,
			'onClosed': function () {
				parent.location.reload(true);
			}
		});
	});
</script>
</asp:Panel>
</asp:Content>


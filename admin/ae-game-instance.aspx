<%@ Page Language="C#" MasterPageFile="~/Popup.master" AutoEventWireup="true" CodeFile="ae-game-instance.aspx.cs" Inherits="admin_ae_game_instance"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
    <script type="text/javascript" src="/js/jquery.autocomplete.js"></script>
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $('#<%=txtPlatform.ClientID %>').autocompleteArray([<asp:Literal ID="litPlatform" runat="server" />]);
            $('#<%=txtDev.ClientID %>').autocompleteArray([<asp:Literal ID="litDev" runat="server" />]);
            $('#<%=txtGenre.ClientID %>').autocompleteArray([<asp:Literal ID="litGenre" runat="server" />]);
            $('#<%=txtPublisher.ClientID %>').autocompleteArray([<asp:Literal ID="litPublisher" runat="server" />]);
        });
    </script>
    <script type="text/javascript">
        var GLOBAL_INSTANCE = <asp:Literal ID="litInstanceID" runat="server" />;
        function removeItem(itemID, itemtype) {
            $.post('remove-instance-item.aspx', { item: itemID, type: itemtype, instance: GLOBAL_INSTANCE},
                function(data) {
                    if (data == "success") {
                        $.post('game-instance-attachments.aspx', {instance: GLOBAL_INSTANCE, type: itemtype},
                            function(data) {
                                switch (itemtype) {
                                    case "developer":
                                        document.getElementById("developers-container").innerHTML = data;
                                        break;   
                                    case "genre":
                                        document.getElementById("genres-container").innerHTML = data;
                                        break;   
                                    case "publisher":
                                        document.getElementById("publishers-container").innerHTML = data;
                                        break; 
                                }
                        });
                    } else {
                        alert("Failed to remove item from this instance.");
                    }
            });
        }
        
        function addItem(textBox, itemtype) {
            var mytitle = document.getElementById(textBox).value;
            if (mytitle.length > 0) {
                $.post('attach-instance-item.aspx', { title: mytitle, type: itemtype, instance: GLOBAL_INSTANCE},
                    function(data) {
                        if (data == "success") {
                            $.post('game-instance-attachments.aspx', {instance: GLOBAL_INSTANCE, type: itemtype},
                                function(data) {
                                    switch (itemtype) {
                                        case "developer":
                                            document.getElementById("developers-container").innerHTML = data;
                                            break;   
                                        case "genre":
                                            document.getElementById("genres-container").innerHTML = data;
                                            break;   
                                        case "publisher":
                                            document.getElementById("publishers-container").innerHTML = data;
                                            break; 
                                    }
                            });
                        } else {
                            alert("Failed to add item to this instance.");
                        }
                });
            } else {
                alert("No data found.");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
<asp:Literal ID="litMsg" runat="server" />
<asp:Panel ID="pnlItemAttachments" runat="server">
<table class="account-form">
    <tr>
        <td>
            <asp:Label ID="label2" AssociatedControlID="txtDev" runat="server">Related Developers</asp:Label>
        </td>
    </tr>
    <tr>
        <td><div id="developers-container"><asp:Repeater ID="rptInstanceDevs" runat="server">
            <HeaderTemplate><ul class="relation-list"></HeaderTemplate>
            <ItemTemplate>
                <li><%#Eval("title") %> &nbsp; <input type="button" value="Remove" class="btn-default" onclick='removeItem(<%#Eval("id") %>, "developer");'/></li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater></div></td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtDev" runat="server" /> &nbsp; <input type="button" value="Add" class="btn-default" onclick='addItem("<%=txtDev.ClientID %>", "developer");'/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="labelGenre" AssociatedControlID="txtGenre" runat="server">Related Genres</asp:Label>
        </td>
    </tr>
    <tr>
        <td><div id="genres-container"><asp:Repeater ID="rptInstanceGenres" runat="server">
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
            <asp:Label ID="label5" AssociatedControlID="txtPublisher" runat="server">Related Publishers</asp:Label></td>
    </tr>
    <tr>
        <td colspan="2"><div id="publishers-container"><asp:Repeater ID="rptInstancePublishers" runat="server">
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
<asp:Panel ID="pnlInstance" runat="server" DefaultButton="btnSubmit" >
<table class="account-form">
    <tr>
        <td align="right"><asp:Label ID="lblPlatform" runat="server" AssociatedControlID="txtPlatform">Platform</asp:Label></td>
        <td><asp:TextBox ID="txtPlatform" runat="server" /></td>
    </tr>
    <tr>
        <td align="right"><asp:Label ID="lblchkApproved" AssociatedControlID="chkApproved" runat="server">Show on site?</asp:Label></td>
        <td><asp:CheckBox ID="chkApproved" runat="server" /></td>
    </tr>
    <tr>
        <td align="right"><asp:Label ID="Label1" AssociatedControlID="txtReleaseDate" runat="server">Release Date</asp:Label></td>
        <td><asp:Textbox ID="txtReleaseDate" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Literal ID="litCoverImage" runat="server" />
        </td>
    </tr>
    <tr>
        <td align="right"><asp:Label ID="Label3" AssociatedControlID="fileCover" runat="server">Cover Image</asp:Label></td>
        <td><asp:FileUpload ID="fileCover" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="2">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn-default" Text="Submit" onclick="btnSubmit_Click" />
        </td>
    </tr>
</table>
</asp:Panel>
</asp:Content>


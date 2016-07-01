<%@ Page Language="C#" MasterPageFile="~/Popup.master" AutoEventWireup="true" CodeFile="add-game-image.aspx.cs" Inherits="admin_add_game_image" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
    <asp:Literal ID="litMsg" runat="server" />
    <table class="account-form">
        <tr>
            <td align="right"><asp:Label ID="lblTitle" AssociatedControlID="fileImage" runat="server">File:</asp:Label></td>
            <td><asp:FileUpload ID="fileImage" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
            <asp:Button ID="btnUpload" runat="server" CssClass="btn-default" Text="Upload" onclick="btnUpload_Click" />
            </td>
        </tr>
    </table>
</asp:Content>


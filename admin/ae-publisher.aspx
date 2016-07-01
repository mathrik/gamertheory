<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ae-publisher.aspx.cs" Inherits="admin_ae_publisher" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AdminSidebar ID="myAdminSidebar" name="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
<asp:Literal ID="litMsg" runat="server" />
<asp:Panel ID="pnlAddEdit" runat="server" DefaultButton="btnSubmit" >
<table class="account-form">
    <tr>
        <td align="right"><asp:Label ID="lblTitle" AssociatedControlID="txtTitle" runat="server">Title:</asp:Label></td>
        <td><asp:TextBox ID="txtTitle" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="2">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn-default" Text="Submit" onclick="btnSubmit_Click" />
        </td>
    </tr>
</table>
</asp:Panel>
</asp:Content>


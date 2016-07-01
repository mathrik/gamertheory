<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="manage-sidebar-ad.aspx.cs" Inherits="admin_manage_sidebar_ad" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AdminSidebar ID="AdminSidebar1" name="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
<asp:Literal ID="litMsg" runat="server" />
<table class="account-form">
    <tr>
        <td align="right"><asp:Label ID="lblTitle" AssociatedControlID="txtURL" runat="server">URL:</asp:Label></td>
        <td><asp:TextBox ID="txtURL" runat="server" /></td>
    </tr>
	<tr>
		<td colspan="2">
			<asp:Literal ID="litCurrentBillboard" runat="server" />
		</td>
	</tr>
    <tr>
        <td align="right"><asp:Label ID="Label1" AssociatedControlID="fileBillboard" runat="server">New File: (250x518) </asp:Label></td>
        <td><asp:FileUpload ID="fileBillboard" runat="server" /></td>
    </tr>
    <tr>
        <td colspan="2">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn-default" Text="Submit" onclick="btnSubmit_Click" />
        </td>
    </tr>
</table>
</asp:Content>


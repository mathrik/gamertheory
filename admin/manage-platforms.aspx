﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="manage-platforms.aspx.cs" Inherits="admin_manage_platforms" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AdminSidebar ID="AdminSidebar1" name="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
    <div style="float:right;  padding: 15px;">
        <table>
            <tr>
                <td><input type="button" class="btn-default" value="Add New Platform" ID="btnAddPlatform" 
                    onclick="document.location.href='/admin/ae-platform.aspx';" /></td>
            </tr>
        </table>
    </div>
    <div class="clearance"></div>
    <asp:Literal ID="litMsg" runat="server" />
    <asp:Repeater ID="rptPlatforms" runat="server" OnItemDataBound="rptPlatforms_DataBound" 
        onitemcommand="rptPlatforms_ItemCommand">
        <HeaderTemplate><ul class="story-list"></HeaderTemplate>
        <ItemTemplate>
        <li class="odd"><a href='/admin/ae-platform.aspx?id=<%#Eval("id") %>'><%#Eval("title") %></a></li>
        </ItemTemplate>
        <AlternatingItemTemplate>
        <li class="even"><a href='/admin/ae-platform.aspx?id=<%#Eval("id") %>'><%#Eval("title") %></a></li>
        </AlternatingItemTemplate>
        <FooterTemplate></ul></FooterTemplate>
    </asp:Repeater>
</asp:Content>


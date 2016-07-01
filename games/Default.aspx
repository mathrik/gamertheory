<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="games_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:GamesLeftNav ID="myGameLeftNav" name="gmrLeftNav" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" Runat="Server">
    This game totally rocks!
</asp:Content>


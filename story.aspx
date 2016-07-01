<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="story.aspx.cs" Inherits="story" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:GamesLeftNav ID="myGameLeftNav" name="gmrLeftNav" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
<h2><asp:Literal ID="litTitle" runat="server" /></h2>
<p>By: <asp:Literal ID="litByline" runat="server" /></p>
<asp:Literal ID="litBody" runat="server" />
<div class="clearance"></div>
<gamer:Comments ID="gamerComments" runat="server" />
</asp:Content>

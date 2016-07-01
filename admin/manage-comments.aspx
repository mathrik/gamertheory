<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="manage-comments.aspx.cs" Inherits="admin_manage_comments" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
	<script type="text/javascript">
		function approvecomment(cID) {
			$.get("/ajax/approve-comment.aspx", { id: cID }, function (data) {
				if (data == "success") {
					$("#comment" + cID).html("<em>Comment approved.</em>");
				} else {
					alert("Error: " + data);
				}
			});
		}

		function deletecomment(cID) {
			if (confirm("Are you sure you wish to delete this comment?")) {
				$.get("/ajax/delete-comment.aspx", { id: cID }, function (data) {
					if (data == "success") {
						$("#comment" + cID).html("<em>Comment deleted.</em>");
					} else {
						alert("Error: " + data);
					}
				});
			}
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AdminSidebar ID="AdminSidebar1" name="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
	<asp:Literal ID="litMsg" runat="server" />
    <div class="subpage-container">
		<div class="subpage-legend">Comments</div>
		<div id="comments-table-container" class="subpage-table-container">
	<table class="row-box-container">
	<asp:Repeater ID="rptComments" runat="server" OnItemDataBound="rptComments_databound">
	<ItemTemplate>
	<tr>
		<td><asp:Literal ID="litThumbnail" runat="server" /></td>
		<td>
			<span class="title"><%#Eval("username") %></span>
			<div id="comment<%#Eval("id") %>">
			<em><asp:Literal ID="litPostDate" runat="server" /></em><br />
			<%#Eval("body") %>
			<br />
			<a href="#" onclick="approvecomment(<%#Eval("id") %>); return false;">Approve</a>
			&nbsp;&nbsp;
			<a href="#" onclick="deletecomment(<%#Eval("id") %>); return false;">Delete</a>
			</div>
		</td>
	</tr>
	</ItemTemplate>
	<AlternatingItemTemplate>
	<tr class="odd">
		<td><asp:Literal ID="litThumbnail" runat="server" /></td>
		<td>
			<span class="title"><%#Eval("username") %></span>
			<div id="comment<%#Eval("id") %>">
			<em><asp:Literal ID="litPostDate" runat="server" /></em><br />
			<%#Eval("body") %>
			<br />
			<a href="#" onclick="approvecomment(<%#Eval("id") %>); return false;">Approve</a>
			&nbsp;&nbsp;
			<a href="#" onclick="deletecomment(<%#Eval("id") %>); return false;">Delete</a>
			</div>
		</td>
	</tr>
	</AlternatingItemTemplate>
	</asp:Repeater>
	</table>
	</div>
	<asp:Panel ID="pnlPagingLogic" runat="server" class="paging-logic">
	<asp:Literal ID="litPagingLinks" runat="server" />
	</asp:Panel>
	</div>
<div class="clearance"></div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="manage-users.aspx.cs" Inherits="admin_manage_users" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentLeft" Runat="Server">
    <gamer:AdminSidebar ID="AdminSidebar1" name="gmrSidebar" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" Runat="Server">
    <h2 class="sansam-left">Manage Users</h2>
	<div class="clearance"></div>
	<asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
		<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
		<asp:Button ID="btnSearch" runat="server" Text="Search" 
			onclick="btnSearch_Click" class="btn-default" />
	</asp:Panel>
	<asp:Panel ID="pnlResults" runat="server">
		<script type="text/javascript">
			var GLOBALUSER = 0;

			function suspendUser(u, p, e) {
				$.post('/admin/suspend_user.aspx', { u: u, p: p, e: e }, function (data) {
					if (data == "success") {
						$("#message" + u).html("User suspended.");
					} else {
						alert("An error occurred: " + data);
					}
				});
			}

			function changeUser(u, t) {
				$.post('/admin/set_usertype.aspx', { u: u, t: t }, function (data) {
					if (data == "success") {
						$("#message" + u).html("User updated.");
					} else {
						alert("An error occurred: " + data);
					}
				});
			}

			function showSuspensionForm(userID) {
				GLOBALUSER = userID;
				$("#suspension-form").show();
				$("#userlevel-form").hide();
			}

			function showUserLevelForm(userID) {
				GLOBALUSER = userID;
				$("#userlevel-form").show();
				$("#suspension-form").hide();
			}

			function doSuspension() {
				if (confirm("Are you sure you wish to suspend this user?")) {
					var p = document.getElementById("permanent").value;
					if (p = "yes") { p = 1; } else { p = 0; }
					var e = document.getElementById("suspension-expiration").value;
					suspendUser(GLOBALUSER, p, e);
					$("#suspension-form").hide();
				}
			}

			function doUserUpdate() {
				if (confirm("Are you sure you wish to update this user?")) {
					var t = document.getElementById("newuserlevel").value;
					changeUser(GLOBALUSER, t);
					$("#userlevel-form").hide();
				}
			}
		</script>
		<div id="suspension-form" style="display:none;">
			End Date: <input type="text" id="suspension-expiration" />
			<br />
			<input type="checkbox" id="permanent" value="yes" />Permanent?
			<br />
			<input type="button" onclick="doSuspension(); return false;" value="Suspend User" class="btn-default" />
		</div>
		<div id="userlevel-form" style="display:none;">
			<select id="newuserlevel">
				<option value="1">User</option>
				<option value="2">Author</option>
				<option value="3">Forum Mod</option>
				<option value="4">Editor</option>
				<option value="5">Admin</option>
			</select>
			<input type="button" onclick="doUserUpdate(); return false;" value="Update User" class="btn-default" />
		</div>
		<table width="100%">
			<tr>
				<th>Username</th>
				<th>User Type</th>
				<th>Status</th>
				<th></th>
			</tr>
		<asp:Repeater ID="rptUsers" runat="server" OnItemDataBound="rptUsers_DataBound">
			<ItemTemplate>
				<tr>
					<td><%#Eval("username") %></td>
					<td><asp:Literal ID="litUsertype" runat="server"/></td>
					<td><asp:Literal ID="litSuspension" runat="server" /></td>
					<td><div id="message<%#Eval("id") %>"></div></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		</table>
	</asp:Panel>
</asp:Content>


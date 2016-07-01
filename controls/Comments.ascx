<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Comments.ascx.cs" Inherits="controls_Comments" %>
<script type="text/javascript">
	function getpage(pagenum) {
		for (var i = 1; i <= <%=numpages %>; i++) {
			$("#comment-paging-link" + i).css("text-decoration", "none");
		}
		$.get("/ajax/fetch-comments-page.aspx", { t: "<%=objecttype %>", id: "<%=objectID %>", p: pagenum, c: "<%=commentcount %>" }, function (data) {
			$("#comments-table-container").html(data);
			$("#comment-paging-link" + pagenum).css("text-decoration", "underline");
		});
	}

	function reportcomment(cID) {
		$.get("/ajax/flag-comment.aspx", {id: cID}, function(data) {
			if (data == "success") {
				$("#comment" + cID).html("<em>Comment reported.</em>");
			} else {
				alert("You must be logged in to report a comment.");
			}
		});
	}
</script>
<div class="subpage-container">
	<div class="subpage-legend">Comments</div>
	<asp:Panel ID="pnlCommentsFormContainer" runat="server" CssClass="subpage-inner-container">
		<table>
			<tr>
				<td><asp:TextBox ID="txtCommentBox" runat="server" TextMode="MultiLine" Width="460" Height="130" /></td>
			</tr>
			<tr>
				<td><asp:Button ID="btnSubmitComment" runat="server" OnClick="btnSubmitComment_clicked" Text="Submit Comment" CssClass="btn-default" /></td>
			</tr>
		</table>
	</asp:Panel>
	<asp:Panel ID="pnlMustLogin" runat="server" CssClass="subpage-inner-container">
	<script type="text/javascript">
		function checkCommentEnterButton(myfield, e) {
			var keycode;
			if (window.event) {
				keycode = window.event.keyCode;
			} else if (e) {
				keycode = e.which;
			}

			if (keycode == 13) {
				handleCommentLogin();
			}
		}

		function handleCommentLogin() {
			var username = $("#commentloginuser").val();
			var passwd = $("#commentloginpass").val();
			$("body").css("cursor", "progress");
			$.post("/ajax/proc-login.aspx", { u: username, p: passwd }, function (data) {
				$("body").css("cursor", "auto");
				if (data == "success") {
					// reload page
					location.reload();
				} else {
					alert("That username or password is invalid.");
					return false;
				}
			});
		}

		function switchCommentLogin() {
			$("#commentloginmsgcontainer").hide();
			$("#commentloginformcontainer").show();
		}
	</script>
	<div id="commentloginmsgcontainer">
	<a href="/account/login.aspx" onclick="switchCommentLogin(); return false;">Log in</a> or <a href="/account/register.aspx">register</a> to post a comment.
	</div>
	<div id="commentloginformcontainer" style="display: none;">
	<table class="row-box-container">
		<tr>
			<td>
				email<br />
				<input type="text" class="field" id="commentloginuser" maxlength="50" onkeypress="return checkCommentEnterButton(this,event);" />
			</td>
			<td>
				password<br />
				<input type="password" class="field" id="commentloginpass" maxlength="50" onkeypress="return checkCommentEnterButton(this,event);" />
			</td>
			<td valign="bottom">
				<input type="button" value="LOGIN" class="btn-default" onclick="handleCommentLogin();" />
			</td>
		</tr>
		<tr>
			<td colspan="2" class="links">
				<a href="/account/register.aspx">register</a> / <a href="/account/forgot-password.aspx">forgot password?</a>
			</td>
			<td>&nbsp;</td>
		</tr>
	</table>
	</div>
	</asp:Panel>
	<div id="comments-table-container" class="subpage-table-container">
	<table class="row-box-container">
	<asp:Repeater ID="rptComments" runat="server" OnItemDataBound="rptComments_databound">
	<ItemTemplate>
	<tr>
		<td><asp:Literal ID="litThumbnail" runat="server" /></td>
		<td>
			<span class="title"><a href="/account/view-user.aspx?id=<%#Eval("userID") %>"><%#Eval("username") %></a></span>
			<div id="comment<%#Eval("id") %>">
			<em><asp:Literal ID="litPostDate" runat="server" /></em><br />
			<%#Eval("body") %>
			<asp:Literal ID="litReportLink" runat="server" />
			</div>
		</td>
	</tr>
	</ItemTemplate>
	<AlternatingItemTemplate>
	<tr class="odd">
		<td><asp:Literal ID="litThumbnail" runat="server" /></td>
		<td>
			<span class="title"><a href="/account/view-user.aspx?id=<%#Eval("userID") %>"><%#Eval("username") %></a></span>
			<div id="comment<%#Eval("id") %>">
			<em><asp:Literal ID="litPostDate" runat="server" /></em><br />
			<%#Eval("body") %>
			<asp:Literal ID="litReportLink" runat="server" />
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
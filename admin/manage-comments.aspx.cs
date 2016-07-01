using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_manage_comments : System.Web.UI.Page {
	protected gamer.Comment comment;
	protected gamer.User user;
	protected int commentcount;
	protected int numpages;

	protected void Page_Load(object sender, EventArgs e) {
		user = new gamer.User();
		comment = new gamer.Comment();
		user.secureAdminPage();
		commentcount = 5;
		this.Master.pg = "admin comments";
		Toolbox toolbox = new Toolbox();
		int totalcomments = comment.GetFlaggedCommentCount();
		if (totalcomments == 0) {
			litPagingLinks.Text = "There are no flagged comments at this time.";
		} else {
			if (totalcomments <= commentcount) {
				pnlPagingLogic.Visible = false;
				numpages = 1;
			} else {
				// do paging logic
				numpages = totalcomments / commentcount + (totalcomments % commentcount > 0 ? 1 : 0);
				litPagingLinks.Text = "";
				string activelink;
				for (int i = 1; i <= numpages; i++) {
					if (i == 1) {
						activelink = " style=\"text-decoration: underline;\" ";
					} else {
						activelink = "";
					}
					litPagingLinks.Text += "<a id=\"comment-paging-link" + i.ToString() + "\" href=\"#\" onclick=\"getpage('" + i.ToString() +
						"'); return false;\"" + activelink + ">" + i.ToString() + "</a>&nbsp;&nbsp;";
				}
			}
			if (!IsPostBack) {
				rptComments.DataSource = comment.GetFlaggedComments(commentcount, 1);
				rptComments.DataBind();
			}
		}
	}

	protected void rptComments_databound(object o, RepeaterItemEventArgs e) {
		gamer.Comment comment = (gamer.Comment)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		Literal litPostDate = (Literal)e.Item.FindControl("litPostDate");
		litPostDate.Text = comment.submissionDate.ToString("M/d/yyyy h:m tt");
		litThumbnail.Text = "<img src=\"/thumb.aspx?i=uploads/users/" + comment.avatar + "&w=70\" alt=\"" + comment.username + "\" />";
	}
}
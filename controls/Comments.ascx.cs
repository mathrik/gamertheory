using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_Comments : System.Web.UI.UserControl {
	public int objecttype;
	public int objectID;
	protected gamer.Comment comment;
	protected gamer.User user;
	protected int commentcount;
	protected int numpages;

	protected void Page_Load(object sender, EventArgs e) {
		comment = new gamer.Comment();
		user = new gamer.User();
		commentcount = 5;
		if (user.loadFromSession()) {
			pnlMustLogin.Visible = false;
		} else {
			pnlCommentsFormContainer.Visible = false;
		}
		int totalcomments = comment.GetCommentCount(objectID, objecttype);
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
				litPagingLinks.Text += "<a id=\"comment-paging-link" + i.ToString() +  "\" href=\"#\" onclick=\"getpage('" + i.ToString() + 
					"'); return false;\"" + activelink + ">" + i.ToString() + "</a>&nbsp;&nbsp;";
			}
		}
		if (!IsPostBack) {
			rptComments.DataSource = comment.GetPagedComments(objectID, objecttype, commentcount, 1);
			rptComments.DataBind();
		}
	}

	protected void rptComments_databound(object o, RepeaterItemEventArgs e) {
		gamer.Comment comment = (gamer.Comment)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		Literal litPostDate = (Literal)e.Item.FindControl("litPostDate");
		litPostDate.Text = comment.submissionDate.ToString("M/d/yyyy h:m tt");
		litThumbnail.Text = "<img src=\"/thumb.aspx?i=uploads/users/" + comment.avatar + "&w=70\" alt=\"" + comment.username + "\" />";
		if (comment.flagged == 0) {
			Literal litReportLink = (Literal)e.Item.FindControl("litReportLink");
			litReportLink.Text = "<br /><a href=\"#\" onclick=\"reportcomment(" + comment.id.ToString() + 
				"); return false;\">Report as inappropriate</a>";
		}
	}

	protected void btnSubmitComment_clicked(object o, EventArgs e) {
		comment = new gamer.Comment();
		Toolbox tools = new Toolbox();
		if (user.id > 0) {
			comment.body = tools.cleanUserSubmission(txtCommentBox.Text);
			comment.userID = user.id;
			comment.objectID = objectID;
			comment.typeID = objecttype;
			comment.flagged = 0;
			comment.Add();
			// load comments to final page so the user sees his newly posted gem
			rptComments.DataSource = comment.GetPagedComments(objectID, objecttype, 10, 1);
			rptComments.DataBind();
			txtCommentBox.Text = "";
		}
	}
}
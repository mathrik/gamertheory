using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user : System.Web.UI.Page {
	protected int userID;

	protected void Page_Load(object sender, EventArgs e) {
		try {
			string friendlyurl = Request.PathInfo;
			int firstslash = friendlyurl.Substring(1).IndexOf("/");
			try {
				if (friendlyurl.Substring(firstslash + 1, 8) == "/uploads") {
					// redirect to actual file
					Response.Redirect(friendlyurl.Substring(firstslash + 1, friendlyurl.Length - firstslash - 1));
					Response.End();
				}
			} catch {
				// in the clear
			}
			Toolbox tools = new Toolbox();
			userID = tools.getInt(friendlyurl.Substring(1, firstslash));
			//gameID = Convert.ToInt32(Request.QueryString["id"]);
		} catch {
			userID = 0;
		}
		//this.Master.pg = "account-home";
		gamer.User viewuser = new gamer.User();
		if (viewuser.LoadByID(userID)) {
			litUsername.Text = viewuser.username;
			gamer.Comment comment = new gamer.Comment();
			rptRecentPosts.DataSource = comment.GetUserComments(viewuser.id);
			rptRecentPosts.DataBind();
			imgAvatar.ImageUrl = "/thumb.aspx?i=uploads/users/" + viewuser.avatar + "&w=400";
			gamer.Story story = new gamer.Story();
			rptGames.DataSource = story.LoadStoriesMultipleTypes_User(viewuser.getStoryTypes(), viewuser.id);
			rptGames.DataBind();
		} else {
			Response.Redirect("/account/default.aspx?f=" + userID);
			Response.End();
		}
	}

	protected void rptRecentPosts_databound(object o, RepeaterItemEventArgs e) {
		gamer.Comment comment = (gamer.Comment)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		Literal litUsername = (Literal)e.Item.FindControl("litUsername");
		Literal litReadMore = (Literal)e.Item.FindControl("litReadMore");
		Literal litReleaseDate = (Literal)e.Item.FindControl("litReleaseDate");
		litReleaseDate.Text = comment.submissionDate.ToString("M/d");
		litReadMore.Text = "<a class=\"read-more\" href=\"/comment-handler.aspx?id=" + comment.id + "\">See Story</a>";
		litUsername.Text = comment.username;
		//litThumbnail.Text = "<img src=\"/thumb.aspx?i=uploads/users/" + comment.avatar + "&w=70\" alt=\"" + comment.username + "\" />";
	}

	protected void rptGames_ItemCommand(object source, RepeaterCommandEventArgs e) {

	}

	protected void rptGames_DataBound(object source, RepeaterItemEventArgs e) {
	}
}
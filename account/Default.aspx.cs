using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class account_Default : System.Web.UI.Page {
	gamer.User user = new gamer.User();
	
	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "account-home";
		if (user.loadFromSession()) {
			litDisplayEmail.Text = user.email;
			litUsername.Text = user.username;
			gamer.Comment comment = new gamer.Comment();
			rptRecentPosts.DataSource = comment.GetUserComments(user.id);
			rptRecentPosts.DataBind();
			imgAvatar.ImageUrl = "/thumb.aspx?i=uploads/users/" + user.avatar + "&w=70";
			gamer.Story story = new gamer.Story();
			rptGames.DataSource = story.LoadStoriesMultipleTypes_User(user.getStoryTypes(), user.id);
			rptGames.DataBind();
		} else {
			Response.Redirect("login.aspx");
			Response.End();
		}
	}

	protected void btnChangeEmail_clicked(object o, EventArgs e) {
		user.email = txtEmail.Text;
		user.Update();
		litMsg.Text = "<div class=\"status\">Email Updated.</div>";
	}

	protected void btnChangePassword_clicked(object o, EventArgs e) {
		if (txtPass1.Text == txtPass2.Text) {
			user.password = txtPass2.Text;
			user.UpdatePassword(txtPass2.Text);
			litMsg.Text = "<div class=\"status\">Password Updated.</div>";
		} else {
			litMsg.Text = "<div class=\"err\">Passwords do not match.</div>";
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

	protected void btnChangeAvatar_clicked(object o, EventArgs e) {
		FileHandler fileMgr = new FileHandler();
		string path = System.Configuration.ConfigurationManager.AppSettings["AvatarDirectory"].ToString();
		string oldavatar = user.avatar;
		user.avatar = fileMgr.save(fileAvatar, path);
		if (user.avatar.Length > 0) {
			user.Update();
			if (File.Exists(path + oldavatar)) {
				File.Delete(path + oldavatar);
			}
		}
		litMsg.Text = "<div class=\"status\">Avatar Updated.</div>";
		imgAvatar.ImageUrl = "/thumb.aspx?i=uploads/users/" + user.avatar + "&w=70";
	}

	protected void rptGames_ItemCommand(object source, RepeaterCommandEventArgs e) {

	}

	protected void rptGames_DataBound(object source, RepeaterItemEventArgs e) {
	}
}

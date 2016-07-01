using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _default : System.Web.UI.Page{
	protected int homecategory;

	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "home";
		Toolbox toolbox = new Toolbox();
		homecategory = toolbox.getInt(Request.QueryString["p"]);
		gamer.Story story = new gamer.Story();
		rptPodcasts.DataSource = story.LoadRecentByType(3, 7);
		rptPodcasts.DataBind();
		rptUpdates.DataSource = story.LoadRecentFeaturedUpdates(8, homecategory);
		rptUpdates.DataBind();
		gamer.Comment comment = new gamer.Comment();
		rptRecentPosts.DataSource = comment.GetRecentComments(3);
		rptRecentPosts.DataBind();
		Billboard billboard = new Billboard();
		if (billboard.LoadByID(1)) {
			string path = System.Configuration.ConfigurationManager.AppSettings["HomeBillboardWebPath"].ToString();
			litHomeBillboard.Text = "<a href='" + billboard.url + "'><img src='" + path + billboard.filename + "' alt='' /></a>";
		}

	}

	protected void rptPodcasts_databound(object o, RepeaterItemEventArgs e) {
		gamer.Story podcast = (gamer.Story)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		Literal litReleaseDate = (Literal)e.Item.FindControl("litReleaseDate");
		litReleaseDate.Text = podcast.submissionDate.ToString("M/d");
		if (podcast.thumbnail.Length > 0) {
			string thumbnailWebPath = System.Configuration.ConfigurationManager.AppSettings["StoryThumbWebPath"].ToString();
			litThumbnail.Text = "<img src=\"/thumb.aspx?i=" + thumbnailWebPath + podcast.thumbnail + "&w=140\" alt=\"" + podcast.title + "\" />";
		}
		//litThumbnail.Text = "<img src=\"/thumb.aspx?i=uploads/podcasts/podcast_34.jpg&w=70\" alt=\"\" />";
	}

	protected void rptRecentPosts_databound(object o, RepeaterItemEventArgs e) {
		gamer.Comment comment = (gamer.Comment)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		Literal litUsername = (Literal)e.Item.FindControl("litUsername");
		Literal litReadMore = (Literal)e.Item.FindControl("litReadMore");
		Literal litReleaseDate = (Literal)e.Item.FindControl("litReleaseDate");
		Literal litBody = (Literal)e.Item.FindControl("litBody");
		litReleaseDate.Text = comment.submissionDate.ToString("M/d");
		litReadMore.Text = "<a class=\"read-more\" href=\"/comment-handler.aspx?id=" + comment.id + "\">READ MORE</a>";
		litUsername.Text = comment.username;
		litThumbnail.Text = "<img src=\"/thumb.aspx?i=uploads/users/" + comment.avatar + "&w=70\" alt=\"" + comment.username + "\" />";
		Toolbox tools = new Toolbox();
		litBody.Text = tools.getWholeWordPreview(comment.body, 130);
		if (comment.body.Length > litBody.Text.Length) { litBody.Text += "..."; }
	}

	protected void rptUpdates_databound(object o, RepeaterItemEventArgs e) {
		gamer.Story story = (gamer.Story)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		if (story.thumbnail.Length > 0) {
			string thumbnailWebPath = System.Configuration.ConfigurationManager.AppSettings["StoryThumbWebPath"].ToString();
			litThumbnail.Text = "<img src=\"/thumb.aspx?i=" + thumbnailWebPath + story.thumbnail + "&w=140\" alt=\"" + story.title + "\" />";
		}
	}
}

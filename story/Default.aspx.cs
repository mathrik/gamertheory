using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class story_Default : System.Web.UI.Page {
	protected int storyID;

	protected void Page_Load(object sender, EventArgs e) {
		Toolbox tools = new Toolbox();
		storyID = tools.getInt(Request.QueryString["id"]);
		gamer.Story story = new gamer.Story();
		gamer.User user = new gamer.User();
		story.id = storyID;
		if (story.LoadByID()) {
			/**************** SCRIPT ENDS HERE ************/
			Response.Redirect(story.permalink);
			Response.End();

			litTitle.Text = story.title;
			user.LoadByID(story.submitter);
			litByline.Text = user.username;
			litBody.Text = "";
			if (story.thumbnail.Length > 0) {
				string thumbnailWebPath = System.Configuration.ConfigurationManager.AppSettings["StoryThumbWebPath"].ToString();
				litBody.Text += "<img src=\"/thumb.aspx?i=" + thumbnailWebPath + story.thumbnail + "&w=200\" alt=\"" + story.title + 
					"\"  style=\"float: left; padding: 5px 15px 10px 0px;\"/>";
			}
			litBody.Text += story.body;
			gamerComments.objectID = story.id;
			gamerComments.objecttype = 1;
		} else {
			Response.Redirect("/news/");
		}
	}
}
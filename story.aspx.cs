using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class story : System.Web.UI.Page {
	protected int storyID;

	protected void Page_Load(object sender, EventArgs e) {
		Toolbox tools = new Toolbox();
		if (Request.PathInfo.Length > 0) {
			string friendlyurl = Request.PathInfo;
			int firstslash = friendlyurl.Substring(1).IndexOf("/");
			string test = friendlyurl.Substring(firstslash + 1, 8);
			if (friendlyurl.Substring(firstslash + 1, 8) == "/uploads") {
				// redirect to actual file
				Response.Redirect(friendlyurl.Substring(firstslash + 1, friendlyurl.Length - firstslash - 1));
				Response.End();
			}
			storyID = tools.getInt(friendlyurl.Substring(1,firstslash));
			gamer.Story story = new gamer.Story();
			gamer.User user = new gamer.User();
			story.id = storyID;
			if (story.LoadByID()) {
				Master.pgtitle = story.title;
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
		} else if (Request.QueryString["storyID"].Length > 0) {
			storyID = tools.getInt(Request.QueryString["storyID"]);
			gamer.Story story = new gamer.Story();
			gamer.User user = new gamer.User();
			story.id = storyID;
			if (story.LoadByID()) {
				Master.pgtitle = story.title;
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

 		} else {
			Response.Redirect("/story/");
		}
	}
}
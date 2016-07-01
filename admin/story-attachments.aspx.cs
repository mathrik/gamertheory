using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class admin_story_attachments : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		string type;
		int storyID;
		Toolbox toolbox = new Toolbox();
		gamer.Story story = new gamer.Story();
		gamer.User user = new gamer.User();
		user.secureUserPage();

		storyID = toolbox.getInt(Request.Form["story"]);
		type = Request.Form["type"].ToLower();
		story.id = storyID;
		if (story.LoadByID() && (story.submitter == user.id || user.canEditOtherStories())) {
			switch (type) {
				case "developer":
					gamer.Developer dev = new gamer.Developer();
					List<gamer.Developer> devList = dev.LoadStoryDevelopers(story.id);
					if (devList.Count > 0) {
						Response.Write("<ul class=\"relation-list\">");
						for (int i = 0; i < devList.Count; i++) {
							Response.Write("<li>" + devList[i].title + " &nbsp; " +
								"<input type=\"button\" value=\"Remove\" class=\"btn-default\" onclick='removeItem(" +
								devList[i].id + ", \"" + type + "\");'/></li>");
						}
						Response.Write("</ul>");
					}
					break;
				case "game":
					gamer.Game theGame = new gamer.Game();
					List<gamer.Game> gameList = theGame.LoadStoryGames(story.id);
					if (gameList.Count > 0) {
						Response.Write("<ul class=\"relation-list\">");
						for (int i = 0; i < gameList.Count; i++) {
							Response.Write("<li>" + gameList[i].title + " &nbsp; " +
								"<input type=\"button\" value=\"Remove\" class=\"btn-default\" onclick='removeItem(" +
								gameList[i].id + ", \"" + type + "\");'/></li>");
						}
						Response.Write("</ul>");
					}
					break;
				case "genre":
					gamer.Genre genre = new gamer.Genre();
					List<gamer.Genre> genreList = genre.LoadStoryGenres(story.id);
					if (genreList.Count > 0) {
						Response.Write("<ul class=\"relation-list\">");
						for (int i = 0; i < genreList.Count; i++) {
							Response.Write("<li>" + genreList[i].title + " &nbsp; " +
								"<input type=\"button\" value=\"Remove\" class=\"btn-default\" onclick='removeItem(" +
								genreList[i].id + ", \"" + type + "\");'/></li>");
						}
						Response.Write("</ul>");
					}
					break;
				case "platform":
					gamer.Platform platform = new gamer.Platform();
					List<gamer.Platform> platformList = platform.LoadStoryPlatforms(story.id);
					if (platformList.Count > 0) {
						Response.Write("<ul class=\"relation-list\">");
						for (int i = 0; i < platformList.Count; i++) {
							Response.Write("<li>" + platformList[i].title + " &nbsp; " +
								"<input type=\"button\" value=\"Remove\" class=\"btn-default\" onclick='removeItem(" +
								platformList[i].id + ", \"" + type + "\");'/></li>");
						}
						Response.Write("</ul>");
					}
					break;
				case "publisher":
					gamer.Publisher publisher = new gamer.Publisher();
					List<gamer.Publisher> publisherList = publisher.LoadStoryPublishers(story.id);
					if (publisherList.Count > 0) {
						Response.Write("<ul class=\"relation-list\">");
						for (int i = 0; i < publisherList.Count; i++) {
							Response.Write("<li>" + publisherList[i].title + " &nbsp; " +
								"<input type=\"button\" value=\"Remove\" class=\"btn-default\" onclick='removeItem(" +
								publisherList[i].id + ", \"" + type + "\");'/></li>");
						}
						Response.Write("</ul>");
					}
					break;
			}
		} else {
			Response.Write("Bad story");
			Response.End();
		}
	}
}

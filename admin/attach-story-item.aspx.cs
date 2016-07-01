using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class admin_attach_story_item : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		string title;
		string type;
		int storyID;
		Toolbox toolbox = new Toolbox();
		gamer.Story story = new gamer.Story();
		gamer.User user = new gamer.User();
		user.secureUserPage();

		storyID = toolbox.getInt(Request.Form["story"]);
		title = Request.Form["title"];
		type = Request.Form["type"].ToLower();
		story.id = storyID;
		if (story.LoadByID() && (story.submitter == user.id || user.canEditOtherStories())) {
			switch (type) {
				case "developer":
					gamer.Developer dev = new gamer.Developer();
					int devID;
					devID = dev.FetchDeveloperFromTitle(title);
					story.AddDeveloper(devID);
					Response.Write("success");
					Response.End();
					break;
				case "game":
					gamer.Game game = new gamer.Game();
					int gameID;
					gameID = game.FetchGameFromTitle(title);
					if (gameID > 0) {
						story.AddGame(gameID);
						Response.Write("success");
						Response.End();
						break;
					} else {
						Response.Write("failed to add game");
						Response.End();
						break;
					}
				case "genre":
					gamer.Genre genre = new gamer.Genre();
					int genreID;
					genreID = genre.FetchGenreFromTitle(title);
					story.AddGenre(genreID);
					Response.Write("success");
					Response.End();
					break;
				case "platform":
					gamer.Platform platform = new gamer.Platform();
					story.AddPlatform(platform.FetchPlatformFromTitle(title));
					Response.Write("success");
					Response.End();
					break;
				case "publisher":
					gamer.Publisher publisher = new gamer.Publisher();
					story.AddPublisher(publisher.FetchPublisherFromTitle(title));
					Response.Write("success");
					Response.End();
					break;
				default:
					Response.Write("Bad item type");
					Response.End();
					break;
			}
		} else {
			Response.Write("Bad story");
			Response.End();
		}
	}
}

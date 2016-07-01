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

public partial class admin_remove_story_item : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		int itemID;
		string type;
		int storyID;
		Toolbox toolbox = new Toolbox();
		gamer.Story story = new gamer.Story();
		gamer.User user = new gamer.User();
		user.secureUserPage();

		storyID = toolbox.getInt(Request.Form["story"]);
		itemID = toolbox.getInt(Request.Form["item"]);
		type = Request.Form["type"].ToLower();
		story.id = storyID;
		if (story.LoadByID() && (story.submitter == user.id || user.canEditOtherStories())) {
			switch (type) {
				case "developer":
					story.RemoveDeveloper(itemID);
					Response.Write("success");
					Response.End();
					break;
				case "game":
					story.RemoveGame(itemID);
					Response.Write("success");
					Response.End();
					break;
				case "genre":
					story.RemoveGenre(itemID);
					Response.Write("success");
					Response.End();
					break;
				case "platform":
					story.RemovePlatform(itemID);
					Response.Write("success");
					Response.End();
					break;
				case "publisher":
					story.RemovePublisher(itemID);
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

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

public partial class admin_remove_story : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		int storyID;
		Toolbox toolbox = new Toolbox();
		gamer.Story story = new gamer.Story();
		gamer.User user = new gamer.User();
		user.secureUserPage();
		storyID = toolbox.getInt(Request.Form["id"]);
		story.id = storyID;
		if (story.LoadByID() && (story.submitter == user.id || user.canEditOtherStories())) {
			story.Delete();
			Response.Write("success");
		} else {
			Response.Write("failure");
		}
	}
}

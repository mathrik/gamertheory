using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class comment_handler : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		Toolbox tools = new Toolbox();
		int id = tools.getInt(Request.QueryString["id"]);
		gamer.Comment comment = new gamer.Comment();
		if (comment.LoadByID(id)) {
			switch (comment.typeID) {
				case 1:
					gamer.Story story = new gamer.Story();
					story.id = comment.objectID;
					if (story.LoadByID()) {
						switch (story.typeID) {
							case 7:
								Response.Redirect("/podcast/?id=" + story.id);
								Response.End();
								break;
							default:
								Response.Redirect("/story/?id=" + story.id);
								break;
						}
					} else {
						Response.Redirect("/default.aspx");
						Response.End();
					}
					break;
				case 2:
					Response.Redirect("/games/details.aspx?id=" + comment.objectID);
					Response.End();
					break;
				default:
					Response.Redirect("/default.aspx");
					Response.End();
					break;
			}
		}
	}
}
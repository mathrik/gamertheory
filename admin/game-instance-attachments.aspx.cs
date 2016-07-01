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

public partial class admin_game_instance_attachments : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		string type;
		int instanceID;
		Toolbox toolbox = new Toolbox();
		gamer.GameInstance instance = new gamer.GameInstance();
		gamer.User user = new gamer.User();
		user.secureAdminPage();

		instanceID = toolbox.getInt(Request.Form["instance"]);
		type = Request.Form["type"].ToLower();
		if (instance.LoadByID(instanceID)) {
			switch (type) {
				case "developer":
					gamer.Developer dev = new gamer.Developer();
					List<gamer.Developer> devList = dev.LoadGameDevelopers(instance.id);
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
				case "genre":
					gamer.Genre genre = new gamer.Genre();
					List<gamer.Genre> genreList = genre.LoadGameGenres(instance.id);
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
				case "publisher":
					gamer.Publisher publisher = new gamer.Publisher();
					List<gamer.Publisher> publisherList = publisher.LoadGamePublishers(instance.id);
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
			Response.Write("Bad game");
			Response.End();
		}
	}
}

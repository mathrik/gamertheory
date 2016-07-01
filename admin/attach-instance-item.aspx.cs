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

public partial class admin_attach_instance_itme : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		string title;
		string type;
		int instanceID;
		Toolbox toolbox = new Toolbox();
		gamer.GameInstance gameinstance = new gamer.GameInstance();
		gamer.User user = new gamer.User();
		user.secureAdminPage();

		instanceID = toolbox.getInt(Request.Form["instance"]);
		title = Request.Form["title"];
		type = Request.Form["type"].ToLower();
		gameinstance.id = instanceID;
		if (gameinstance.LoadByID(instanceID)) {
			switch (type) {
				case "developer":
					gamer.Developer dev = new gamer.Developer();
					int devID;
					devID = dev.FetchDeveloperFromTitle(title);
					gameinstance.AddDeveloper(devID);
					Response.Write("success");
					Response.End();
					break;
				case "genre":
					gamer.Genre genre = new gamer.Genre();
					int genreID;
					genreID = genre.FetchGenreFromTitle(title);
					gameinstance.AddGenre(genreID);
					Response.Write("success");
					Response.End();
					break;
				case "publisher":
					gamer.Publisher publisher = new gamer.Publisher();
					gameinstance.AddPublisher(publisher.FetchPublisherFromTitle(title));
					Response.Write("success");
					Response.End();
					break;
				default:
					Response.Write("Bad item type");
					Response.End();
					break;
			}
		} else {
			Response.Write("Bad game.");
			Response.End();
		}
	}
}

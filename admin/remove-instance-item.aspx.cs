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

public partial class admin_remove_instance_item : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		int itemID;
		string type;
		int instanceID;
		Toolbox toolbox = new Toolbox();
		gamer.GameInstance gameinstance = new gamer.GameInstance();
		gamer.User user = new gamer.User();
		user.secureUserPage();

		instanceID = toolbox.getInt(Request.Form["instance"]);
		itemID = toolbox.getInt(Request.Form["item"]);
		type = Request.Form["type"].ToLower();
		gameinstance.id = instanceID;
		if (gameinstance.LoadByID(instanceID)) {
			switch (type) {
				case "developer":
					gameinstance.RemoveDeveloper(itemID);
					Response.Write("success");
					Response.End();
					break;
				case "genre":
					gameinstance.RemoveGenre(itemID);
					Response.Write("success");
					Response.End();
					break;
				case "publisher":
					gameinstance.RemovePublisher(itemID);
					Response.Write("success");
					Response.End();
					break;
				default:
					Response.Write("Bad item type");
					Response.End();
					break;
			}
		} else {
			Response.Write("Bad Game");
			Response.End();
		}
	}
}

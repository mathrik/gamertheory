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

public partial class admin_remove_game_image : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		int imageID;
		Toolbox toolbox = new Toolbox();
		gamer.GameScreenShot image = new gamer.GameScreenShot();
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		imageID = toolbox.getInt(Request.Form["id"]);
		if (image.LoadByID(imageID)) {
			image.Delete();
			Response.Write("success");
		} else {
			Response.Write("failure");
		}
	}
}

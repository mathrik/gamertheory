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

public partial class admin_remove_game : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		int gameID;
		Toolbox toolbox = new Toolbox();
		gamer.Game game = new gamer.Game();
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		gameID = toolbox.getInt(Request.Form["id"]);
		if (game.LoadByID(gameID)) {
			game.Delete();
			Response.Write("success");
		} else {
			Response.Write("failure");
		}
	}
}

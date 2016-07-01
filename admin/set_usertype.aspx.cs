using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_set_usertype : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		Toolbox toolbox = new Toolbox();
		user.secureAdminPage();
		int userID = toolbox.getInt(Request.Form["u"]);
		int userlevel = toolbox.getInt(Request.Form["t"]);
		user.change_userlevel(userID, userlevel);
		Response.Write("success");
		Response.End();
	}
}
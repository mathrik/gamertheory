using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_suspend_user : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		Toolbox toolbox = new Toolbox();
		user.secureAdminPage();
		int userID = toolbox.getInt(Request.Form["u"]);
		bool permanent = Request.Form["p"] == "1";
		DateTime expiration;
		try {
			expiration = Convert.ToDateTime(Request.Form["e"]);
		} catch {
			expiration = DateTime.Now;
		}
		user.suspend_user(userID, permanent, expiration);
		Response.Write("success");
		Response.End();
	}
}
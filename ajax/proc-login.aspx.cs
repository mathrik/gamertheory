using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_proc_login : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User this_user = new gamer.User();
		string username = Request.Form["u"];
		string password = Request.Form["p"];
		if (this_user.ValidateUser(username, password)) {
			this_user.saveToSession();
			Response.Write("success");
		} else {
			Response.Write("fail");
		}
	}
}
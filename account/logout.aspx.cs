using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class account_logout : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.killSession();
		Response.Redirect("login.aspx");
	}
}
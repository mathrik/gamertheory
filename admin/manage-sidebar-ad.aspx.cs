using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_manage_sidebar_ad : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		this.Master.pg = "admin";
		Toolbox toolbox = new Toolbox();
		switch (toolbox.getInt(Request.QueryString["m"])) {
			case 1:
				litMsg.Text = "<div class=\"status\">Sidebar Added.</div>";
				break;
			case 2:
				litMsg.Text = "<div class=\"status\">Sidebar Updated.</div>";
				break;
		}
	}

	protected void btnSubmit_Click(object sender, EventArgs e) {

	}
}
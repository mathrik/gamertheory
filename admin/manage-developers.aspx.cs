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

public partial class admin_manage_developers : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		this.Master.pg = "admin developers";
		Toolbox toolbox = new Toolbox();
		gamer.Developer dev = new gamer.Developer();
		rptDevelopers.DataSource = dev.LoadAll();
		rptDevelopers.DataBind();
		switch (toolbox.getInt(Request.QueryString["m"])) {
			case 1:
				litMsg.Text = "<div class=\"status\">Developer Added.</div>";
				break;
			case 2:
				litMsg.Text = "<div class=\"status\">Developer Updated.</div>";
				break;
		}
	}

	protected void rptDevelopers_ItemCommand(object source, RepeaterCommandEventArgs e) {

	}

	protected void rptDevelopers_DataBound(object source, RepeaterItemEventArgs e) {
	}
}
